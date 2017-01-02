namespace WMaster.Sandbox
{
    partial class FrmMain
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCheckXMLSerialisation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckXMLSerialisation
            // 
            this.btnCheckXMLSerialisation.Location = new System.Drawing.Point(13, 13);
            this.btnCheckXMLSerialisation.Name = "btnCheckXMLSerialisation";
            this.btnCheckXMLSerialisation.Size = new System.Drawing.Size(187, 27);
            this.btnCheckXMLSerialisation.TabIndex = 0;
            this.btnCheckXMLSerialisation.Text = "Check XML Serialisation";
            this.btnCheckXMLSerialisation.UseVisualStyleBackColor = true;
            this.btnCheckXMLSerialisation.Click += new System.EventHandler(this.btnCheckXMLSerialisation_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 359);
            this.Controls.Add(this.btnCheckXMLSerialisation);
            this.Name = "FrmMain";
            this.Text = "WManager sandbox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckXMLSerialisation;
    }
}

