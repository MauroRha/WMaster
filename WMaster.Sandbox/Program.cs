namespace WMaster.Sandbox
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using WMaster;

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
            Application.Run(new FrmMain());
        }

        static void Initialize()
        {
            
        }
    }
}
