namespace WMaster.Win32.IHM
{
    using System;
    using System.Windows.Forms;

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
            Application.Run(new Form1());
        }

        static void Initialize()
        {
            // Set Win32 log instance to log manager.
            WMLog.InitialiseLog(WMaster.Win32.Diagnostics.Log.LogInstance);
            Win32.Diagnostics.Log.LogInstance.Switch = WMLog.LogSwitch.INFORMATION;
        }
    }
}
