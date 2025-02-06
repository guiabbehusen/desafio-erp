using DesafioERP.API.Models;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Data;

namespace DesafioERP.Repositorios
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private readonly ERPDBContext _dbContext;

        public EnderecoRepositorio(ERPDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Endereco> AdicionarEndereco(Endereco endereco)
        {
            if (endereco.UsuarioCPF == null)  
            {
                throw new Exception("O CPF do usuário é necessário para adicionar o endereço.");
            }

            var usuarioExistente = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CPF == endereco.UsuarioCPF);  
            if (usuarioExistente == null)
            {
                throw new Exception($"Usuário com CPF {endereco.UsuarioCPF} não encontrado.");
            }

            await _dbContext.Enderecos.AddAsync(endereco);
            await _dbContext.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> BuscarEnderecoPorCEP(string CEP)
        {
            var endereco = await _dbContext.Enderecos.FirstOrDefaultAsync(e => e.CEP == CEP);
            if (endereco == null)
            {
                throw new Exception($"Endereço com o CEP {CEP} não encontrado.");
            }
            return endereco;
        }

        public async Task<Endereco> EditarEndereco(Endereco endereco)
        {
            var enderecoExistente = await _dbContext.Enderecos.FindAsync(endereco.CEP);
            if (enderecoExistente == null)
                throw new Exception($"Endereço com CEP {endereco.CEP} não encontrado.");

            enderecoExistente.Rua = endereco.Rua;
            enderecoExistente.Numero = endereco.Numero;
            enderecoExistente.Bairro = endereco.Bairro;
            enderecoExistente.Cidade = endereco.Cidade;
            enderecoExistente.Estado = endereco.Estado;

            _dbContext.Enderecos.Update(enderecoExistente);
            await _dbContext.SaveChangesAsync();

            return enderecoExistente;
        }

        public async Task<Endereco> DeletarEndereco(string CEP)
        {
            var endereco = await _dbContext.Enderecos.FirstOrDefaultAsync(e => e.CEP == CEP);
            if (endereco == null)
                throw new Exception($"Endereço com CEP {CEP} não encontrado.");

            _dbContext.Enderecos.Remove(endereco);
            await _dbContext.SaveChangesAsync();

            return endereco;
        }

        public async Task<List<Endereco>> BuscarEnderecosPorCPF(string CPF)
        {
            return await _dbContext.Enderecos
                .Where(e => e.UsuarioCPF == CPF)  
                .ToListAsync();
        }
    }
}
