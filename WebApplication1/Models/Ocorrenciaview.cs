using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Ocorrenciaview
{
    public long Id { get; set; }
    public string CodigoTipo { get; set; } = null!;

    public string Descricaotipo { get; set; } = null!;

    public DateTime DataoCorrencia { get; set; }

    public DateTime? DataSolucao { get; set; }

    public DateTime OcorreuEm { get; set; }

    public DateTime? SolucaoEm { get; set; }

    public string CnpjTrasnportador { get; set; } = null!;

    public string DescricaoTransportador { get; set; } = null!;
}
