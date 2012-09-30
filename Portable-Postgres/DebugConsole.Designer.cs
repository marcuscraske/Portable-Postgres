namespace Portable_Postgres
{
    partial class DebugConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugConsole));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttDump = new System.Windows.Forms.Button();
            this.buttCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 3);
            this.panel2.TabIndex = 12;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(0, 38);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(616, 606);
            this.txtConsole.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Portable_Postgres.Properties.Resources.background;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.buttClear);
            this.panel1.Controls.Add(this.buttDump);
            this.panel1.Controls.Add(this.buttCopy);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 35);
            this.panel1.TabIndex = 11;
            // 
            // buttDump
            // 
            this.buttDump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttDump.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttDump.FlatAppearance.BorderSize = 0;
            this.buttDump.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttDump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttDump.Location = new System.Drawing.Point(250, 6);
            this.buttDump.Name = "buttDump";
            this.buttDump.Size = new System.Drawing.Size(119, 23);
            this.buttDump.TabIndex = 8;
            this.buttDump.Text = "Dump to debug.txt";
            this.buttDump.UseVisualStyleBackColor = false;
            this.buttDump.Click += new System.EventHandler(this.buttDump_Click);
            // 
            // buttCopy
            // 
            this.buttCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttCopy.FlatAppearance.BorderSize = 0;
            this.buttCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttCopy.Location = new System.Drawing.Point(137, 6);
            this.buttCopy.Name = "buttCopy";
            this.buttCopy.Size = new System.Drawing.Size(107, 23);
            this.buttCopy.TabIndex = 7;
            this.buttCopy.Text = "Copy to Clipboard";
            this.buttCopy.UseVisualStyleBackColor = false;
            this.buttCopy.Click += new System.EventHandler(this.buttCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Debug Console";
            // 
            // buttClear
            // 
            this.buttClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttClear.FlatAppearance.BorderSize = 0;
            this.buttClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttClear.Location = new System.Drawing.Point(375, 6);
            this.buttClear.Name = "buttClear";
            this.buttClear.Size = new System.Drawing.Size(92, 23);
            this.buttClear.TabIndex = 9;
            this.buttClear.Text = "Clear Console";
            this.buttClear.UseVisualStyleBackColor = false;
            this.buttClear.Click += new System.EventHandler(this.buttClear_Click);
            // 
            // DebugConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(616, 644);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebugConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debug Console - Portable Postgres - Ubermeat.co.uk FOSS";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DebugConsole_Load);
            this.Shown += new System.EventHandler(this.DebugConsole_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button buttDump;
        private System.Windows.Forms.Button buttCopy;
        private System.Windows.Forms.Button buttClear;
    }
}