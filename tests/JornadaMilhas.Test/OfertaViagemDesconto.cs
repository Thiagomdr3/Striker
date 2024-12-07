using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {

        [Theory]
        [InlineData(120, 30)]     // Caso onde o desconto é maior que o máximo permitido
        [InlineData(100, 30)]     // Caso onde o desconto é exatamente o máximo permitido
        [InlineData(-1, 100)]     // Caso onde o desconto é menor ou igual a zero
        [InlineData(0, 100)]      // Caso onde o desconto é exatamente zero
        [InlineData(70, 30)]      // Caso onde o desconto é exatamente 70%
        [InlineData(20, 80)]      // Caso normal com desconto de 20%
        [InlineData(10, 90)]      // Caso normal com desconto de 10%
        [InlineData(10.5, 89.5)]  // Caso normal com desconto de 10.5%
        public void ValudaCoesGeraisDeDesconto(double desconto, double precoComDesconto)
        {
            // Arrange
            Rota rota = new Rota("São Paulo", "Rio de Janeiro");
            Periodo periodo = new Periodo(DateTime.Now, DateTime.Now.AddDays(1));
            double precoOriginal = 100.00;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            // Act
            oferta.Desconto = desconto;

            // Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

    }
}
