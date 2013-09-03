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
        internal static GPLCUser user = new GPLCUser();
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

        /* check if current user has the authority to do operation
         * return true or false
         * */
        public static bool AuthVerify(GPLCAuthority auth)
        {
            try
            {
                Auth(auth);
                return true;
            }
            catch (UnauthorizedException)
            {
                return false;
            }
        }
        /* check if current user has the authority to do operation
         * throw exception when current user has no enough authority
         * */
        public static void Auth(GPLCAuthority auth)
        {
            user.Authenticate(auth);
        }
    }
}
