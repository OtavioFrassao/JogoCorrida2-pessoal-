namespace JogoCorridaWinFormsApp
{
    partial class TelaEstatisticaJogoCorrida
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelPrincipal3 = new Panel();
            picLogo = new PictureBox();
            panelPrincipal3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelPrincipal3
            // 
            panelPrincipal3.BackColor = Color.Transparent;
            panelPrincipal3.Controls.Add(picLogo);
            panelPrincipal3.Location = new Point(12, 12);
            panelPrincipal3.Name = "panelPrincipal3";
            panelPrincipal3.Size = new Size(1304, 604);
            panelPrincipal3.TabIndex = 0;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.BackgroundImage = Properties.Resources.LogoStreetCars;
            picLogo.BackgroundImageLayout = ImageLayout.Stretch;
            picLogo.Location = new Point(456, -15);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(395, 168);
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // TelaEstatisticaJogoCorrida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.FundoStreetCars;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1328, 628);
            Controls.Add(panelPrincipal3);
            Name = "TelaEstatisticaJogoCorrida";
            Text = "TelaEstatisticaJogoCorrida";
            panelPrincipal3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPrincipal3;
        private PictureBox picLogo;
    }
}