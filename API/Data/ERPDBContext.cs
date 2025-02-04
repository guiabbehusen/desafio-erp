using DesafioERP.API.Models;
using Microsoft.EntityFrameworkCore;


namespace SistemaTarefas.Data
{
    public class ERPDBContext : DbContext{
    public ERPDBContext(DbContextOptions<ERPDBContext> options)
        :  base(options){

        }

        public DbSet<Usuario> Usuarios {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
}
}