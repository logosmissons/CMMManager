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

            frmCMMManager frmMainCMMManager = new frmCMMManager();

            frmLogin frmLogin = new frmLogin();
            frmLogin.StartPosition = FormStartPosition.CenterParent;

            //Boolean bLoginSuccess = false;

            for (int i = 0; i < 3; i++)
            {
                DialogResult loginResult = frmLogin.ShowDialog();

                if (loginResult == DialogResult.OK)
                {
                    //bLoginSuccess = true;

                    frmMainCMMManager.nLoggedUserId = frmLogin.nLoggedUserId;
                    frmMainCMMManager.LoggedInUserName = frmLogin.LoggedInUserName;
                    frmMainCMMManager.LoggedInUserRole = frmLogin.nLoggedUserRole;
                    frmMainCMMManager.LoggedInUserDepartment = frmLogin.nLoggedInUserDepartmentId;
                    frmMainCMMManager.bLoginSuccess = true;

                    Application.Run(frmMainCMMManager);

                    //nLoggedUserId = frmLogin.nLoggedUserId;
                    //LoggedInUserName = frmLogin.LoggedInUserName;
                    //LoggedInUserRole = frmLogin.nLoggedUserRole;
                    //LoggedInUserDepartment = frmLogin.nLoggedInUserDepartmentId;

                    break;
                }
                else if (loginResult == DialogResult.Cancel)
                {
                    MessageBox.Show("Login Canceled", "Alert");
                    break;
                }
                else if (loginResult == DialogResult.Retry)
                {
                    continue;
                }
            }

            //if (bLoginSuccess == false) Close();
            //Application.Run(new frmCMMManager());
        }
    }
}
