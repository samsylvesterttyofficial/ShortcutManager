﻿using System;
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
        }


        private static bool getPrevInstance()
        {
            //get the name of current process, i,e the process 
            //name of this current application

            string currPrsName = Process.GetCurrentProcess().ProcessName;

            //Get the name of all processes having the 
            //same name as this process name 
            Process[] allProcessWithThisName
                         = Process.GetProcessesByName(currPrsName);

            //if more than one process is running return true.
            //which means already previous instance of the application 
            //is running
            if (allProcessWithThisName.Length > 1)
            {
                MessageBox.Show("Already Running", "Hot Key Mgr", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true; // Yes Previous Instance Exist
            }
            else
            {
                return false; //No Prev Instance Running
            }
        }

    }
}
