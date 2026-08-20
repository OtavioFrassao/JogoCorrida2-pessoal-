using JogoCorrida;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

        Console.SetWindowSize (50, 40);
        Console.SetBufferSize (50, 40);

        Jogo jogo = new Jogo
        {
            Faixa1Inicio = 1,
            Faixa1Fim = 10,
            Faixa2Inicio = 12,
            Faixa2Fim = 21,
            Faixa3Inicio = 23,
            Faixa3Fim = 32,
            Faixa4Inicio = 34,
            Faixa4Fim = 43
        };
        jogo.YMaximo = 30;
        jogo.IniciaJogo();
        int faixaCarro = 1;
        jogo.Carro.PosicaoX = jogo.PosicionaObjeto(1);
        jogo.Carro.PosicaoY = jogo.YMaximo - 1;
        jogo.Velocidade = 100;
        var tempoUltimaMovimentacao = DateTime.Now;
        

        var diferencaTempo = (DateTime.Now - tempoUltimaMovimentacao).Milliseconds;


        for ( ; ; ) {
            Desenha_Cenario();
            DesenhaVidas(jogo.Vidas);
            Desenha_Elemento(jogo.Carro.PosicaoY + 1, jogo.Carro.PosicaoX, '8');
            foreach (var ob in jogo.Obstaculos)
            {
                if(ob.PosicaoY >= 0)
                {
                    Desenha_Elemento(ob.PosicaoY + 1, ob.PosicaoX, '0');
                }
            }
            

            if((DateTime.Now - tempoUltimaMovimentacao).TotalMilliseconds > jogo.Velocidade)
            {
                tempoUltimaMovimentacao = DateTime.Now;
                jogo.MovimentaObstaculos();
            }
            if (Console.KeyAvailable)
            {
                var tecla = Console.ReadKey();
                if(tecla.Key == ConsoleKey.LeftArrow)
                {
                    if(faixaCarro > 1)
                    {
                        faixaCarro--;
                        jogo.Carro.PosicaoX = jogo.PosicionaObjeto(faixaCarro);
                    }
                }
                else if(tecla.Key == ConsoleKey.RightArrow)
                {
                    if(faixaCarro < 4)
                    {
                        faixaCarro++;
                        jogo.Carro.PosicaoX = jogo.PosicionaObjeto(faixaCarro);
                    }
                }
                TocarSom();
            }
            if (jogo.ChecarColisao())
            {
                jogo.PerdeVida();
                
                TocarSom();

                if(jogo.Vidas <= 0)
                {
                    GameOver();
                    break;
                }
                
            }
            Thread.Sleep(150);
        }
    }

    public static void Desenha_Elemento(int linha, int coluna, char simbolo)
    {
        var xOriginal = Console.CursorLeft;
        var yOriginal = Console.CursorTop;
        Console.SetCursorPosition(coluna, linha);
        Console.Write(simbolo.ToString());
        Console.SetCursorPosition(xOriginal, yOriginal);
    }

    public static void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(17, 10);
        Console.WriteLine("Game Over!");
        Console.WriteLine();
        Console.SetCursorPosition(9, 12);
        Console.WriteLine("Voce perdeu todas as vidas!");
        Console.SetCursorPosition(0, 14);
        Console.ReadKey();
    }


    public static void Desenha_Cenario()
    {
        Console.SetCursorPosition(0, 0);


        Console.WriteLine("+----------+----------+----------+----------+");
        for (int i = 0; i < 30; i++) {

            Console.WriteLine("|          |          |          |          |");
        }
        
        Console.WriteLine("+----------+----------+----------+----------+");
    }

    public static void DesenhaVidas(int Vidas)
    {
        Console.SetCursorPosition(0, 33);
        Console.WriteLine("Vidas:          ");

        Console.SetCursorPosition(0, 34);
        for (int i = 0; i < 3; i++)
        {
            if (i < Vidas)
            {
                Console.Write("♥ ");
            }
            else
            {
                Console.Write("♡ ");
            }
        }
    }
    static void TocarSom()
    {
        Console.Beep();
    }
}

