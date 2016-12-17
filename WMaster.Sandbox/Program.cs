namespace WMaster.Sandbox
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using WMaster;
    using WMaster.DAL;

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
            SaveGame sg = new SaveGame();
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Aaliyah Love.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abbey Brooks.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abbie Cat.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abby Cross.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abby Lee Brazil.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abella Anderson.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abigail Mac.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Abigaile Johnson.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Adele Stephens.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Adriana Chechik.girlsx" });
            sg.Resources.GirlResources.Add(new Resource() { Filename = "Adriana Malao.girlsx" });

            XmlSerializer xs = new XmlSerializer(typeof(SaveGame));
            using (StreamWriter wr = new StreamWriter("test.xml"))
            {
                xs.Serialize(wr, sg);
            }
        }
    }
}
