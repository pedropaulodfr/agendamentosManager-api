using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamentosmanager_api.DTO.Servicos
{
    public class ServicosDTO
    {
        public long Id { get; set; }

        public string? Identificacao { get; set; }

        public string? Descricao { get; set; }

        public int? TempoEstimado { get; set; }

        public bool? Ativo { get; set; }
        public string? Status { get; set; }
    }
}