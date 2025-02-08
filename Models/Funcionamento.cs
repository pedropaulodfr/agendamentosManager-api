using System;
using System.Collections.Generic;

namespace agendamentosmanager_api.Models;

public partial class Funcionamento
{
    public long Id { get; set; }

    public DateTimeOffset? Abertura { get; set; }

    public DateTimeOffset? Fechamento { get; set; }

    public DateTimeOffset? Almoco { get; set; }

    public string? Dias { get; set; }
}
