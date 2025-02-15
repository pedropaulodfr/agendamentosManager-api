using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.DTO.Relatorios;
using agendamentosmanager_api.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Services
{
    public class RelatoriosService
    {
        private readonly AgendamentobotContext _dbContext;
        public RelatoriosService(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<RelatorioAgendamentosDTO>> RelatorioAgendamentos(RelatorioAgendamentoFiltros filtros)
        {
            try
            {
                List<Agendamento> agendamentos = await _dbContext.Agendamentos.Where(x =>
                    (filtros.DataInicial == null || x.Data.Date >= filtros.DataInicial) &&
                    (filtros.DataFinal == null || x.Data.Date <= filtros.DataFinal) &&
                    (string.IsNullOrEmpty(filtros.Executado) || x.Executado == Boolean.Parse(filtros.Executado)))
                    .ToListAsync();

                List<RelatorioAgendamentosDTO> relatorio = agendamentos.Select(x => new RelatorioAgendamentosDTO
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    Data = x.Data,
                    UsuarioId = x.UsuarioId,
                    Servico = x.Servico,
                    Executado = x.Executado,
                    Status = x.Executado == true ? "Executado" : "Pendente"
                }).ToList();

                return relatorio;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}