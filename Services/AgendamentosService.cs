using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Services
{
    public class AgendamentosService
    {
        private readonly AgendamentobotContext _dbContext;

        public AgendamentosService(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AgendamentoDTO>> GetAll(AgendamentoFiltrosDTO filtros)
        {
            try
            {
                List<Agendamento> agendamentos = new List<Agendamento>();

                agendamentos = await _dbContext.Agendamentos
                .Where(x => 
                    (string.IsNullOrEmpty(filtros.Telefone) || x.Telefone == filtros.Telefone ) &&
                    (string.IsNullOrEmpty(filtros.Nome) || x.Nome == filtros.Nome ) &&
                    (string.IsNullOrEmpty(filtros.Servico) || x.Servico == filtros.Servico ) &&
                    (filtros.Data != null || x.Data.Date == filtros.Data.Date )
                )
                .AsNoTracking()
                .ToListAsync();

                List<AgendamentoDTO> retorno = agendamentos.Select(x => new AgendamentoDTO
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Telefone  = x.Telefone,
                    Data = x.Data,
                    UsuarioId = x.UsuarioId,
                    Servico = x.Servico,
                    Executado = x.Executado
                }).ToList();

                return retorno;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<AgendamentoDTO> Update(AgendamentoDTO model)
        {
            try
            {
                var existeAgendamento = await _dbContext.Agendamentos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if(existeAgendamento == null)
                    throw new ArgumentException("Erro ao atualizar, o agendamento não existe!");
                
                existeAgendamento.Nome = model.Nome;
                existeAgendamento.Telefone = model.Telefone;
                existeAgendamento.Servico = model.Servico;
                existeAgendamento.Executado = model.Executado;

                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}