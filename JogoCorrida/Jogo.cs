
namespace JogoCorrida
{
    public class Jogo
    {

        public Elemento Carro { get; set; }
        public List<Elemento> Obstaculos { get; set; }
        public int Velocidade { get; set; }
        public int Pontuacao { get; set; }
        public int Tempo { get; set; }
        public int MelhorPontuacao { get; set; }
        public int ColisoesPermitidas { get; set; }
        public int Faixa1Inicio { get; set; }
        public int Faixa1Fim { get; set; }
        public int Faixa2Inicio { get; set; }
        public int Faixa2Fim { get; set; }
        public int Vidas { get; set; }
        public int Faixa3Inicio { get; set; }
        public int Faixa3Fim { get; set; }
        public int Faixa4Inicio { get; set; }
        public int Faixa4Fim { get; set; }

        public int YMaximo { get; set; } = 50;

        public void IniciaJogo()
        {
            Carro = new Elemento();
            Carro.Tipo = TipoElemento.Carro;
            Carro.PosicaoX = PosicionaObjeto(1);
            Carro.PosicaoY = YMaximo - 100;

            Obstaculos = FabricaObstaculos(200, 300, 450);
        }
        public List<Elemento> FabricaObstaculos(int qtd, int dmin, int dmax)
        {
            var y_incial = 0;
            var obstaculos = new List<Elemento>();
            var rnd = new Random();

            for (int i = 0; i < qtd; i++)
            {
                if (i != 0)
                    y_incial -= rnd.Next(dmin, dmax);
                var ob = new Elemento()
                {
                    Tipo = TipoElemento.Obstaculo
                };
                var faixa = rnd.Next(1, 5);
                ob.PosicaoX = PosicionaObjeto(faixa);
                ob.PosicaoY = y_incial;
                obstaculos.Add(ob);

            }
            return obstaculos;

        }
        public int PosicionaObjeto(int faixa)
        {
            switch (faixa)
            {
                case 1: return Faixa1Inicio + ((Faixa1Fim - Faixa1Inicio) / 2);
                case 2: return Faixa2Inicio + ((Faixa2Fim - Faixa2Inicio) / 2);
                case 3: return Faixa3Inicio + ((Faixa3Fim - Faixa3Inicio) / 2);
                case 4: return Faixa4Inicio + ((Faixa4Fim - Faixa4Inicio) / 2);
                default: return Faixa1Inicio + ((Faixa1Fim - Faixa1Inicio) / 2);
            }
        }
        public void Acelerar(int incremento)
        {
            Velocidade += incremento;
        }

        public void PerdeVida()
        {
            Vidas--;
        }
        private int ChecaFaixaElemento(Elemento elemento)
        {
            if (elemento.PosicaoX >= Faixa1Inicio &&
                elemento.PosicaoX <= Faixa1Fim)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public bool ChecarColisao()
        {
            int larguraObjeto = 80;
            int alturaObjeto = 110;

            int larguraCarro = 80;
            int alturaCarro = 110;

            foreach (var ob in Obstaculos)
            {
                bool bateuX = Carro.PosicaoX < (ob.PosicaoX + larguraObjeto) &&
                              (Carro.PosicaoX + larguraCarro) > ob.PosicaoX;

                bool bateuY = Carro.PosicaoY < (ob.PosicaoY + alturaObjeto) &&
                              (Carro.PosicaoY + alturaCarro) > ob.PosicaoY;

                if (bateuX && bateuY)
                {
                    ob.PosicaoY = -250; 
                    return true;
                }
            }
            return false;
        }

        public void MovimentaObstaculos()
        {
            var rnd = new Random();

            int YMaisAlto = 0;

            foreach (var o in Obstaculos)
            {
                if (o.PosicaoY < YMaisAlto) YMaisAlto = o.PosicaoY;
            }

            foreach (var ob in Obstaculos)
            {
                ob.PosicaoY += 10;
                if (ob.PosicaoY > YMaximo + 150)
                {
                    ob.PosicaoY = YMaisAlto - rnd.Next(250, 400);
                    ob.PosicaoX = PosicionaObjeto(rnd.Next(1, 5));

                    YMaisAlto = ob.PosicaoY;
                }
            }
        }
        public bool VerificaFimJogo()
        {
            return true;
        }

    }
}
