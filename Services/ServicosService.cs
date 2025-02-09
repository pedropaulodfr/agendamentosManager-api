using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Servicos;
using agendamentosmanager_api.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Services
{
    public class ServicosService
    {
        private readonly AgendamentobotContext _dbContext;
        public ServicosService(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ServicosDTO>> GetAll()
        {
            try
            {
                List<Servico> servicos = await _dbContext.Servicos.AsNoTracking().ToListAsync();

                List<ServicosDTO> retorno = servicos.Select(x => new ServicosDTO
                {
                    Id = x.Id,
                    Identificacao = x.Identificacao,
                    Descricao = x.Descricao,
                    TempoEstimado = x.TempoEstimado,
                    Ativo = x.Ativo,
                    Status = x.Ativo == true ? "Ativo" : "Inativo"
                }).ToList();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<ServicosDTO> Insert(ServicosDTO model)
        {
            var existeServico = await _dbContext.Servicos.Where(x => x.Identificacao.Trim() == model.Identificacao.Trim() && x.Ativo != false).FirstOrDefaultAsync();
            if(existeServico != null)
                throw new ArgumentException("Esse serviço já está cadastrado");

            Servico servico = new Servico
            {
                Identificacao = model.Identificacao,
                Descricao = model.Descricao,
                TempoEstimado = model.TempoEstimado,
                Ativo = model.Status == "Ativo" ? true : false,
            };

            await _dbContext.AddAsync(servico);
            await _dbContext.SaveChangesAsync();
            
            return model;
        }

        public async Task<ServicosDTO> Update(ServicosDTO model)
        {
            try
            {
                var existeServico = await _dbContext.Servicos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if(existeServico == null)
                    throw new ArgumentException("Erro ao atualizar, o serviço não existe!");
                
                existeServico.Identificacao = model.Identificacao;
                existeServico.Descricao = model.Descricao;
                existeServico.TempoEstimado = model.TempoEstimado;
                existeServico.Ativo = model.Status == "Ativo" ? true : false;

                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task Delete(long Id)
        {
            try
            {
                var existeServico = await _dbContext.Servicos.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if(existeServico == null)
                    throw new ArgumentException("O serviço não existe!");

                _dbContext.Remove(existeServico);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}