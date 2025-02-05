using System.ComponentModel.DataAnnotations;

namespace DesafioERP.API.Models
{
    public class Usuario
    
    {
        [Key]
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Senha { get; set; }

        public Usuario() { }

        public Usuario(string cpf, string nome, string email, string endereco, string telefone, string senha)
        {
            CPF = cpf;
            Nome = nome;
            Email = email;
            Endereco = endereco;
            Telefone = telefone;
            Senha = senha;
        }
    }
}
