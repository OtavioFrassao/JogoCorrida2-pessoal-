using JogoCorrida;
using JogoCorridaWinFormsApp.Properties;
using System.Resources;

namespace JogoCorridaWinFormsApp
{
    public partial class FormJogoCorrida : Form
    {
        Jogo jogo;
        DateTime tempoUltimaMovimentacao = DateTime.Now;
        List<PictureBox> pictureBoxes = [];
        int faixaAtual = 1;
        DateTime tempoInicioJogo;

        private readonly bool isMultiplayer;
        private readonly TipoCenario cenarioSelecionado;
        public FormJogoCorrida(bool modoMultiplayer, TipoCenario cenario)
        {
            InitializeComponent();

            isMultiplayer = modoMultiplayer;
            cenarioSelecionado = cenario;

            CarregarImagemCenario();

            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;


            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJogoCorrida));
            jogo = new Jogo
            {
                Faixa1Inicio = 120,
                Faixa1Fim = 650,
                Faixa2Inicio = 500,
                Faixa2Fim = 950,
                Faixa3Inicio = 930,
                Faixa3Fim = 1250,
                Faixa4Inicio = 1380,
                Faixa4Fim = 1500,
                Vidas = 3
            };

            jogo.YMaximo = Screen.PrimaryScreen.Bounds.Height; 
            jogo.IniciaJogo();
            jogo.Carro.PosicaoX = jogo.PosicionaObjeto(faixaAtual);
            jogo.Carro.PosicaoY = jogo.YMaximo - 200;
            jogo.Velocidade = 100;


            foreach (var ob in jogo.Obstaculos) // criação das imagens dos obstaculos
            {
                var picOb = new PictureBox();
                picOb.BackColor = Color.Transparent;
                picOb.BackgroundImage = Properties.Resources.picObjeto;
                picOb.BackgroundImageLayout = ImageLayout.Stretch;
                //picObjeto.BackgroundImage = (Image)resources.GetObject("picObjeto.BackgroundImage");
                //picOb.BackgroundImageLayout = ImageLayout.Stretch;
                picOb.Size = new Size(80, 110);
                pictureBoxes.Add(picOb); // adicona a imagem na lista interna
                this.Controls.Add(picOb); // adiciona a imagem no formulario
            }

            TextoTempo.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 250, 20);
            TextoVidas.ForeColor = Color.Red;
            TextoVidas.Text = "Vidas: ❤️❤️❤️";
            TextoVidas.Font = new Font("Segoe UI Emoji", 16, FontStyle.Bold);


            TextoTempo.Location = new Point(this.ClientSize.Width - -810, 25);

            tempoInicioJogo = DateTime.Now;

            timerJogo.Enabled = true;

        }



        public void GameOver()
        {
            timerJogo.Enabled = false;

            TimeSpan tempoFinal = DateTime.Now - tempoInicioJogo;

            string mensagem = $"Você perdeu todas as vidas...\nE ficou {tempoFinal.Minutes} minutos e {tempoFinal.Seconds} segundos no jogo!";

            MessageBox.Show(mensagem, "Fim de Jogo!");

            Application.Exit();
        }

        static void TocarSom()
        {
            Console.Beep();
        }


        private void FormJogoCorrida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (faixaAtual < 4)
                {
                    faixaAtual++;
                    jogo.Carro.PosicaoX = jogo.PosicionaObjeto(faixaAtual);
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (faixaAtual > 1)
                {
                    faixaAtual--;
                    jogo.Carro.PosicaoX = jogo.PosicionaObjeto(faixaAtual);
                }
            }
        }


        private void timerJogo_Tick(object sender, EventArgs e)
        {
            TimeSpan tempoDecorrido = DateTime.Now - tempoInicioJogo;
            TextoTempo.Text = $"Tempo: {tempoDecorrido.Minutes:D2}:{tempoDecorrido.Seconds:D2}";

            jogo.YMaximo = this.ClientSize.Height;
            picCarro.Location = new Point(jogo.Carro.PosicaoX, jogo.Carro.PosicaoY);
            var i = 0;
            foreach (var ob in jogo.Obstaculos)
            {
               
                pictureBoxes[i].Location = new Point(ob.PosicaoX, ob.PosicaoY);
                
                i++;
            }


            if ((DateTime.Now - tempoUltimaMovimentacao).Milliseconds > jogo.Velocidade)
            {
                tempoUltimaMovimentacao = DateTime.Now;
                jogo.MovimentaObstaculos();
            }

            if (jogo.ChecarColisao())
            {
                jogo.PerdeVida();
                AtualizarTituloJanela();

                AtualizarVidasUI();

                if (jogo.Vidas <= 0)
                {
                    timerJogo.Enabled = false;
                    GameOver();
                }

            }
            Application.DoEvents();

        }

        private void AtualizarTituloJanela()
        {
            this.Text = $"JogoCorrida - IFSP | Vidas: {jogo.Vidas}";
        }

        private void AtualizarVidasUI()
        {
            if (jogo.Vidas == 3) TextoVidas.Text = "Vidas: ♥♥♥";
            else if (jogo.Vidas == 2) TextoVidas.Text = "Vidas: ♥♥♡";
            else if (jogo.Vidas == 1) TextoVidas.Text = "Vidas: ♥♡♡";
            else TextoVidas.Text = "Vidas: ♡♡♡";
        }

        private void CarregarImagemCenario()
        {
            Image imagemEscolhida;

            if (cenarioSelecionado == TipoCenario.Areas_Rochosas)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario1multgif
                    : Properties.Resources.cenario1gif;
            }
            else if (cenarioSelecionado == TipoCenario.Alem_Do_Mundo)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario2multgif
                    : Properties.Resources.cenario2gif;
            }
            else if (cenarioSelecionado == TipoCenario.Terras_Desconhecidas)
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario3multgif
                    : Properties.Resources.cenario3gif;
            }
            else
            {
                imagemEscolhida = isMultiplayer
                    ? Properties.Resources.cenario4multgif
                    : Properties.Resources.cenario4gif;
            }

            this.BackgroundImage = imagemEscolhida;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
