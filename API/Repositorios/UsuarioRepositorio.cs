using DesafioERP.API.Models;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Data;

namespace DesafioERP.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ERPDBContext _dbContext;

        public UsuarioRepositorio(ERPDBContext erpdbcontext)
        {
            _dbContext = erpdbcontext;
        }

        public async Task<Usuario> BuscaPorLogin(string login)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<Usuario> BuscaPorCPF(string cpf)
        {
            return await _dbContext.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(x => x.CPF == cpf);
        }



        public async Task<Usuario> ApagarUsuario(string CPF)
        {
            var usuario_busca = await BuscaPorCPF(CPF);
            if (usuario_busca == null)
            {
                throw new Exception($"Usuário de CPF: {CPF} não encontrado.");
            }
            _dbContext.Remove(usuario_busca);
            await _dbContext.SaveChangesAsync();

            return usuario_busca;
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario1)
        {
            await _dbContext.Usuarios.AddAsync(usuario1);
            await _dbContext.SaveChangesAsync();
            return usuario1;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario, string cpf)
        {

            var usuarioExistente = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.CPF == cpf);

            usuarioExistente = usuario;

            _dbContext.Usuarios.Update(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            return usuarioExistente;
        }

    }
}
