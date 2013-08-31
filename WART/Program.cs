﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WART
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);

        public static bool UseUI = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool forceUI = CheckForceUI();

            if (!forceUI)
            {
                UseUI = !Attach();
            }

            if (UseUI)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
            Context c = new Context();
            c.Run();

            //exit
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
        }

        private static bool CheckForceUI()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length >= 2 && args[1] == "ui")
            {
                return true;
            }
            return false;
        }

        static bool Attach()
        {
            return AttachConsole(-1);
        }
    }
}
