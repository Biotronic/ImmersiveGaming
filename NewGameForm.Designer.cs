namespace ImmersiveGaming
{
    partial class NewGameForm
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
            this.chkWindowTitle = new System.Windows.Forms.CheckBox();
            this.chkClassName = new System.Windows.Forms.CheckBox();
            this.chkFileName = new System.Windows.Forms.CheckBox();
            this.txtWindowTitle = new System.Windows.Forms.TextBox();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmbWindowTitle = new System.Windows.Forms.ComboBox();
            this.cmbClassName = new System.Windows.Forms.ComboBox();
            this.cmbFileName = new System.Windows.Forms.ComboBox();
            this.displayChooser1 = new ImmersiveGaming.DisplayChooser();
            this.lblMonitors = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkHideCursor = new System.Windows.Forms.CheckBox();
            this.chkBlackout = new System.Windows.Forms.CheckBox();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkWindowTitle
            // 
            this.chkWindowTitle.AutoSize = true;
            this.chkWindowTitle.Checked = true;
            this.chkWindowTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowTitle.Location = new System.Drawing.Point(12, 9);
            this.chkWindowTitle.Name = "chkWindowTitle";
            this.chkWindowTitle.Size = new System.Drawing.Size(91, 17);
            this.chkWindowTitle.TabIndex = 0;
            this.chkWindowTitle.Text = "Window Title:";
            this.chkWindowTitle.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // chkClassName
            // 
            this.chkClassName.AutoSize = true;
            this.chkClassName.Checked = true;
            this.chkClassName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClassName.Location = new System.Drawing.Point(12, 35);
            this.chkClassName.Name = "chkClassName";
            this.chkClassName.Size = new System.Drawing.Size(85, 17);
            this.chkClassName.TabIndex = 1;
            this.chkClassName.Text = "Class Name:";
            this.chkClassName.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // chkFileName
            // 
            this.chkFileName.AutoSize = true;
            this.chkFileName.Checked = true;
            this.chkFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileName.Location = new System.Drawing.Point(12, 61);
            this.chkFileName.Name = "chkFileName";
            this.chkFileName.Size = new System.Drawing.Size(76, 17);
            this.chkFileName.TabIndex = 5;
            this.chkFileName.Text = "File Name:";
            this.chkFileName.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // txtWindowTitle
            // 
            this.txtWindowTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowTitle.Location = new System.Drawing.Point(195, 7);
            this.txtWindowTitle.Name = "txtWindowTitle";
            this.txtWindowTitle.Size = new System.Drawing.Size(269, 20);
            this.txtWindowTitle.TabIndex = 2;
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassName.Location = new System.Drawing.Point(195, 33);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(269, 20);
            this.txtClassName.TabIndex = 3;
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(195, 59);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(269, 20);
            this.txtFileName.TabIndex = 6;
            // 
            // cmbWindowTitle
            // 
            this.cmbWindowTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWindowTitle.FormattingEnabled = true;
            this.cmbWindowTitle.Items.AddRange(new object[] {
            "Equals",
            "Starts with",
            "Ends with",
            "Contains",
            "Regex"});
            this.cmbWindowTitle.Location = new System.Drawing.Point(109, 7);
            this.cmbWindowTitle.Name = "cmbWindowTitle";
            this.cmbWindowTitle.Size = new System.Drawing.Size(80, 21);
            this.cmbWindowTitle.TabIndex = 7;
            // 
            // cmbClassName
            // 
            this.cmbClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassName.FormattingEnabled = true;
            this.cmbClassName.Items.AddRange(new object[] {
            "Equals",
            "Starts with",
            "Ends with",
            "Contains",
            "Regex"});
            this.cmbClassName.Location = new System.Drawing.Point(109, 33);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(80, 21);
            this.cmbClassName.TabIndex = 8;
            // 
            // cmbFileName
            // 
            this.cmbFileName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileName.FormattingEnabled = true;
            this.cmbFileName.Items.AddRange(new object[] {
            "Equals",
            "Starts with",
            "Ends with",
            "Contains",
            "Regex"});
            this.cmbFileName.Location = new System.Drawing.Point(109, 59);
            this.cmbFileName.Name = "cmbFileName";
            this.cmbFileName.Size = new System.Drawing.Size(80, 21);
            this.cmbFileName.TabIndex = 9;
            // 
            // displayChooser1
            // 
            this.displayChooser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayChooser1.Location = new System.Drawing.Point(12, 106);
            this.displayChooser1.Name = "displayChooser1";
            this.displayChooser1.Size = new System.Drawing.Size(452, 234);
            this.displayChooser1.TabIndex = 10;
            // 
            // lblMonitors
            // 
            this.lblMonitors.AutoSize = true;
            this.lblMonitors.Location = new System.Drawing.Point(12, 81);
            this.lblMonitors.Name = "lblMonitors";
            this.lblMonitors.Size = new System.Drawing.Size(50, 13);
            this.lblMonitors.TabIndex = 11;
            this.lblMonitors.Text = "Monitors:";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkHideCursor);
            this.grpOptions.Controls.Add(this.chkBlackout);
            this.grpOptions.Location = new System.Drawing.Point(15, 346);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(449, 189);
            this.grpOptions.TabIndex = 12;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkHideCursor
            // 
            this.chkHideCursor.AutoSize = true;
            this.chkHideCursor.Location = new System.Drawing.Point(8, 42);
            this.chkHideCursor.Name = "chkHideCursor";
            this.chkHideCursor.Size = new System.Drawing.Size(114, 17);
            this.chkHideCursor.TabIndex = 1;
            this.chkHideCursor.Text = "Hide mouse cursor";
            this.chkHideCursor.UseVisualStyleBackColor = true;
            // 
            // chkBlackout
            // 
            this.chkBlackout.AutoSize = true;
            this.chkBlackout.Location = new System.Drawing.Point(8, 19);
            this.chkBlackout.Name = "chkBlackout";
            this.chkBlackout.Size = new System.Drawing.Size(151, 17);
            this.chkBlackout.TabIndex = 0;
            this.chkBlackout.Text = "Black out unused monitors";
            this.chkBlackout.UseVisualStyleBackColor = true;
            // 
            // NewGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 644);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.lblMonitors);
            this.Controls.Add(this.displayChooser1);
            this.Controls.Add(this.cmbFileName);
            this.Controls.Add(this.cmbClassName);
            this.Controls.Add(this.cmbWindowTitle);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.txtWindowTitle);
            this.Controls.Add(this.chkFileName);
            this.Controls.Add(this.chkClassName);
            this.Controls.Add(this.chkWindowTitle);
            this.Name = "NewGameForm";
            this.Text = "NewGameForm";
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkWindowTitle;
        private System.Windows.Forms.CheckBox chkClassName;
        private System.Windows.Forms.CheckBox chkFileName;
        private System.Windows.Forms.TextBox txtWindowTitle;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ComboBox cmbWindowTitle;
        private System.Windows.Forms.ComboBox cmbClassName;
        private System.Windows.Forms.ComboBox cmbFileName;
        private DisplayChooser displayChooser1;
        private System.Windows.Forms.Label lblMonitors;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkHideCursor;
        private System.Windows.Forms.CheckBox chkBlackout;
    }
}