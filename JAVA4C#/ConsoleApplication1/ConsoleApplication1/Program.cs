using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JNI;
using System.IO;
using System.Reflection;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // JVM Log option Params Dictionary
            Dictionary<string, string> jvmParameters = new Dictionary<string, string>();
            JavaNativeInterface Java = new JavaNativeInterface();

            // Set class path
            string strKey = "-Djava.class.path";
            string strValue = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            jvmParameters.Add(strKey, strValue);

            Java.LoadVM(jvmParameters, false);
            // Instance Create: HelloWorld
            Java.InstantiateJavaObject("HelloWorld");

            // Print java version
            System.Console.WriteLine(Java.JavaVersion());

            // Params lists for JNI
            List<object> jniParam = new List<object>();

            // Call HelloWorldProcedure()
            jniParam.Add("WestWoodForever");
            Java.CallVoidMethod("HelloWorldProcedure", "(Ljava/lang/String;)V", jniParam);

            // Call AddTwoNumbers()
            jniParam.Clear();
            jniParam.Add(2);
            jniParam.Add(5);
            jniParam.Add("WestWoodForever");
            string returnJNI = Java.CallMethod<int>("AddTwoNumbers", "((IILjava/lang/String;)I", jniParam).ToString();
            System.Console.WriteLine(returnJNI);

            Java.Dispose();
        }
    }
}
