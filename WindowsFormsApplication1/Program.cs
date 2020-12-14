using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotKeyMgr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            if (!getPrevInstance())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
                MessageBox.Show("Already Running", "Hot Key Mgr", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private static bool getPrevInstance()
        {
            //get the name of current process, i,e the process 
            //name of this current application
            // new change that introduced in TEM-33333

            string currPrsName = Process.GetCurrentProcess().ProcessName;

            //Get the name of all processes having the 
            //same name as this process name 
            Process[] allProcessWithThisName
                         = Process.GetProcessesByName(currPrsName);

            // Return true if the process is already runing. (More than 1)
            return allProcessWithThisName.Length > 1;            
        }

    }
}
