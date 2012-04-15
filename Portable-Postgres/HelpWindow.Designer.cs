namespace Portable_Postgres
{
    partial class HelpWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttView = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.buttRefresh = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.buttView);
            this.splitContainer1.Panel1.Controls.Add(this.tbURL);
            this.splitContainer1.Panel1.Controls.Add(this.buttRefresh);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(991, 530);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttView
            // 
            this.buttView.Location = new System.Drawing.Point(595, 6);
            this.buttView.Name = "buttView";
            this.buttView.Size = new System.Drawing.Size(102, 23);
            this.buttView.TabIndex = 0;
            this.buttView.Text = "View in Browser";
            this.buttView.UseVisualStyleBackColor = true;
            this.buttView.Click += new System.EventHandler(this.buttView_Click);
            // 
            // tbURL
            // 
            this.tbURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbURL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbURL.Location = new System.Drawing.Point(19, 6);
            this.tbURL.Name = "tbURL";
            this.tbURL.ReadOnly = true;
            this.tbURL.Size = new System.Drawing.Size(489, 21);
            this.tbURL.TabIndex = 0;
            this.tbURL.Text = "Loading...";
            // 
            // buttRefresh
            // 
            this.buttRefresh.Location = new System.Drawing.Point(514, 6);
            this.buttRefresh.Name = "buttRefresh";
            this.buttRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttRefresh.TabIndex = 1;
            this.buttRefresh.Text = "Refresh";
            this.buttRefresh.UseVisualStyleBackColor = true;
            this.buttRefresh.Click += new System.EventHandler(this.buttRefresh_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(991, 491);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser_Navigated);
            // 
            // HelpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(991, 530);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpWindow";
            this.Text = "Help - Portable Postgres - Ubermeat.co.uk FOSS";
            this.Shown += new System.EventHandler(this.HelpWindow_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button buttView;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button buttRefresh;
    }
}