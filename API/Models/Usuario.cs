using System.ComponentModel.DataAnnotations;

namespace DesafioERP.API.Models
{
    public class Usuario
    {
        public string CPF { get; set; }


        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "O nome deve conter apenas letras e espaços.")]
        [MinLength(4, ErrorMessage ="O nome deve ter pelo menos 4 caracteres")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        
        public Usuario(string cpf, string nome, string email, string telefone, string senha)
        {
            CPF = cpf;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Senha = senha;
        }
    }
}
    

