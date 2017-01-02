namespace WMaster.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using WMaster.Tool;

    /// <summary>
    /// Facade providing all OS dependant functionality need by game engine.
    /// </summary>
    public class FacadeOS : WMaster.Tool.IFacadeOS
    {
        #region Singleton
        /// <summary>
        /// <see cref="FacadeOD"/> unique instance.
        /// </summary>
        private static FacadeOS _instance;
        /// <summary>
        /// Fet the <see cref="FacadeOS"/> singleton.
        /// </summary>
        public static FacadeOS Entry
        {
            get
            {
                if (FacadeOS._instance == null)
                { FacadeOS._instance = new FacadeOS(); }
                return FacadeOS._instance;
            }
        }
        /// <summary>
        /// Private constructore of <see cref="FacadeOS"/> for singleton template.
        /// </summary>
        private FacadeOS()
        {
        }
        #endregion

        /// <summary>
        /// Return configuration data on <see cref="XElement"/> format.
        /// </summary>
        /// <returns>Configuration data on <see cref="XElement"/> format.</returns>
        public XElement GetConfiguration(string filename = "config.xml")
        {
            try
            {
                XDocument xd = XDocument.Load(filename);
                return xd.Root;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return null;
            }
        }
        /// <summary>
        /// Save configuration data from <see cref="XElement"/> source.
        /// </summary>
        /// <param name="configurationData"><see cref="XElement"/> of configuration data.</param>
            /// <param name="filename">Destination to store XML configuration data.</param>
    /// <returns><b>True</b> if save will be done correctly.</returns>
        public bool SetConfiguration(XElement configurationData, string filename = "config.xml")
        {
            try
            {
                XDocument xd = new XDocument();
                xd.Add(configurationData);
                xd.Save(filename);
                return true;
            }
            catch (Exception ex)
            {
                WMLog.Trace(ex, WMLog.TraceLog.ERROR);
                return false;
            }
        }

        public IResourceManager GetResourceManager()
        {
            throw new NotImplementedException();
        }
    }
}
