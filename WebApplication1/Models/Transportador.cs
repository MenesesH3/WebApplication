using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Transportador
{
    public long Id { get; set; }

    public string Cnpj { get; set; } = null!;

    public string Descricao { get; set; } = null!;
}
