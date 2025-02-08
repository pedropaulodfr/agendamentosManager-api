using System;
using System.Collections.Generic;

namespace agendamentosmanager_api.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public string Perfil { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cpfcnpj { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public bool? Master { get; set; }

    public bool? Ativo { get; set; }

    public bool? Deletado { get; set; }
}
