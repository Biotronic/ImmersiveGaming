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
            this.txtWindowTitle = new System.Windows.Forms.ComboBox();
            this.txtClassName = new System.Windows.Forms.ComboBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmbWindowTitle = new System.Windows.Forms.ComboBox();
            this.cmbClassName = new System.Windows.Forms.ComboBox();
            this.cmbFileName = new System.Windows.Forms.ComboBox();
            this.lblMonitors = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkHideCursor = new System.Windows.Forms.CheckBox();
            this.chkBlackout = new System.Windows.Forms.CheckBox();
            this.monitors = new ImmersiveGaming.DisplayChooser();
            this.btnCapture = new System.Windows.Forms.Button();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkWindowTitle
            // 
            this.chkWindowTitle.AutoSize = true;
            this.chkWindowTitle.Checked = true;
            this.chkWindowTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowTitle.Location = new System.Drawing.Point(63, 2);
            this.chkWindowTitle.Name = "chkWindowTitle";
            this.chkWindowTitle.Size = new System.Drawing.Size(91, 17);
            this.chkWindowTitle.TabIndex = 1;
            this.chkWindowTitle.Text = "Window Title:";
            this.chkWindowTitle.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // chkClassName
            // 
            this.chkClassName.AutoSize = true;
            this.chkClassName.Checked = true;
            this.chkClassName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClassName.Location = new System.Drawing.Point(63, 29);
            this.chkClassName.Name = "chkClassName";
            this.chkClassName.Size = new System.Drawing.Size(85, 17);
            this.chkClassName.TabIndex = 4;
            this.chkClassName.Text = "Class Name:";
            this.chkClassName.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // chkFileName
            // 
            this.chkFileName.AutoSize = true;
            this.chkFileName.Checked = true;
            this.chkFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileName.Location = new System.Drawing.Point(63, 55);
            this.chkFileName.Name = "chkFileName";
            this.chkFileName.Size = new System.Drawing.Size(76, 17);
            this.chkFileName.TabIndex = 7;
            this.chkFileName.Text = "File Name:";
            this.chkFileName.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // txtWindowTitle
            // 
            this.txtWindowTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowTitle.Location = new System.Drawing.Point(249, 0);
            this.txtWindowTitle.Name = "txtWindowTitle";
            this.txtWindowTitle.Size = new System.Drawing.Size(223, 21);
            this.txtWindowTitle.TabIndex = 3;
            this.txtWindowTitle.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassName.Location = new System.Drawing.Point(249, 27);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(223, 21);
            this.txtClassName.TabIndex = 6;
            this.txtClassName.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(249, 53);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(223, 20);
            this.txtFileName.TabIndex = 9;
            this.txtFileName.TextChanged += new System.EventHandler(this.InputChanged);
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
            this.cmbWindowTitle.Location = new System.Drawing.Point(159, 1);
            this.cmbWindowTitle.Name = "cmbWindowTitle";
            this.cmbWindowTitle.Size = new System.Drawing.Size(80, 21);
            this.cmbWindowTitle.TabIndex = 2;
            this.cmbWindowTitle.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
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
            this.cmbClassName.Location = new System.Drawing.Point(159, 27);
            this.cmbClassName.Name = "cmbClassName";
            this.cmbClassName.Size = new System.Drawing.Size(80, 21);
            this.cmbClassName.TabIndex = 5;
            this.cmbClassName.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
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
            this.cmbFileName.Location = new System.Drawing.Point(159, 53);
            this.cmbFileName.Name = "cmbFileName";
            this.cmbFileName.Size = new System.Drawing.Size(80, 21);
            this.cmbFileName.TabIndex = 8;
            this.cmbFileName.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblMonitors
            // 
            this.lblMonitors.AutoSize = true;
            this.lblMonitors.Location = new System.Drawing.Point(5, 86);
            this.lblMonitors.Name = "lblMonitors";
            this.lblMonitors.Size = new System.Drawing.Size(50, 13);
            this.lblMonitors.TabIndex = 11;
            this.lblMonitors.Text = "Monitors:";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkHideCursor);
            this.grpOptions.Controls.Add(this.chkBlackout);
            this.grpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpOptions.Location = new System.Drawing.Point(0, 430);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(475, 69);
            this.grpOptions.TabIndex = 11;
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
            this.chkHideCursor.CheckedChanged += new System.EventHandler(this.InputChanged);
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
            this.chkBlackout.CheckedChanged += new System.EventHandler(this.chkBlackout_CheckedChanged);
            // 
            // monitors
            // 
            this.monitors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monitors.Location = new System.Drawing.Point(0, 102);
            this.monitors.Name = "monitors";
            this.monitors.Size = new System.Drawing.Size(472, 322);
            this.monitors.TabIndex = 10;
            this.monitors.MonitorSelectedChanged += new System.EventHandler<ImmersiveGaming.MonitorEventArgs>(this.monitors_MonitorSelectedChanged);
            // 
            // btnCapture
            // 
            this.btnCapture.Image = global::ImmersiveGaming.Properties.Resources.Crosshairs;
            this.btnCapture.Location = new System.Drawing.Point(8, 0);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(47, 72);
            this.btnCapture.TabIndex = 12;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // NewGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.lblMonitors);
            this.Controls.Add(this.monitors);
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
            this.Size = new System.Drawing.Size(475, 499);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkWindowTitle;
        private System.Windows.Forms.CheckBox chkClassName;
        private System.Windows.Forms.CheckBox chkFileName;
        private System.Windows.Forms.ComboBox txtWindowTitle;
        private System.Windows.Forms.ComboBox txtClassName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ComboBox cmbWindowTitle;
        private System.Windows.Forms.ComboBox cmbClassName;
        private System.Windows.Forms.ComboBox cmbFileName;
        private DisplayChooser monitors;
        private System.Windows.Forms.Label lblMonitors;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkHideCursor;
        private System.Windows.Forms.CheckBox chkBlackout;
        private System.Windows.Forms.Button btnCapture;
    }
}