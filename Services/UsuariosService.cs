using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agendamentosmanager_api.DTO;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Services
{
    public class UsuariosService
    {
        private readonly AgendamentobotContext _dbContext;
        public UsuariosService(AgendamentobotContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UsuarioDTO>> GetAll()
        {
            try
            {
                List<Usuario> usuarios = await _dbContext.Usuarios.AsNoTracking().ToListAsync();

                List<UsuarioDTO> retorno = usuarios.Select(x => new UsuarioDTO
                {
                    Id = x.Id,
                    Perfil = x.Perfil,
                    Nome = x.Nome,
                    Cpfcnpj = x.Cpfcnpj,
                    Senha = "",
                    Master = x.Master,
                    Status = x.Ativo == true ? "Ativo" : "Inativo",
                    Deletado = x.Deletado

                }).ToList();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<UsuarioDTO> Insert(UsuarioDTO model)
        {
            var existeUsuario = await _dbContext.Usuarios.Where(x => x.Cpfcnpj.Trim() == model.Cpfcnpj.Trim() && x.Deletado != true).FirstOrDefaultAsync();
            if(existeUsuario != null)
                throw new ArgumentException("Já existe um usuário cadastrado com esse CFP/CNPJ");

            Usuario usuario = new Usuario
            {
                Perfil = model.Perfil,
                Nome = model.Nome,
                Cpfcnpj = model.Cpfcnpj.Trim().Replace("/","").Replace("-","").Replace(".",""),
                Senha = model.Senha.Trim(),
                Master = model.Master,
                Ativo = model.Status == "Ativo" ? true : false,
                Deletado = false
            };

            await _dbContext.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            
            return model;
        }

        public async Task<UsuarioDTO> Update(UsuarioDTO model)
        {
            try
            {
                var existUsuario = await _dbContext.Usuarios.Where(u => u.Id == model.Id).FirstOrDefaultAsync();
                if (existUsuario == null)
                    throw new ArgumentException("Erro ao atualizar, o usuário não existe!");

                var existUsuarioEmail = await _dbContext.Usuarios.Where(x => x.Cpfcnpj == model.Cpfcnpj.Trim() && x.Id != model.Id && x.Deletado != true).FirstOrDefaultAsync();
                if (existUsuarioEmail != null)
                    throw new ArgumentException("Já existe um usuário cadastrado com esse e-mail!");

                existUsuario.Perfil = model.Perfil;
                existUsuario.Nome = model.Nome;
                existUsuario.Master = model.Master;
                existUsuario.Ativo = model.Status == "Ativo" ? true : false;

                if(!string.IsNullOrEmpty(model.Senha))
                    existUsuario.Senha = model.Senha;
                
                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task Delete(long id)
        {
            try
            {
                var usuario = await _dbContext.Usuarios.Where(u => u.Id == id).FirstOrDefaultAsync();
                if (usuario == null)
                    throw new ArgumentException("O usuário não existe!");

                usuario.Deletado = true;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}