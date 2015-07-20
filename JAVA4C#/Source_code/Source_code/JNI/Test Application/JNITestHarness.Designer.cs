namespace WindowsFormsApplication1
{
    partial class frmJNITestHarness
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btLoadJVM = new System.Windows.Forms.Button();
            this.btJavaProcedure = new System.Windows.Forms.Button();
            this.btJavaFunction = new System.Windows.Forms.Button();
            this.btJavaVersion = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.gbUserDeatils = new System.Windows.Forms.GroupBox();
            this.nudSecondValue = new System.Windows.Forms.NumericUpDown();
            this.nudFirstValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.gbUserDeatils.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSecondValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstValue)).BeginInit();
            this.SuspendLayout();
            // 
            // btLoadJVM
            // 
            this.btLoadJVM.AutoEllipsis = true;
            this.btLoadJVM.Location = new System.Drawing.Point(0, 97);
            this.btLoadJVM.Name = "btLoadJVM";
            this.btLoadJVM.Size = new System.Drawing.Size(92, 26);
            this.btLoadJVM.TabIndex = 0;
            this.btLoadJVM.Text = "Load VM";
            this.btLoadJVM.UseVisualStyleBackColor = true;
            this.btLoadJVM.Click += new System.EventHandler(this.btLoadJVM_Click);
            // 
            // btJavaProcedure
            // 
            this.btJavaProcedure.Location = new System.Drawing.Point(0, 126);
            this.btJavaProcedure.Name = "btJavaProcedure";
            this.btJavaProcedure.Size = new System.Drawing.Size(92, 26);
            this.btJavaProcedure.TabIndex = 1;
            this.btJavaProcedure.Text = "Java method";
            this.btJavaProcedure.UseVisualStyleBackColor = true;
            this.btJavaProcedure.Click += new System.EventHandler(this.btJavaProcedure_Click);
            // 
            // btJavaFunction
            // 
            this.btJavaFunction.Location = new System.Drawing.Point(109, 97);
            this.btJavaFunction.Name = "btJavaFunction";
            this.btJavaFunction.Size = new System.Drawing.Size(92, 26);
            this.btJavaFunction.TabIndex = 2;
            this.btJavaFunction.Text = "Java int method";
            this.btJavaFunction.UseVisualStyleBackColor = true;
            this.btJavaFunction.Click += new System.EventHandler(this.btJavaFunction_Click);
            // 
            // btJavaVersion
            // 
            this.btJavaVersion.Location = new System.Drawing.Point(109, 126);
            this.btJavaVersion.Name = "btJavaVersion";
            this.btJavaVersion.Size = new System.Drawing.Size(92, 26);
            this.btJavaVersion.TabIndex = 3;
            this.btJavaVersion.Text = "Java Version";
            this.btJavaVersion.UseVisualStyleBackColor = true;
            this.btJavaVersion.Click += new System.EventHandler(this.btJavaVersion_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 155);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(205, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Users Name";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(99, 39);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(100, 20);
            this.tbUserName.TabIndex = 6;
            this.tbUserName.Text = "Simon";
            // 
            // gbUserDeatils
            // 
            this.gbUserDeatils.Controls.Add(this.nudSecondValue);
            this.gbUserDeatils.Controls.Add(this.nudFirstValue);
            this.gbUserDeatils.Controls.Add(this.label3);
            this.gbUserDeatils.Controls.Add(this.tbClassName);
            this.gbUserDeatils.Controls.Add(this.label2);
            this.gbUserDeatils.Controls.Add(this.tbUserName);
            this.gbUserDeatils.Controls.Add(this.label1);
            this.gbUserDeatils.Location = new System.Drawing.Point(0, 4);
            this.gbUserDeatils.Name = "gbUserDeatils";
            this.gbUserDeatils.Size = new System.Drawing.Size(205, 87);
            this.gbUserDeatils.TabIndex = 7;
            this.gbUserDeatils.TabStop = false;
            this.gbUserDeatils.Text = "User Details ";
            // 
            // nudSecondValue
            // 
            this.nudSecondValue.Location = new System.Drawing.Point(155, 61);
            this.nudSecondValue.Name = "nudSecondValue";
            this.nudSecondValue.Size = new System.Drawing.Size(43, 20);
            this.nudSecondValue.TabIndex = 11;
            this.nudSecondValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // nudFirstValue
            // 
            this.nudFirstValue.Location = new System.Drawing.Point(99, 62);
            this.nudFirstValue.Name = "nudFirstValue";
            this.nudFirstValue.Size = new System.Drawing.Size(43, 20);
            this.nudFirstValue.TabIndex = 10;
            this.nudFirstValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Numbers";
            // 
            // tbClassName
            // 
            this.tbClassName.Enabled = false;
            this.tbClassName.Location = new System.Drawing.Point(99, 13);
            this.tbClassName.Name = "tbClassName";
            this.tbClassName.Size = new System.Drawing.Size(100, 20);
            this.tbClassName.TabIndex = 8;
            this.tbClassName.Text = "HelloWorld";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Java Class Name";
            // 
            // frmJNITestHarness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 177);
            this.Controls.Add(this.gbUserDeatils);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btJavaVersion);
            this.Controls.Add(this.btJavaFunction);
            this.Controls.Add(this.btJavaProcedure);
            this.Controls.Add(this.btLoadJVM);
            this.Name = "frmJNITestHarness";
            this.Text = "JNI Test Harness";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbUserDeatils.ResumeLayout(false);
            this.gbUserDeatils.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSecondValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLoadJVM;
        private System.Windows.Forms.Button btJavaProcedure;
        private System.Windows.Forms.Button btJavaFunction;
        private System.Windows.Forms.Button btJavaVersion;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.GroupBox gbUserDeatils;
        private System.Windows.Forms.TextBox tbClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudSecondValue;
        private System.Windows.Forms.NumericUpDown nudFirstValue;
        private System.Windows.Forms.Label label3;
    }
}

