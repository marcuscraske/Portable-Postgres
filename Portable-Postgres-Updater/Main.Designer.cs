namespace Portable_Postgres_Updater
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.txtLabelStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtLabelStatus
            // 
            this.txtLabelStatus.AutoSize = true;
            this.txtLabelStatus.BackColor = System.Drawing.Color.Transparent;
            this.txtLabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabelStatus.ForeColor = System.Drawing.Color.White;
            this.txtLabelStatus.Location = new System.Drawing.Point(12, 9);
            this.txtLabelStatus.Name = "txtLabelStatus";
            this.txtLabelStatus.Size = new System.Drawing.Size(51, 16);
            this.txtLabelStatus.TabIndex = 9;
            this.txtLabelStatus.Text = "Status";
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.BackColor = System.Drawing.Color.Transparent;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(12, 25);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(27, 13);
            this.txtStatus.TabIndex = 11;
            this.txtStatus.Text = "Idle.";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(416, 23);
            this.progressBar.TabIndex = 12;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Portable_Postgres_Updater.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(440, 87);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtLabelStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater - Portable Postgres - UberMeat.co.uk FOSS";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtLabelStatus;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

