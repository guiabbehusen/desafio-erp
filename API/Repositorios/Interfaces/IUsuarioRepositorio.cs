using DesafioERP.API.Models;

namespace DesafioERP.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> CriarUsuario(Usuario usuario1);
        Task<Usuario> EditarUsuario(Usuario usuario1, string CPF);
        Task<Usuario> ApagarUsuario(string CPF);
        Task<Usuario> BuscaPorCPF(string CPF);
        Task<Usuario> BuscaPorLogin(string login);
    }
}
