using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.DTO.Relatorios;
using agendamentosmanager_api.DTO.Servicos;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace agendamentosmanager_api.bin
{
    [Route("[controller]")]
    public class RelatoriosController : Controller
    {
        private readonly RelatoriosService _relatoriosService;
        public RelatoriosController(RelatoriosService relatoriosService)
        {
            _relatoriosService = relatoriosService;
        }

        [Authorize]
        [HttpPost]
        [Route("relatorioAgendamentos")]
        public async Task<ActionResult> RelatorioAgendamentos([FromBody] RelatorioAgendamentoFiltros filtros)
        {
            try
            {
                return StatusCode(200, await _relatoriosService.RelatorioAgendamentos(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}