using MIRLE_GPLC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIRLE_GPLC
{
    static class GPLC
    {
        internal static GPLCUser user;
        private static MainForm mainForm;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            Application.Run(mainForm);
        }

        public static void Refresh()
        {
            mainForm.Refresh();
        }

        public static bool AuthVerify(GPLCAuthority auth)
        {
            try
            {
                user.Authenticate(auth);
                return true;
            }
            catch (UnauthorizedException)
            {
                return false;
            }
        }
        public static void Auth(GPLCAuthority auth)
        {
            user.Authenticate(auth);
        }
    }
}
