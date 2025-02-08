using System;
using System.Collections.Generic;

namespace agendamentosmanager_api.Models;

public partial class Servico
{
    public long Id { get; set; }

    public string? Identificacao { get; set; }

    public string? Descricao { get; set; }

    public int? TempoEstimado { get; set; }

    public bool? Ativo { get; set; }
}
