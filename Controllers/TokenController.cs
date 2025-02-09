using agendamentosmanager_api.DTO;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Controllers
{
    public class AuthController : ControllerBase
    {

        private readonly AgendamentobotContext _dbContext;

        public AuthController(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioDTO model)
        {
            try
            {
                // Recupera o usuário
                var user = await _dbContext.Usuarios.Where(x => x.Cpfcnpj == model.Cpfcnpj && x.Senha == model.Senha && x.Ativo != false && x.Deletado != true)
                .AsNoTracking()
                .FirstOrDefaultAsync();

                UsuarioDTO _user = new UsuarioDTO()
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Perfil = user.Perfil,
                    Master = user.Master,
                    Cpfcnpj = user.Cpfcnpj,
                    Senha = user.Senha,
                };

                // Verifica se o usuário existe
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                
                // Gera o Token
                var token = TokenService.GenerateToken(_user);

                // Oculta a senha
                user.Senha = "";
                user.Cpfcnpj = model.Cpfcnpj;


                // Retorna os dados encapsulados em um ActionResult
                UsuarioAutenticadoDTO usuarioAutenticado = new UsuarioAutenticadoDTO
                {
                    UsuarioId = _user.Id,
                    Nome = _user.Nome,
                    Cpfcnpj = _user.Cpfcnpj,
                    Perfil = _user.Perfil,
                    Master = _user.Master.ToString(),
                    Token = token,
                };
                
                return Ok(usuarioAutenticado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro alo tentar logar" });
            }
        }

        [HttpPost("validate")]
        public async Task<ActionResult<User>> ValidarToken([FromBody] User model)
        {
            // Lógica para validar o token JWT
            User user = TokenService.ValidarTokenJWT(model.Token);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(new { mensagem = "Token inválido" });
            }
        }

    }
}