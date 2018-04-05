namespace capf17g5project1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timerKeyPress = new System.Windows.Forms.Timer(this.components);
            this.timerLog = new System.Windows.Forms.Timer(this.components);
            this.notifyPopup = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnUpload = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerProcessActivity = new System.Windows.Forms.Timer(this.components);
            this.btnUploadToOSF = new System.Windows.Forms.Button();
            this.lblOSFuploadStatus = new System.Windows.Forms.Label();
            this.pBarUploadStatus = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(172, 42);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START RECORDING";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(194, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(63, 42);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timerKeyPress
            // 
            this.timerKeyPress.Interval = 10;
            this.timerKeyPress.Tick += new System.EventHandler(this.timerKeyPress_Tick);
            // 
            // timerLog
            // 
            this.timerLog.Interval = 6000;
            this.timerLog.Tick += new System.EventHandler(this.timerLog_Tick);
            // 
            // notifyPopup
            // 
            this.notifyPopup.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyPopup.BalloonTipText = "Application has started";
            this.notifyPopup.BalloonTipTitle = "KEY PRESS LOGGER";
            this.notifyPopup.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyPopup.Icon")));
            this.notifyPopup.Text = "Double-click to maximize";
            this.notifyPopup.Visible = true;
            this.notifyPopup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyPopup_MouseDoubleClick);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(12, 89);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(115, 36);
            this.btnUpload.TabIndex = 5;
            this.btnUpload.Text = "Upload to AWS (S3)";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 129);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: Idle";
            // 
            // timerProcessActivity
            // 
            this.timerProcessActivity.Interval = 3000;
            this.timerProcessActivity.Tick += new System.EventHandler(this.timerProcessActivity_Tick);
            // 
            // btnUploadToOSF
            // 
            this.btnUploadToOSF.Location = new System.Drawing.Point(11, 150);
            this.btnUploadToOSF.Margin = new System.Windows.Forms.Padding(2);
            this.btnUploadToOSF.Name = "btnUploadToOSF";
            this.btnUploadToOSF.Size = new System.Drawing.Size(116, 36);
            this.btnUploadToOSF.TabIndex = 7;
            this.btnUploadToOSF.Text = "Upload to OSF";
            this.btnUploadToOSF.UseVisualStyleBackColor = true;
            this.btnUploadToOSF.Click += new System.EventHandler(this.btnUploadToOSF_Click);
            // 
            // lblOSFuploadStatus
            // 
            this.lblOSFuploadStatus.AutoSize = true;
            this.lblOSFuploadStatus.Location = new System.Drawing.Point(11, 192);
            this.lblOSFuploadStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOSFuploadStatus.Name = "lblOSFuploadStatus";
            this.lblOSFuploadStatus.Size = new System.Drawing.Size(60, 13);
            this.lblOSFuploadStatus.TabIndex = 8;
            this.lblOSFuploadStatus.Text = "Status: Idle";
            // 
            // pBarUploadStatus
            // 
            this.pBarUploadStatus.Location = new System.Drawing.Point(15, 224);
            this.pBarUploadStatus.Name = "pBarUploadStatus";
            this.pBarUploadStatus.Size = new System.Drawing.Size(211, 23);
            this.pBarUploadStatus.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 266);
            this.Controls.Add(this.pBarUploadStatus);
            this.Controls.Add(this.lblOSFuploadStatus);
            this.Controls.Add(this.btnUploadToOSF);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timerKeyPress;
        private System.Windows.Forms.Timer timerLog;
        private System.Windows.Forms.NotifyIcon notifyPopup;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerProcessActivity;
        private System.Windows.Forms.Button btnUploadToOSF;
        private System.Windows.Forms.Label lblOSFuploadStatus;
        private System.Windows.Forms.ProgressBar pBarUploadStatus;
    }
}

