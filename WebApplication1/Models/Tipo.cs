using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Tipo
{
    public long Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descricao { get; set; } = null!;
}
