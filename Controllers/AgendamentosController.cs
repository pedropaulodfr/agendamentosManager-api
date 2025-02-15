using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace agendamentosmanager_api.Controllers
{
    [Route("[controller]")]
    public class AgendamentosController : Controller
    {
        private readonly AgendamentosService _agendamentosService;

        public AgendamentosController(AgendamentosService agendamentosService)
        {
            _agendamentosService = agendamentosService;
        }

        [Authorize]
        [HttpPost]
        [Route("getAll")]
        public async Task<ActionResult> GetAll([FromBody] AgendamentoFiltrosDTO filtros)
        {
            try
            {
                return StatusCode(200, await _agendamentosService.GetAll(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message  });
            }
        }

        [Authorize]    
        [HttpPost]
        [Route("concluirAgendamento")]
        public async Task<ActionResult> ConcluirAgendamento([FromBody] AgendamentoDTO model)
        {
            try
            {
                return StatusCode(200, await _agendamentosService.ConcluirAgendamento(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message  });
            }
        }   

        [Authorize]    
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] AgendamentoDTO model)
        {
            try
            {
                return StatusCode(200, await _agendamentosService.Update(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message  });
            }
        }
        
    }
}