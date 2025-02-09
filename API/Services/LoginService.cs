using System;
using System.Text;
using System.Threading.Tasks;
using CryptoMJ;
using CryptoMJ.Methods;
using DesafioERP.API.Models;
using DesafioERP.Repositorios;

namespace DesafioERP.API.Services
{
    public class LoginService
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public LoginService(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public string CriptografarSenha(string senha)
        {
            string chave = "senha_chave";
            try
            {
                ICrypto crypto = new CryptoBuilder().Build();
                string senha_criptografada = crypto.Encrypt(senha, chave);
                return senha_criptografada;
            }
            catch (Exception ex)
            {
                return $"Erro ao criptografar: {ex.Message}";
            }
        }

        public async Task<bool> ValidarLogin(string usuario_fornecido, string senha_fornecida)
        { 
            var usuario = await _usuarioRepositorio.BuscaPorLogin(usuario_fornecido);

            if (usuario == null)
            {
                return false; 
            }

            string senha_criptografada = CriptografarSenha(senha_fornecida);

            return usuario.Senha == senha_criptografada;
        }
    }
}
