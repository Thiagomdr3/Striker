using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class GerenciadorDeOfertasRecuperaMaiorDesconto
    {
        [Fact]
        public void RetornaOfertaNulaQuandoListaEstaVazia()
        {
            // Arrange
            var lista= new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = (o) => o.Rota.Destino == "São Paulo";

            // Act
            var oferta = gerenciador.RecuperarMaiorDesconto(filtro);

            // Assert
            Assert.Null(oferta);
        }

        [Fact]
        //destino = São Paulo, desconto = 40%, preço = 80
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
        {
            // Arrange
            var fakePeriodo = new Faker<Periodo>()
                .CustomInstantiator(f =>
                {
                    DateTime dataInicial = f.Date.Soon();
                    return new Periodo(dataInicial, dataInicial.AddDays(30));
                });

            var rota = new Rota("Curitiba", "São Paulo");

            var fakeOferta = new Faker<OfertaViagem>()
                .CustomInstantiator(f =>
                {
                    var periodo = fakePeriodo.Generate();
                    return new OfertaViagem(rota, periodo, 100 * f.Random.Int(1,100));
                })
                .RuleFor(o => o.Desconto, f => 40)
                .RuleFor(o => o.Ativa, f=>true );
                

            var ofertaEscolhida = new OfertaViagem(rota, fakePeriodo.Generate(), 80) 
            {

                Desconto = 40,
                Ativa = true
            };

            var ofertaInativa = new OfertaViagem(rota, fakePeriodo.Generate(), 80)
            {
                Desconto = 50,
                Ativa = false
            };
            
            var lista = fakeOferta.Generate(200);
            lista.Add(ofertaEscolhida);
            //lista.Add(ofertaInativa);

            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = (o) => o.Rota.Destino == "São Paulo" && o.Ativa;

            var precoEsperado = 80 - (80 * 0.4);
            // Act
            var oferta = gerenciador.RecuperarMaiorDesconto(filtro);

            // Assert
            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.Preco, 0.001);
        }
    }
}
