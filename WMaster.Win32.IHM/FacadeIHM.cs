namespace WMaster.Win32
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WMaster.Tool;

    /// <summary>
    /// Facade providing all IHM dependant functionality need by game engine.
    /// </summary>
    public class FacadeIHM : IFacadeIHM
    {
        #region Singleton
        /// <summary>
        /// <see cref="FacadeOD"/> unique instance.
        /// </summary>
        private static FacadeIHM _instance;
        /// <summary>
        /// Fet the <see cref="FacadeOS"/> singleton.
        /// </summary>
        public static FacadeIHM Entry
        {
            get
            {
                if (FacadeIHM._instance == null)
                { FacadeIHM._instance = new FacadeIHM(); }
                return FacadeIHM._instance;
            }
        }
        /// <summary>
        /// Private constructore of <see cref="FacadeIHM"/> for singleton template.
        /// </summary>
        private FacadeIHM()
        {
        }
        #endregion
    }
}
