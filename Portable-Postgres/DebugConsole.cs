using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Portable_Postgres
{
    public partial class DebugConsole : Form
    {
        #region "Variables"
        public Debugger debugger;
        public string baseDirectory;
        #endregion
        #region "Methods - Constructors"
        public DebugConsole(Debugger debugger, string baseDirectory)
        {
            InitializeComponent();
            this.debugger = debugger;
            this.baseDirectory = baseDirectory;
        }
        #endregion
        #region "Methods - Event Handlers"
        void debugger_NewMessage(DebugMessage msg)
        {
            try
            {
                Invoke((MethodInvoker)delegate()
                {
                    txtConsole.Text += msg + "\r\n\r\n";
                    scrollToBottom();
                });
            }
            catch { }
        }
        private void DebugConsole_Load(object sender, EventArgs e)
        {
            debugger.NewMessage += new Debugger.MessageEvent(debugger_NewMessage);
            txtConsole.Text += debugger;
        }
        private void DebugConsole_Shown(object sender, EventArgs e)
        {
            scrollToBottom();
        }
        private void buttCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtConsole.Text);
        }
        private void buttDump_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(baseDirectory + "\\debug.txt", txtConsole.Text);
                debugger.write("Successfully wrote debug data to '" + baseDirectory + "\\debug.txt'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to write data to '" + baseDirectory + "\\debug.txt' - " + ex.Message + "!", "Error");
                debugger.write("Failed to write debug data to '" + baseDirectory + "\\debug.txt'");
            }
            try
            {
                // Selects the debug.txt file in explorer
                Process.Start("explorer.exe", "/select , \"" + baseDirectory + "\\debug.txt\"");
            }
            catch { }
        }
        private void buttClear_Click(object sender, EventArgs e)
        {
            txtConsole.Text = string.Empty;
            debugger.clear();
        }
        #endregion
        #region "Methods"
        void scrollToBottom()
        {
            Invoke((MethodInvoker)delegate()
            {
                txtConsole.Focus();
                txtConsole.SelectionStart = txtConsole.TextLength;
                txtConsole.ScrollToCaret();
            });
        }
        #endregion
    }
}
