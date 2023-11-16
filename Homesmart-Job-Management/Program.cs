using System;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new frmHome());
            } catch (Exception ex) { }
        }
    }
}
