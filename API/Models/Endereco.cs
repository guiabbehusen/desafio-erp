using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioERP.API.Models
{
    public class Endereco
    {
        public Endereco() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string CEP { get; set; }
        public required string Rua { get; set; }
        public required string Numero { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string UsuarioCPF { get; set; }

        public Endereco(int id, string cep, string rua, string numero, string bairro, string cidade, string estado, string usuarioCPF)
        {
            CEP = cep;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            UsuarioCPF = usuarioCPF;
        }
    }
}