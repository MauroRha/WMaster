namespace WMaster.Win32.IHM
{
    using System;
    using System.Windows.Forms;
    using WMaster.Win32.Diagnostics;

    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FrmMain.Instance);
        }

        static void Initialize()
        {
            // Set Win32 log instance switch to log information.
            Log.LogInstance.Switch = WMLog.LogSwitch.INFORMATION;
            Game.GameInitialize(FacadeOS.Entry, FacadeIHM.Entry, Log.LogInstance);
        }
    }
}
