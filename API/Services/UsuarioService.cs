using System.Text.RegularExpressions;

namespace DesafioERP.API.Services
{

    public class UsuarioService
    {


        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
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

            return true;
        }
    }
}