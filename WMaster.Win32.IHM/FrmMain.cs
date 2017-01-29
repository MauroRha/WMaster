using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMaster.Win32
{
    public partial class FrmMain : Form
    {
        #region Singleton
        /// <summary>
        /// Singleton of <see cref="FrmMain"/>.
        /// </summary>
        private static FrmMain m_Instance;

        /// <summary>
        /// Get unique instance of <see cref="FrmMain"/>.
        /// </summary>
        public static FrmMain Instance
        {
            get
            {
                if (FrmMain.m_Instance == null)
                { FrmMain.m_Instance = new FrmMain(); }
                return FrmMain.m_Instance;
            }
        }

        /// <summary>
        /// Privatec constructor for Singleton template.
        /// </summary>
        private FrmMain()
        {
            InitializeComponent();
            this.BackgroundImage = new Bitmap(@"D:\ProjetEnCour\WM\WhoreMaster\Resources\Images\background.jpg");
            this.ShomMenu();
        }
        #endregion

        public void ShomMenu()
        {
            UCMainMenu ucMenu = new UCMainMenu();
            this.Controls.Clear();
            
            ucMenu.Location = new Point((this.ClientSize.Width-ucMenu.Width) / 2, (this.ClientSize.Height-ucMenu.Height) / 2);
            this.Controls.Add(ucMenu);
        }
    }
}
