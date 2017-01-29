namespace WMaster.Win32
{
    partial class UCMainMenu
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbxContinue = new System.Windows.Forms.PictureBox();
            this.pbxLoadGame = new System.Windows.Forms.PictureBox();
            this.pbxNewGame = new System.Windows.Forms.PictureBox();
            this.pbxSettings = new System.Windows.Forms.PictureBox();
            this.pbxQuit = new System.Windows.Forms.PictureBox();
            this.IliButtonMenu = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxContinue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoadGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNewGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQuit)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxContinue
            // 
            this.pbxContinue.BackColor = System.Drawing.Color.Transparent;
            this.pbxContinue.Location = new System.Drawing.Point(31, 31);
            this.pbxContinue.Name = "pbxContinue";
            this.pbxContinue.Size = new System.Drawing.Size(256, 64);
            this.pbxContinue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxContinue.TabIndex = 0;
            this.pbxContinue.TabStop = false;
            // 
            // pbxLoadGame
            // 
            this.pbxLoadGame.BackColor = System.Drawing.Color.Transparent;
            this.pbxLoadGame.Location = new System.Drawing.Point(31, 127);
            this.pbxLoadGame.Name = "pbxLoadGame";
            this.pbxLoadGame.Size = new System.Drawing.Size(256, 64);
            this.pbxLoadGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxLoadGame.TabIndex = 1;
            this.pbxLoadGame.TabStop = false;
            // 
            // pbxNewGame
            // 
            this.pbxNewGame.BackColor = System.Drawing.Color.Transparent;
            this.pbxNewGame.Location = new System.Drawing.Point(31, 223);
            this.pbxNewGame.Name = "pbxNewGame";
            this.pbxNewGame.Size = new System.Drawing.Size(256, 64);
            this.pbxNewGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxNewGame.TabIndex = 2;
            this.pbxNewGame.TabStop = false;
            // 
            // pbxSettings
            // 
            this.pbxSettings.BackColor = System.Drawing.Color.Transparent;
            this.pbxSettings.Location = new System.Drawing.Point(31, 319);
            this.pbxSettings.Name = "pbxSettings";
            this.pbxSettings.Size = new System.Drawing.Size(256, 64);
            this.pbxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxSettings.TabIndex = 3;
            this.pbxSettings.TabStop = false;
            // 
            // pbxQuit
            // 
            this.pbxQuit.BackColor = System.Drawing.Color.Transparent;
            this.pbxQuit.Location = new System.Drawing.Point(31, 415);
            this.pbxQuit.Name = "pbxQuit";
            this.pbxQuit.Size = new System.Drawing.Size(256, 64);
            this.pbxQuit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxQuit.TabIndex = 4;
            this.pbxQuit.TabStop = false;
            // 
            // IliButtonMenu
            // 
            this.IliButtonMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.IliButtonMenu.ImageSize = new System.Drawing.Size(256, 64);
            this.IliButtonMenu.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // UCMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(191)))), ((int)(((byte)(228)))));
            this.Controls.Add(this.pbxQuit);
            this.Controls.Add(this.pbxSettings);
            this.Controls.Add(this.pbxNewGame);
            this.Controls.Add(this.pbxLoadGame);
            this.Controls.Add(this.pbxContinue);
            this.MaximumSize = new System.Drawing.Size(320, 512);
            this.MinimumSize = new System.Drawing.Size(320, 512);
            this.Name = "UCMainMenu";
            this.Size = new System.Drawing.Size(320, 512);
            ((System.ComponentModel.ISupportInitialize)(this.pbxContinue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLoadGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNewGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQuit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxContinue;
        private System.Windows.Forms.PictureBox pbxLoadGame;
        private System.Windows.Forms.PictureBox pbxNewGame;
        private System.Windows.Forms.PictureBox pbxSettings;
        private System.Windows.Forms.PictureBox pbxQuit;
        private System.Windows.Forms.ImageList IliButtonMenu;
    }
}
