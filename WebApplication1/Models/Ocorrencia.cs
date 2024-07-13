using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Ocorrencia
{
    public long Id { get; set; }

    public long IdTipo { get; set; }

    public long IdTransportador { get; set; }

    public DateTime OcorreuEm { get; set; }

    public DateTime? SolucaoEm { get; set; }
}
