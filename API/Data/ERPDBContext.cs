using DesafioERP.API.Models;
using DesafioERP.Data.Map;
using Microsoft.EntityFrameworkCore;


namespace SistemaTarefas.Data
{
    public class ERPDBContext : DbContext{
    public ERPDBContext(DbContextOptions<ERPDBContext> options)
        :  base(options){

        }

        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            base.OnModelCreating(modelBuilder);

        }
}
}