using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.DTO.Servicos;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace agendamentosmanager_api.Controllers
{
    [Route("[controller]")]
    public class ServicosController : Controller
    {
        private readonly ServicosService _servicosService;
        public ServicosController(ServicosService servicosService)
        {
            _servicosService = servicosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return StatusCode(200, await _servicosService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Insert([FromBody] ServicosDTO model)
        {
            try
            {
                return StatusCode(200, await _servicosService.Insert(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] ServicosDTO model)
        {
            try
            {
                return StatusCode(200, await _servicosService.Update(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
        
    }
}