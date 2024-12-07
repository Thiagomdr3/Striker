using JornadaMilhasV1.Validador;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhasV1.Modelos;

public class Rota: Valida
{
    public int Id { get; set; }
    public string Origem { get; set; }
    public string Destino { get; set; }

    public Rota(string origem, string destino)
    {
        Origem = origem;
        Destino = destino;
        Validar();
    }

    protected override void Validar()
    {
        if (string.IsNullOrEmpty(this.Origem))
        {
            Erros.RegistrarErro("A rota não pode possuir uma origem nula ou vazia.");
        }
        else if (string.IsNullOrEmpty(this.Destino))
        {
            Erros.RegistrarErro("A rota não pode possuir um destino nulo ou vazio.");
        }
    }
}
