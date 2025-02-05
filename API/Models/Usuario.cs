using System.ComponentModel.DataAnnotations;

namespace DesafioERP.API.Models
{
    public class Usuario
    
    {
        [Key]
        public required string CPF { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public required string Senha { get; set; }
        public required string Login {get; set;}

        public Usuario() { }

        public Usuario(string cpf, string nome, string email, string endereco, string telefone, string senha, string login)
        {
            CPF = cpf;
            Nome = nome;
            Email = email;
            Endereco = endereco;
            Telefone = telefone;
            Senha = senha;
            Login = login;
        }

    }
}
