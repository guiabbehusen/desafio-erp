using DesafioERP.API.Models;

namespace DesafioERP.Repositorios.Interfaces
{
    public interface IEnderecoRepositorio
    {
        Task<Endereco> AdicionarEndereco(Endereco endereco);
        Task<Endereco> BuscarEnderecoPorCEP(string CEP);
        Task<Endereco> EditarEndereco(Endereco endereco);
        Task<Endereco> DeletarEndereco(string CPF, string CEP);
        Task<List<Endereco>> BuscarEnderecosPorCPF(string CPF);
    }
}
