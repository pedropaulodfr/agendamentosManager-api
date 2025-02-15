using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamentosmanager_api.DTO.Relatorios
{
    public class RelatorioAgendamentosDTO
    {
       public long Id { get; set; }

        public string? Nome { get; set; }

        public string? Telefone { get; set; } = null!;

        public DateTime? Data { get; set; }

        public long? UsuarioId { get; set; }

        public string Servico { get; set; } = null!;

        public bool? Executado { get; set; } 
        public string? Status { get; set; } 
    }

    public class RelatorioAgendamentoFiltros
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public string? Executado { get; set; } 
    }
}