namespace SW.DatabaseCheckout.Client
{
    partial class WinFormsClient
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormsClient));
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.timerProcessWatcher = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// trayIcon
			// 
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "trayIcon";
			this.trayIcon.Visible = true;
			// 
			// timerProcessWatcher
			// 
			this.timerProcessWatcher.Enabled = true;
			this.timerProcessWatcher.Interval = 1000;
			this.timerProcessWatcher.Tick += new System.EventHandler(this.timerProcessWatcher_Tick);
			// 
			// WinFormsClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 61);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(310, 100);
			this.Name = "WinFormsClient";
			this.ShowInTaskbar = false;
			this.Text = "WinForms SignalR Client";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFormsClient_FormClosing);
			this.Load += new System.EventHandler(this.WinFormsClient_Load);
			this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.Timer timerProcessWatcher;
	}
}

