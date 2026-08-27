using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Obrigatório para ordenar a navegação do teclado
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    public partial class TelaInicioJogoCorrida : Form
    {
        List<PictureBox> listaPersonagens = new List<PictureBox>();
        int indiceSelecionado = 0;
        bool telaTravada = false;

        System.Windows.Forms.Timer timerRoleta = new System.Windows.Forms.Timer();
        int ticksRoleta = 0;

        System.Diagnostics.Stopwatch cronometroRoleta = new System.Diagnostics.Stopwatch();
        PictureBox personagemVencedor = null;

        bool bordaAcesa = true;

        System.Media.SoundPlayer somRoleta = new System.Media.SoundPlayer(@"D:\roleta-normal.wav");
        public TelaInicioJogoCorrida()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            timerRoleta.Tick += TimerRoleta_Tick;
            this.Load += TelaInicioJogoCorrida_Load;
        }

        private void TelaInicioJogoCorrida_Load(object sender, EventArgs e)
        {
            somRoleta.LoadAsync();
            if (panelPrincipal != null)
            {
                foreach (Control controle in panelPrincipal.Controls)
                {
                    if (controle is PictureBox pic)
                    {
                        if (pic.Name == "picLogo" || (pic.Image == null && pic.BackgroundImage == null))
                        {
                            continue;
                        }

                        pic.BorderStyle = BorderStyle.None;
                        pic.Paint += Personagem_Paint;

                        listaPersonagens.Add(pic);

                        if (pic.Name == "picAleatorio")
                            pic.Click += PicAleatorio_Click;
                        else
                            pic.Click += Personagem_Click;
                    }
                }
                listaPersonagens = listaPersonagens.OrderBy(p => p.Top).ThenBy(p => p.Left).ToList();
            }

            if (listaPersonagens.Count > 0)
            {
                AtualizarBordaVisual();
            }
        }

        private void Personagem_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;

            if (listaPersonagens.Count > 0 && pic == listaPersonagens[indiceSelecionado] && bordaAcesa)
            {
                ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid,
                    Color.DarkRed, 4, ButtonBorderStyle.Solid);
            }
        }

        private void AtualizarBordaVisual()
        {
            if (listaPersonagens.Count == 0) return;

            foreach (var pic in listaPersonagens)
            {
                pic.Invalidate();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal != null)
            {
                panelPrincipal.Dock = DockStyle.None;
                panelPrincipal.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal.Height) / 2;
                panelPrincipal.Location = new Point(centroX, centroY);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (telaTravada || listaPersonagens.Count == 0) return true;


            if (keyData == Keys.Right || keyData == Keys.D || keyData == Keys.Tab)
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaPersonagens.Count) indiceSelecionado = 0; // Se chegou no fim, volta pro começo
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Left || keyData == Keys.A)
            {
                indiceSelecionado--;
                if (indiceSelecionado < 0) indiceSelecionado = listaPersonagens.Count - 1; // Se chegou no começo, vai pro fim
                AtualizarBordaVisual();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                ConfirmarSelecao(listaPersonagens[indiceSelecionado]);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Personagem_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;

            PictureBox clicado = sender as PictureBox;
            indiceSelecionado = listaPersonagens.IndexOf(clicado); 
            AtualizarBordaVisual();
            ConfirmarSelecao(clicado);
        }

        private void PicAleatorio_Click(object sender, EventArgs e)
        {
            if (telaTravada) return;
            ExecutarSorteio();
        }

        private void ConfirmarSelecao(PictureBox selecionado)
        {
            if (selecionado.Name == "picAleatorio")
            {
                ExecutarSorteio();
            }
            else
            {
                IniciarCorrida(selecionado);
            }
        }

        private void ExecutarSorteio()
        {
            telaTravada = true;

            somRoleta.Play();

            cronometroRoleta.Restart();
            timerRoleta.Interval = 40; 
            timerRoleta.Start();
        }

        private void TimerRoleta_Tick(object sender, EventArgs e)
        {
            ticksRoleta++;

            do
            {
                indiceSelecionado++;
                if (indiceSelecionado >= listaPersonagens.Count) indiceSelecionado = 0;

            } while (listaPersonagens[indiceSelecionado].Name == "picAleatorio");

            AtualizarBordaVisual();

            long tempoPassado = cronometroRoleta.ElapsedMilliseconds;

            if (tempoPassado > 6500) timerRoleta.Interval = 100; // Aos 7 seg freia
            if (tempoPassado > 7800) timerRoleta.Interval = 300; // Aos 8.5 seg freia mais
            if (tempoPassado > 9300) timerRoleta.Interval = 450; //parando
            if (tempoPassado >= 9500)
            {
                timerRoleta.Stop();
                cronometroRoleta.Stop();

                IniciarTransicao(listaPersonagens[indiceSelecionado]);
            }
        }
        private void IniciarTransicao(PictureBox personagemEscolhido)
        {
            telaTravada = true;
            //SOM DE CONFIRMAÇÃO
            // new System.Media.SoundPlayer(@"C:\caminho\som_escolhido.wav").Play();

            int piscadas = 0;
            System.Windows.Forms.Timer timerPiscar = new System.Windows.Forms.Timer();
            timerPiscar.Interval = 150; // A cada 150 milissegundos ela pisca
            timerPiscar.Tick += (s, args) =>
            {
                bordaAcesa = !bordaAcesa; 
                AtualizarBordaVisual();
                piscadas++;

                if (piscadas >= 6)
                {
                    timerPiscar.Stop();
                    bordaAcesa = true;
                    AtualizarBordaVisual();
                }
            };
            timerPiscar.Start();

            System.Windows.Forms.Timer timerEspera = new System.Windows.Forms.Timer();
            timerEspera.Interval = 2500;
            timerEspera.Tick += (s, args) =>
            {
                timerEspera.Stop();

                FormJogoCorrida jogo = new FormJogoCorrida();
                jogo.Show();
                this.Hide();
            };
            timerEspera.Start();
        }
        private void IniciarCorrida(PictureBox personagemEscolhido)
        {
            
            FormJogoCorrida jogo = new FormJogoCorrida();
            jogo.Show();

            this.Hide();
        }

    }
}
