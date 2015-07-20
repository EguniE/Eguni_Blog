using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JNI;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class frmJNITestHarness : Form
    {
        JavaNativeInterface Java;
      //  private List<string> jvmOptions;
        private readonly Dictionary<string, string> pair = new Dictionary<string, string>();

        public frmJNITestHarness()
        {
            InitializeComponent();
        //    jvmOptions = new List<string>();
        }

        public void AddJVMOption(string name, string value)
        {
            if (!pair.ContainsKey(name))
              pair.Add(name, value);

         //   jvmOptions.Add(name + "=" + value);
        }
        private void btLoadJVM_Click(object sender, EventArgs e)
        {
            Java = new JavaNativeInterface();

            // Set the path where the java class is located
            AddJVMOption("-Djava.class.path", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Java.LoadVM(pair, false);
            Java.InstantiateJavaObject(tbClassName.Text);
            statusStrip1.Items[0].Text = "JVM Loaded and class instantiated";
        }

        private void btJavaProcedure_Click(object sender, EventArgs e)
        {  // Call the Java method that takes a string and returns a Void i.e "(Ljava/lang/String;)V"
            List<object> olParameters = new List<object>();
            olParameters.Add(tbUserName.Text);
            Java.CallVoidMethod("HelloWorldProcedure", "(Ljava/lang/String;)V", olParameters);
            statusStrip1.Items[0].Text = "HelloWorldProcedure called";
        }

        private void btJavaFunction_Click(object sender, EventArgs e)
        {
            List<object> olParameters = new List<object>();
            //olParameters.Add((int)nudFirstValue.Value);
            //olParameters.Add((int)nudSecondValue.Value);
            //olParameters.Add(tbUserName.Text);

            //string msg = Java.CallMethod<int>("AddTwoNumbers", "(IILjava/lang/String;)I", olParameters).ToString();

            int[] intArr = new int[2] { (int)nudFirstValue.Value, (int)nudSecondValue.Value };

            olParameters.Clear();
            olParameters.Add(intArr);
            olParameters.Add(tbUserName.Text);
            //      Java.CallVoidMethod("AddArray", "([I)V", olParameters);
            string msg = Java.CallMethod<int>("AddTwoNumbersFromArray", "([ILjava/lang/String;)I", olParameters).ToString(CultureInfo.InvariantCulture);

            MessageBox.Show("The sum of the two numbers is : " + msg);
            statusStrip1.Items[0].Text = "AddTwoNumbers returned : " + msg; ;
        }

        private void btJavaVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The version of JRE is : " + Java.JavaVersion());
        }

    }
}
