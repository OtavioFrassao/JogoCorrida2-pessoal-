namespace JogoCorridaWinFormsApp
{
    partial class FormJogoCorrida
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerJogo = new System.Windows.Forms.Timer(components);
            picCarro = new PictureBox();
            TextoVidas = new Label();
            TextoTempo = new Label();
            ((System.ComponentModel.ISupportInitialize)picCarro).BeginInit();
            SuspendLayout();
            // 
            // timerJogo
            // 
            timerJogo.Tick += timerJogo_Tick;
            // 
            // picCarro
            // 
            picCarro.BackColor = Color.Transparent;
            picCarro.BackgroundImage = Properties.Resources.picCarro;
            picCarro.BackgroundImageLayout = ImageLayout.Stretch;
            picCarro.Location = new Point(287, 504);
            picCarro.Margin = new Padding(3, 4, 3, 4);
            picCarro.Name = "picCarro";
            picCarro.Size = new Size(105, 128);
            picCarro.TabIndex = 0;
            picCarro.TabStop = false;
            // 
            // TextoVidas
            // 
            TextoVidas.AutoSize = true;
            TextoVidas.BackColor = Color.Transparent;
            TextoVidas.Font = new Font("Segoe UI", 15F);
            TextoVidas.ForeColor = SystemColors.ActiveCaptionText;
            TextoVidas.Location = new Point(1, 27);
            TextoVidas.Name = "TextoVidas";
            TextoVidas.Size = new Size(68, 28);
            TextoVidas.TabIndex = 1;
            TextoVidas.Text = "Vidas: ";
            // 
            // TextoTempo
            // 
            TextoTempo.AutoSize = true;
            TextoTempo.BackColor = Color.Transparent;
            TextoTempo.Font = new Font("Segoe UI", 15F);
            TextoTempo.ForeColor = SystemColors.ActiveCaptionText;
            TextoTempo.Location = new Point(1027, 41);
            TextoTempo.Name = "TextoTempo";
            TextoTempo.Size = new Size(75, 28);
            TextoTempo.TabIndex = 2;
            TextoTempo.Text = "Tempo:";
            // 
            // FormJogoCorrida
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.pista4faixas;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1110, 728);
            Controls.Add(TextoTempo);
            Controls.Add(TextoVidas);
            Controls.Add(picCarro);
            ForeColor = SystemColors.ControlLight;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormJogoCorrida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JogoCorrida - IFSP";
            KeyDown += FormJogoCorrida_KeyDown;
            ((System.ComponentModel.ISupportInitialize)picCarro).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox2;
        private System.Windows.Forms.Timer timerJogo;
        private PictureBox picCarro;
        private Label TextoVidas;
        private Label TextoTempo;
    }
}
