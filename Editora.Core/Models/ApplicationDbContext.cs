using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Editora.Core.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CodingCraft_Editora", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Autor> Autores { get; set; }

        public DbSet<Obra> Obras { get; set; }
    }
}