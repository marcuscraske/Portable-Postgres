/*
 * Creative Commons Attribution-ShareAlike 3.0 unported
 * ***************************************************************
 * Author:  limpygnome
 * E-mail:  limpygnome@gmail.com
 * Site:    ubermeat.co.uk
 * ***************************************************************
 * Credit to:
 * -- none
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Portable_Postgres
{
    public partial class HelpWindow : Form
    {
        #region "Constants"
        private const string helpURL = "http://www.ubermeat.co.uk/mirror/PortablePostgres/Help.html";
        #endregion

        #region "Constructor"
        public HelpWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region "Methods - Events"
        /// <summary>
        /// Invoked when the form is shown; this causes the browser to navigate to the helpURL constant.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpWindow_Shown(object sender, EventArgs e)
        {
            visitHelpPage();
        }
        /// <summary>
        /// Invoked when the browser has navigated/loaded a new page; this is used to assign the URL of the new page to the textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri == helpURL)
            {
                string html = webBrowser.DocumentText;
                webBrowser.Document.Write(string.Empty);
                webBrowser.Document.Write(html);
            }
            else
                tbURL.Text = e.Url.AbsoluteUri;
        }
        /// <summary>
        /// Invoked when the user clicks the refresh button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttRefresh_Click(object sender, EventArgs e)
        {
            visitHelpPage();
        }
        /// <summary>
        /// Launches the help page in the users browser by passing the URL to the Windows shell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttView_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(helpURL);
        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Used to navigate to the help page defined by the constant helpURL in the web browser.
        /// </summary>
        void visitHelpPage()
        {
            webBrowser.Navigate(helpURL);
        }
        #endregion
    }
}