﻿////////////////////////////////////////////////////////////////////////////////////////////////  
//  An excellent resource for the JNI library is on the website 
// Java Native Interface: Programmer's Guide and Specification by Sheng Liang 
//  http://docs.oracle.com/javase/7/docs/technotes/guides/jni/
// for a list of all the functions 
// http://download.oracle.com/javase/6/docs/technotes/guides/jni/spec/functions.html
////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace JNI
{
    public unsafe class JavaNativeInterface : IDisposable
    {
        private const string JRE_REGISTRY_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft\Java Runtime Environment";

        private IntPtr javaClass;
        private IntPtr javaObject;
        private string javaClassName;
        private JavaVM jvm;
        private JNIEnv env;

        public bool AttachToCurrentJVMThread { get; set; }

        public void LoadVM(Dictionary<string, string> options, bool AddToExistingJVM)
        {
            // Get the location of the current version of the JVM.dll          
            string jreVersion = (string) Registry.GetValue(JRE_REGISTRY_KEY, "CurrentVersion", null);
            string keyName = Path.Combine(JRE_REGISTRY_KEY, jreVersion);

            string jvmDir = (string) Registry.GetValue(keyName, "RuntimeLib", null);

            if ((jvmDir.Length == 0) || (!File.Exists(jvmDir)))
                throw new Exception("Error determining the location of the Java Runtime Environment");

            // Set the directory to the location of the JVM.dll. 
            // This will ensure that the API call JNI_CreateJavaVM will work
            Directory.SetCurrentDirectory(Path.GetDirectoryName(jvmDir));

            var args = new JavaVMInitArgs();

            switch (Convert.ToInt32((decimal.Parse(jreVersion.Substring(0, 3)) - 1)/2*10))
            {
                case 0:
                    throw new Exception("Unsupported java version. Please upgrade your version of the JRE.");

                case 1:
                    args.version = JNIVersion.JNI_VERSION_1_2;
                    break;
                case 2:
                    args.version = JNIVersion.JNI_VERSION_1_4;
                    break;
                default:
                    args.version = JNIVersion.JNI_VERSION_1_6;
                    break;
            }

            args.ignoreUnrecognized = JavaVM.BooleanToByte(true); // True

            if (options.Count > 0)
            {
                args.nOptions = options.Count;
                var opt = new JavaVMOption[options.Count];
                int i = 0;
                foreach (KeyValuePair<string, string> kvp in options)
                {
                    opt[i++].optionString = Marshal.StringToHGlobalAnsi(kvp.Key.ToString() + "=" + kvp.Value.ToString());
                }
                fixed (JavaVMOption* a = &opt[0])
                {
                    // prevents the garbage collector from relocating the opt variable as this is used in unmanaged code that the gc does not know about
                    args.options = a;
                }
            }

            if (!AttachToCurrentJVMThread)
            {
                IntPtr environment;
                IntPtr javaVirtualMachine;
                int result = JavaVM.JNI_CreateJavaVM(out javaVirtualMachine, out environment, &args);
                if (result != JNIReturnValue.JNI_OK)
                {
                    throw new Exception("Cannot create JVM " + result.ToString());
                }

                jvm = new JavaVM(javaVirtualMachine);
                env = new JNIEnv(environment);
            }
            else AttachToCurrentJVM(args);
        }

        private void AttachToCurrentJVM(JavaVMInitArgs args)
        {
            // This is only required if you want to reuse the same instance of the JVM
            // This is especially useful if you are using JNI in a webservice. see page 89 of the
            // Java Native Interface: Programmer's Guide and Specification by Sheng Liang
            if (AttachToCurrentJVMThread)
            {
                int nVMs;

                IntPtr javaVirtualMachine;
                int res = JavaVM.JNI_GetCreatedJavaVMs(out javaVirtualMachine, 1, out nVMs);
                if (res != JNIReturnValue.JNI_OK)
                {
                    throw new Exception("JNI_GetCreatedJavaVMs failed (" + res.ToString() + ")");
                }
                if (nVMs > 0)
                {
                    jvm = new JavaVM(javaVirtualMachine);
                    res = jvm.AttachCurrentThread(out env, args);
                    if (res != JNIReturnValue.JNI_OK)
                    {
                        throw new Exception("AttachCurrentThread failed (" + res.ToString() + ")");
                    }
                }
            }
        }

        public void InstantiateJavaObject(string ClassName)
        {
            // need to create class before we can call any methods
            javaClassName = ClassName;
            try
            {
                javaClass = env.FindClass(javaClassName);

                IntPtr methodId = env.GetMethodId(javaClass, "<init>", "()V");
                javaObject = env.NewObject(javaClass, methodId, new JValue() {});
            }
            catch
            {
                throw new Exception(env.CatchJavaException());
            }
        }

        public void CallVoidMethod(string methodName, string sig, List<object> param)
        {
            try
            {
                IntPtr methodId = env.GetMethodId(javaClass, methodName, sig);
                env.CallVoidMethod(javaObject, methodId, ParseParameters(sig, param));
            }
            catch
            {
                throw new Exception(env.CatchJavaException());
            }
        }

        private JValue[] ParseParameters(string sig, List<object> param)
        {
            JValue[] retval = new JValue[param.Count];

            int startIndex = sig.IndexOf('(') + 1;

            for (int i = 0; i < param.Count; i++)
            {
                string paramSig = "";
                if (sig.Substring(startIndex, 1) == "[")
                    paramSig = sig.Substring(startIndex++, 1); 

                if (sig.Substring(startIndex, 1) == "L") {
                  paramSig = paramSig + sig.Substring(startIndex, sig.IndexOf(';', startIndex) - startIndex);
                  startIndex++; // skip past ;
                }
                else
                    paramSig = paramSig + sig.Substring(startIndex, 1);

                startIndex = startIndex + (paramSig.Length - (paramSig.IndexOf("[", StringComparison.Ordinal) + 1 ));
                
                if (param[i] is string)
                {
                    if (!paramSig.Equals("Ljava/lang/String"))
                    {
                        throw new Exception("Signature (" + paramSig + ") does not match parameter value (" + param[i].GetType().ToString() + ").");
                    }
                    retval[i] = new JValue() { l = env.NewString(param[i].ToString(), param[i].ToString().Length) };
                } else if (param[i] == null) {
                    retval[i] = new JValue(); // Just leave as default value
                }
                else if (paramSig.StartsWith("["))
                {
                    retval[i] = ProcessArrayType(paramSig, param[i]);
                }
                else
                {
                    retval[i] = new JValue();
                    FieldInfo paramField = retval[i].GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).AsQueryable().FirstOrDefault(a => a.Name.ToUpper().Equals(paramSig));
                    if ((paramField != null) && ((param[i].GetType() == paramField.FieldType) || ((paramField.FieldType == typeof(bool)) && (param[i] is byte))))
                    {
                        paramField.SetValueDirect(__makeref(retval[i]),paramField.FieldType == typeof (bool)  // this is an undocumented feature to set struct fields via reflection
                                                      ? JavaVM.BooleanToByte((bool)param[i])
                                                      : param[i]);
                    }
                    else throw new Exception("Signature (" + paramSig + ") does not match parameter value (" + param[i].GetType().ToString() + ")."); 
                }                                                 
            }
            return retval;
        }

        private JValue ProcessArrayType(string paramSig, object param)
        {
            IntPtr arrPointer;
            if (paramSig.Equals("[I"))
                arrPointer = env.NewIntArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[J"))
                arrPointer = env.NewLongArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[C"))
                arrPointer = env.NewCharArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[B")) 
                arrPointer = env.NewByteArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[S"))
                arrPointer = env.NewShortArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[D"))
                arrPointer = env.NewDoubleArray(((Array)param).Length, javaClass);
            else if (paramSig.Equals("[F"))
                arrPointer = env.NewFloatArray(((Array)param).Length, javaClass);
            else if (paramSig.Contains("[Ljava/lang/"))
                arrPointer = env.NewObjectArray(((Array)param).Length, javaClass, (IntPtr)param) ;
            else
            {
                throw new Exception("Signature (" + paramSig + ") does not match parameter value (" +
                                   param.GetType().ToString() + "). All arrays types should be defined as objects because I do not have enough time to defines every possible array type");
            }

            if (paramSig.Contains("[Ljava/lang/"))
            {  // we are dealing with an object so create it on the heap and pass a pointer to it to the stack
                for (int j = 0; j < ((Array)param).Length; j++)
                {
                    object obj = ((Array)param).GetValue(j);

                    if (paramSig.Contains("[Ljava/lang/String"))
                    {
                        IntPtr str = env.NewString(obj.ToString(), obj.ToString().Length);
                        env.SetObjectArrayElement(arrPointer, j, str);
                    }
                    else
                        env.SetObjectArrayElement(arrPointer, j, (IntPtr)obj);
                }
            }
            env.PackPrimitiveArray<int>((int[])param, arrPointer);
            return new JValue() { l = arrPointer };
        }

        public T CallMethod<T>(string methodName, string sig, List<object> param)
        {
            IntPtr methodId = env.GetMethodId(javaClass, methodName, sig);
            try
            {
                if (typeof (T) == typeof (byte))
                {
                    // Call the byte method 
                    byte res = env.CallByteMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (bool))
                {
                    // Call the boolean method 
                    bool res = env.CallBooleanMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                if (typeof (T) == typeof (char))
                {
                    // Call the char method 
                    char res = env.CallCharMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (short))
                {
                    // Call the short method 
                    short res = env.CallShortMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (int))
                {
                    // Call the int method               
                    int res = env.CallIntMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (long))
                {
                    // Call the long method 
                    long res = env.CallLongMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (float))
                {
                    // Call the float method 
                    float res = env.CallFloatMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res;
                }
                else if (typeof (T) == typeof (double))
                {
                    // Call the double method 
                    double res = env.CallDoubleMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T) (object) res; // need to fix this
                }
                else if (typeof (T) == typeof (string))
                {
                    // Call the string method 
                    IntPtr jstr = env.CallObjectMethod(javaObject, methodId, ParseParameters(sig, param));

                    string res = env.JStringToString(jstr);
                    env.DeleteLocalRef(jstr);
                    return (T) (object) res;
                }
                else if (typeof(T) == typeof(byte[]))
                {
                    // Call the string method
                    IntPtr jstr = env.CallStaticObjectMethod(javaObject, methodId, ParseParameters(sig, param));
                    if (jstr.ToInt32() == 0)
                    {
                        return default(T);
                    }
                    byte[] res = env.JStringToByte(jstr);
                    env.DeleteLocalRef(jstr);
                    return (T)(object)res;
                }
                else if (typeof(T) == typeof(object))
                {
                    // Call the object method and deal with whatever comes back in the call code 
                    IntPtr res = env.CallObjectMethod(javaObject, methodId, ParseParameters(sig, param));
                    return (T)(object)res;                    
                }
                return default(T);
            }
            catch
            {
                throw new Exception(env.CatchJavaException());
            }
        }

        public string JavaVersion()
        {
            int majorVersion = env.GetMajorVersion();
            int minorVersion = env.GetMinorVersion();
            return majorVersion.ToString() + "." + minorVersion.ToString();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~JavaNativeInterface()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            // free native resources if there are any.
            if (javaClass != IntPtr.Zero)
            {
                env.DeleteGlobalRef(javaClass);
                javaClass = IntPtr.Zero;
            }

            if (javaObject != IntPtr.Zero)
            {
                env.DeleteLocalRef(javaObject);
                javaObject = IntPtr.Zero;
            }

            if (disposing)
            {
                // free managed resources
                if (jvm != null)
                {
                    jvm.Dispose();
                    jvm = null;
                }

                if (env != null)
                {
                    env.Dispose();
                    env = null;
                }
            }
        }
    }
}
