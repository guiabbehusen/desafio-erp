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
            var usuario = await _dbContext.Usuarios
                .Include(u => u.Enderecos)
                .FirstOrDefaultAsync(x => x.Login == login);

            if (usuario == null)
            {
                throw new Exception("Login inválido.");
            }

            return usuario;
        }

        public async Task<Usuario> BuscaPorCPF(string cpf)
        {
            return await _dbContext.Usuarios
            .Include(u => u.Enderecos)
            .FirstOrDefaultAsync(x => x.CPF == cpf)
                ?? throw new Exception($"Usuário com CPF {cpf} não encontrado.");
        }

        public async Task<Usuario> ApagarUsuario(string CPF)
        {
            var usuario_busca = await BuscaPorCPF(CPF);
            if (usuario_busca == null)
            {
                throw new Exception($"Usuario de CPF: {CPF} não encontrado.");
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

        public async Task<Usuario> EditarUsuario(Usuario usuario1, string CPF)
        {
            var usuario_busca = await BuscaPorCPF(CPF);
            if (usuario_busca == null)
            {
                throw new Exception($"Usuario para o CPF: {CPF} Não foi encontrado.");
            }
            
            usuario_busca.Nome = usuario1.Nome;
            usuario_busca.Email = usuario1.Email;
            usuario_busca.Telefone = usuario1.Telefone;

            usuario_busca.Enderecos = usuario1.Enderecos;

            _dbContext.Usuarios.Update(usuario_busca);
            await _dbContext.SaveChangesAsync();
            return usuario_busca;

        }
    }
}
