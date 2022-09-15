using LoginScreen;
using Shopping_Mart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_System
{
    static class Program
    {
        public static Form1 login;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DashBoard());
            login = new Form1();
            Application.Run(Program.login);
        }
    }
}
