using DesafioERP.API.Models;

namespace DesafioERP.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> CriarUsuario(Usuario usuario1);
        Task<Usuario> ApagarUsuario(string CPF);
        Task<Usuario> BuscaPorCPF(string CPF);
        Task<Usuario> BuscaPorLogin(string login);
        Task<Usuario> AtualizarUsuario(Usuario usuario, string cpf);
    }
}
