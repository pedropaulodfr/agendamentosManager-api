using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Horarios;
using agendamentosmanager_api.Models;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Services
{
    public class HorariosService
    {
        private readonly AgendamentobotContext _dbContext;
        public HorariosService(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<HorariosDTO>> GetAll()
        {
            try
            {
                List<Horario> horarios = await _dbContext.Horarios.AsNoTracking().ToListAsync();

                List<HorariosDTO> retorno = horarios.Select(x => new HorariosDTO
                {
                    Id = x.Id,
                    Hora = x.Hora
                }).ToList();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<HorariosDTO> Insert(HorariosDTO model)
        {
            Horario horario = new Horario
            {
                Hora = model.Hora
            };

            await _dbContext.AddAsync(horario);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task Delete(long Id)
        {
            try
            {
                var existeHorario = await _dbContext.Horarios.Where(x => x.Id == Id).FirstOrDefaultAsync();
                if(existeHorario == null)
                    throw new ArgumentException("Erro ao deletar, o horário não existe!");
                
                _dbContext.Remove(existeHorario);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}