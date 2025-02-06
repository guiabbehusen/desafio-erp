using System.ComponentModel.DataAnnotations;

namespace DesafioERP.API.Models
{
    public class Usuario
    {
        [Key]
        public required string CPF { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string Senha { get; set; }
        public required string Login { get; set; }
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}