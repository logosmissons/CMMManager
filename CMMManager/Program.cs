using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMMManager
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
            //frmCMMLogin login = new frmCMMLogin();
            //if (login.ShowDialog() == DialogResult.OK) Application.Run(new frmCMMManager());
            //else return;
            Application.Run(new frmCMMManager());
        }
    }
}
