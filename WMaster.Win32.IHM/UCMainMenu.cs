using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMaster.Win32
{
    public partial class UCMainMenu : UserControl
    {
        public UCMainMenu()
        {
            this.InitializeComponent();
            this.InitButtonImage();
            if (Game.MainMenu.CanContinueGame())
            {
                this.pbxContinue.Image = this.IliButtonMenu.Images["ContinueOff"];
                this.pbxContinue.MouseEnter += pbxContinue_MouseEnter;
                this.pbxContinue.MouseLeave += pbxContinue_MouseLeave;
                this.pbxContinue.Click += pbxContinue_Click;
            }
            else
            { this.pbxContinue.Image = this.IliButtonMenu.Images["ContinueDisabled"]; }

            if (Game.MainMenu.CanLoadGame())
            {
                this.pbxLoadGame.Image = this.IliButtonMenu.Images["LoadGameOff"];
                this.pbxLoadGame.MouseEnter += pbxLoadGame_MouseEnter;
                this.pbxLoadGame.MouseLeave += pbxLoadGame_MouseLeave;
                this.pbxLoadGame.Click += pbxLoadGame_Click;
            }
            else
            { this.pbxLoadGame.Image = this.IliButtonMenu.Images["LoadGameDisabled"]; }

            if (Game.MainMenu.CanCreateGame())
            {
                this.pbxNewGame.Image = this.IliButtonMenu.Images["NewGameOff"];
                this.pbxNewGame.MouseEnter += pbxNewGame_MouseEnter;
                this.pbxNewGame.MouseLeave += pbxNewGame_MouseLeave;
                this.pbxNewGame.Click += pbxNewGame_Click;
            }
            else
            { this.pbxNewGame.Image = this.IliButtonMenu.Images["NewGameDisabled"]; }

                this.pbxSettings.Image = this.IliButtonMenu.Images["SettingsOff"];
                this.pbxSettings.MouseEnter += pbxSettings_MouseEnter;
                this.pbxSettings.MouseLeave += pbxSettings_MouseLeave;
                this.pbxSettings.Click += pbxSettings_Click;

            if (Game.MainMenu.CanQuitGame())
            {
                this.pbxQuit.Image = this.IliButtonMenu.Images["QuitGameOff"];
                this.pbxQuit.MouseEnter += pbxQuit_MouseEnter;
                this.pbxQuit.MouseLeave += pbxQuit_MouseLeave;
                this.pbxQuit.Click += pbxQuit_Click;
            }
            else
            { this.pbxQuit.Image = this.IliButtonMenu.Images["QuitGameDisabled"]; }
        }

        private void InitButtonImage()
        {
            this.IliButtonMenu.Images.Clear();
            this.IliButtonMenu.Images.Add("ContinueDisabled", new Bitmap(@"Resources\Interface\Buttons\ContinueDisabled.png"));
            this.IliButtonMenu.Images.Add("ContinueOff", new Bitmap(@"Resources\Interface\Buttons\ContinueOff.png"));
            this.IliButtonMenu.Images.Add("ContinueOn", new Bitmap(@"Resources\Interface\Buttons\ContinueOn.png"));
            this.IliButtonMenu.Images.Add("LoadGameDisabled", new Bitmap(@"Resources\Interface\Buttons\LoadGameDisabled.png"));
            this.IliButtonMenu.Images.Add("LoadGameOff", new Bitmap(@"Resources\Interface\Buttons\LoadGameOff.png"));
            this.IliButtonMenu.Images.Add("LoadGameOn", new Bitmap(@"Resources\Interface\Buttons\LoadGameOn.png"));
            this.IliButtonMenu.Images.Add("NewGameDisabled", new Bitmap(@"Resources\Interface\Buttons\NewGameDisabled.png"));
            this.IliButtonMenu.Images.Add("NewGameOff", new Bitmap(@"Resources\Interface\Buttons\NewGameOff.png"));
            this.IliButtonMenu.Images.Add("NewGameOn", new Bitmap(@"Resources\Interface\Buttons\NewGameOn.png"));
            this.IliButtonMenu.Images.Add("QuitGameDisabled", new Bitmap(@"Resources\Interface\Buttons\QuitGameDisabled.png"));
            this.IliButtonMenu.Images.Add("QuitGameOff", new Bitmap(@"Resources\Interface\Buttons\QuitGameOff.png"));
            this.IliButtonMenu.Images.Add("QuitGameOn", new Bitmap(@"Resources\Interface\Buttons\QuitGameOn.png"));
            this.IliButtonMenu.Images.Add("SettingsDisabled", new Bitmap(@"Resources\Interface\Buttons\SettingsDisabled.png"));
            this.IliButtonMenu.Images.Add("SettingsOff", new Bitmap(@"Resources\Interface\Buttons\SettingsOff.png"));
            this.IliButtonMenu.Images.Add("SettingsOn", new Bitmap(@"Resources\Interface\Buttons\SettingsOn.png"));
        }

        #region Click event
        private void pbxNewGame_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pbxLoadGame_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pbxContinue_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pbxSettings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pbxQuit_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Mouse over event
        private void pbxContinue_MouseLeave(object sender, EventArgs e)
        { this.pbxContinue.Image = this.IliButtonMenu.Images["ContinueOff"]; }

        private void pbxContinue_MouseEnter(object sender, EventArgs e)
        { this.pbxContinue.Image = this.IliButtonMenu.Images["ContinueOn"]; }

        private void pbxLoadGame_MouseLeave(object sender, EventArgs e)
        { this.pbxLoadGame.Image = this.IliButtonMenu.Images["LoadGameOff"]; }

        private void pbxLoadGame_MouseEnter(object sender, EventArgs e)
        { this.pbxLoadGame.Image = this.IliButtonMenu.Images["LoadGameOn"]; }

        private void pbxNewGame_MouseLeave(object sender, EventArgs e)
        { this.pbxNewGame.Image = this.IliButtonMenu.Images["NewGameOff"]; }

        private void pbxNewGame_MouseEnter(object sender, EventArgs e)
        { this.pbxNewGame.Image = this.IliButtonMenu.Images["NewGameOn"]; }

        private void pbxSettings_MouseLeave(object sender, EventArgs e)
        { this.pbxSettings.Image = this.IliButtonMenu.Images["SettingsOff"]; }

        private void pbxSettings_MouseEnter(object sender, EventArgs e)
        { this.pbxSettings.Image = this.IliButtonMenu.Images["SettingsOn"]; }

        private void pbxQuit_MouseLeave(object sender, EventArgs e)
        { this.pbxQuit.Image = this.IliButtonMenu.Images["QuitGameOff"]; }

        private void pbxQuit_MouseEnter(object sender, EventArgs e)
        { this.pbxQuit.Image = this.IliButtonMenu.Images["QuitGameOn"]; }
        #endregion
    }
}
