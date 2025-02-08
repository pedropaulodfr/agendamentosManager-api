using System;
using System.Collections.Generic;

namespace agendamentosmanager_api.Models;

public partial class Horario
{
    public long Id { get; set; }

    public TimeOnly? Hora { get; set; }
}
