using System.Text.RegularExpressions;
using DesafioERP.API.Models;
using DesafioERP.Repositorios;
using DesafioERP.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioERP.API.Services
{
    public class UsuarioService
    {
        private readonly LoginService _loginService;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(LoginService loginService, UsuarioRepositorio usuarioRepositorio)
        {
            _loginService = loginService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public List<string> ValidarCadastro(Usuario usuario)
        {
            var erros = new List<string>();

            var usuarioExistente = _usuarioRepositorio.BuscaPorCPF(usuario.CPF).Result;
            if (usuarioExistente != null)
            {
                erros.Add("Já existe um usuário com este CPF cadastrado.");
            }

            if (!ValidarCPF(usuario.CPF))
                erros.Add("CPF inválido.");

            if (!ValidarNome(usuario.Nome))
                erros.Add("O nome deve conter pelo menos 4 caracteres e conter apenas letras e espaços.");

            if (!ValidarEmail(usuario.Email))
                erros.Add("E-mail inválido.");

            if (!ValidarTelefone(usuario.Telefone))
                erros.Add("Telefone inválido.");

            if (!ValidarSenha(usuario.Senha))
                erros.Add("Senha inválida. A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.");

            foreach (var endereco in usuario.Enderecos)
            {
                if (!ValidarEndereco(endereco.Rua, endereco.Numero, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP))
                {
                    erros.Add("Endereço inválido. Todos os campos devem ser preenchidos e o CEP deve estar no formato correto.");
                }
            }

            return erros;
        }


        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            return true;
        }

        public bool ValidarNome(string nome)
        {
            if (nome.Length < 4 || !Regex.IsMatch(nome, @"^[a-zA-Z\s]+$"))
                return false;
            return true;
        }

        public bool ValidarEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return false;
            return true;
        }

        public bool ValidarTelefone(string telefone)
        {
            if (!Regex.IsMatch(telefone, @"^\(\d{2}\) \d{5}-\d{4}$"))
                return false;
            return true;
        }

        public bool ValidarSenha(string senha)
        {
            if (!Regex.IsMatch(senha, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
                return false;
            _loginService.CriptografarSenha(senha);
            return true;
        }

        public bool ValidarEndereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
        {
            if (string.IsNullOrEmpty(rua) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(bairro) ||
                string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(cep))
            {
                return false;
            }

            if (!Regex.IsMatch(cep, @"^\d{5}-\d{3}$"))
            {
                return false;
            }

            if (estado.Length != 2)
            {
                return false;
            }

            return true;
        }

        public async Task<Usuario> EditarUsuario(Usuario usuario, string CPF)
        {
            var usuarioExistente = await _usuarioRepositorio.BuscaPorCPF(CPF);
            if (usuarioExistente == null)
            {
                return null;
            }
            try
            {
                ValidarNome(usuario.Nome);
                usuarioExistente.Nome = usuario.Nome;
            }
            catch (System.Exception)
            {

                throw new Exception("Nome inválido. Deve conter no mínimo 4 caracteres.");
            }
            try
            {
                ValidarEmail(usuario.Email);
                usuarioExistente.Email = usuario.Email;
            }
            catch (System.Exception)
            {

                throw new Exception("Email inválido. Ex: email@email.com");
            }
            try
            {
                ValidarTelefone(usuario.Telefone);
                usuarioExistente.Telefone = usuario.Telefone;
            }
            catch (System.Exception)
            {

                throw new Exception("Telefone inválido. Ex: (12) 93456-7890");
            }

            return usuarioExistente;
        }

    }
}
