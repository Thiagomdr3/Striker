using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Validador;

namespace JornadaMilhasV1.Modelos;

public class OfertaViagem : Valida
{
    public const double DESCONTO_MAXIMO = 0.7;
    private double desconto;

    public int Id { get; set; }
    public Rota Rota { get; set; }
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }
    public bool Ativa { get; set; } = true;
    public double Desconto
    {
        get => desconto;
        set
        {
            desconto = value;
            Preco = AplicarDesconto();
        }
    }

    private double AplicarDesconto()
    {
        if (desconto <= 0)
            return Preco;

        if (desconto >= 70)
            return Preco * (1 - DESCONTO_MAXIMO);

        return Preco - (Preco * (desconto / 100));
    }


    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
    }

    protected override void Validar()
    {
        if (Rota == null || Periodo == null)
        {
            Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
        }
        if (Preco <= 0)
        {
            Erros.RegistrarErro("O preço da oferta de viagem deve ser maior que zero.");
        }
        if (Rota != null && !Rota.IsValid)
        {
            Erros.RegistrarErro(Rota.Erros.Sumario);
        }
        if (Periodo != null && !Periodo.IsValid)
        {
            Erros.RegistrarErro(Periodo.Erros.Sumario);
        }
    }
}
