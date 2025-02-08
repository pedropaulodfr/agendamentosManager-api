using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.DTO.Horarios;
using agendamentosmanager_api.DTO.Servicos;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace agendamentosmanager_api.Controllers
{
    [Route("[controller]")]
    public class HorariosController : Controller
    {
        private readonly HorariosService _horariosService;
        public HorariosController(HorariosService horariosService)
        {
            _horariosService = horariosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return StatusCode(200, await _horariosService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Insert([FromBody] HorariosDTO model)
        {
            try
            {
                return StatusCode(200, await _horariosService.Insert(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _horariosService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}