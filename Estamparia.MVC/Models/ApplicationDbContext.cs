using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Estamparia.MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Tamanho> Tamanhos { get; set; }

        public DbSet<Gola> Golas { get; set; }

        public DbSet<Estampa> Estampas { get; set; }

        public ApplicationDbContext()
            : base("CodingCraft_Estamparia", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}