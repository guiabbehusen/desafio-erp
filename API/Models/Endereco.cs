namespace DesafioERP.API.Models

{
    
    public class Endereco{
        public Endereco()
        {
        }

        public required int Id {get; set;}
        public required string Rua {get;set;}
        public required string Numero { get; set; }
        public required string Bairro {get; set;}
        public required string Cidade {get; set;}
        public required string Estado {get; set;}
        public required string CEP {get; set;}

        public required string UsuarioCPF {get; set;}
        public required Usuario Usuario {get; set;}
        
        public Endereco(int id, string rua, string numero, string bairro, string cidade, string estado, string cep, string usuarioCpf, Usuario usuario)
        {
            Id = id;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            UsuarioCPF = usuarioCpf;
            Usuario = usuario;
        }

    }

    
}