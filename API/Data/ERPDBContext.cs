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
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);

        }
}
}