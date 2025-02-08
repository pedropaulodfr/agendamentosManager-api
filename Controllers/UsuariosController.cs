using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO;
using agendamentosmanager_api.DTO.Agendamento;
using agendamentosmanager_api.DTO.Horarios;
using agendamentosmanager_api.DTO.Servicos;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace agendamentosmanager_api.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuariosService;
        public UsuariosController(UsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return StatusCode(200, await _usuariosService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Insert([FromBody] UsuarioDTO model)
        {
            try
            {
                return StatusCode(200, await _usuariosService.Insert(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] UsuarioDTO model)
        {
            try
            {
                return StatusCode(200, await _usuariosService.Update(model));
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
                await _usuariosService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}