using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", "Rio de Janeiro", "2024-02-01", "2024-02-02", 10, false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2024-02-01", "2024-02-05", 100.0,true)]
        [InlineData(null, "Rio de Janeiro", "2024-02-01", "2024-02-05", -1,false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2024-02-01", "2024-02-05", 0,false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2024-02-01", "2024-02-05", -500.0,false)]
        public void RetornarValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataInicio, string dataFinal, double preco, bool validacao)
        {
            //cenario Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataInicio), DateTime.Parse(dataFinal));

            //ação Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //verificação Assert
            //Assert.Equal(validacao, rota.IsValid);
            Assert.Equal(validacao, oferta.IsValid);
        }

        [Fact]
        public void RetornarMensagemDeErroQuandoRotaNullaOuPeriodosInvalidos()
        {
            //cenario Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(1));
            double preco = 100.0;
            bool validacao = true;

            //ação Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //verificação Assert
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.IsValid);
        }


        //[Fact]
        //public void TestandoDataInicialMaiorQueFinal()
        //{
        //    Rota rota = new Rota("São Paulo", "Rio de Janeiro");
        //    Periodo periodo = new Periodo(DateTime.Now.AddDays(1), DateTime.Now);
        //    double preco = 100.0;
        //    bool validacao = true;
        //    OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

        //    bool valido = oferta.Periodo.DataInicial < oferta.Periodo.DataFinal;

        //    Assert.True(valido, "A data Inicial deve ser menor que a data Final.");
        //    Assert.False(oferta.IsValid);
        //}

        [Fact]
        public void RetornarMensagemDeErroQuandoOPrecoForMenosQueZero()
        {
            //Arrange
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(1));
            double preco = -1;
            //Act
            JornadaMilhasV1.Modelos.OfertaViagem oferta = new JornadaMilhasV1.Modelos.OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);

        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            //Arrange
            int quantidadeEsperada = 3;
            Rota rota = null;
            Periodo periodo = new Periodo(DateTime.Now.AddDays(1), DateTime.Now);
            double preco = -1;
           
            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}