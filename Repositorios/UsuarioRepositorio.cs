using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Contexto _dbContext;

        public UsuarioRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsuarioModel>> GetAll()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        public async Task<UsuarioModel> InsertUsuario(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarios = await GetById(id);
            if(usuario == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                usuario.UsuarioNome = usuario.UsuarioNome;
                usuario.UsuarioTelefone = usuario.UsuarioTelefone;
                usuario.UsuarioEmail = usuario.UsuarioEmail;
                usuario.UsuarioSenha = usuario.UsuarioSenha;
                _dbContext.Usuarios.Update(usuario);
                await _dbContext.SaveChangesAsync();
            }
            return usuario;
           
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            UsuarioModel usuario = await GetById(id);
            if (usuario == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();
            return true;
        }    
       
    }
}
