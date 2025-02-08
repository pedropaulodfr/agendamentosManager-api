using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamentosmanager_api.DTO.Agendamento
{
    public class AgendamentoFiltrosDTO
    {
        public string? Nome { get; set; }

        public string? Telefone { get; set; } = null!;

        public DateTime Data { get; set; }

        public string Servico { get; set; } = null!;
    }
}