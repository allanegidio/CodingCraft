using Lojinha.MVC.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lojinha.MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CodingCraft_Lojinha", throwIfV1Schema: false)
        {
            // Para gerar SQL no Debug Output
            //Database.Log = s => Debug.WriteLine(s);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ProdutoFornecedor> ProdutosFornecedores { get; set; }
        public DbSet<ProdutoLoja> ProdutosLojas { get; set; }
        public DbSet<CompraFornecedor> ComprasFornecedores { get; set; }
        public DbSet<CompraFornecedorProduto> ComprasFornecedoresProdutos { get; set; }
        public DbSet<VendaLoja> VendasLojas { get; set; }
        public DbSet<VendaLojaProduto> VendasLojasProdutos { get; set; }
        public DbSet<Contabilidade> Contabilidades { get; set; }

        public override int SaveChanges()
        {
            try
            {
                CheckEntities();

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(v => v.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                CheckEntities();

                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(v => v.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionsMessage, ex.EntityValidationErrors);
            }
        }

        private void CheckEntities()
        {
            var currentTime = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity != null &&
                     typeof(IEntidadeNaoEditavel).IsAssignableFrom(e.Entity.GetType())))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property(nameof(IEntidadeNaoEditavel.DataCriacao)) != null)
                    {
                        entry.Property(nameof(IEntidadeNaoEditavel.DataCriacao)).CurrentValue = currentTime;
                    }

                    if (entry.Property(nameof(IEntidadeNaoEditavel.UsuarioCriacao)) != null)
                    {
                        entry.Property(nameof(IEntidadeNaoEditavel.UsuarioCriacao)).CurrentValue = HttpContext.Current != null
                                                                                                    ? HttpContext.Current.User.Identity.Name
                                                                                                    : "Usuario";
                    }
                }

                if (typeof(IEntidade).IsAssignableFrom(entry.Entity.GetType()) && entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(IEntidadeNaoEditavel.DataCriacao)).IsModified = false;
                    entry.Property(nameof(IEntidadeNaoEditavel.UsuarioCriacao)).IsModified = false;

                    if (entry.Property(nameof(IEntidade.DataModificacao)) != null)
                    {
                        entry.Property(nameof(IEntidade.DataModificacao)).CurrentValue = currentTime;
                    }

                    if (entry.Property(nameof(IEntidade.UsuarioModificacao)) != null)
                    {
                        entry.Property(nameof(IEntidade.UsuarioModificacao)).CurrentValue = HttpContext.Current != null
                                                                                            ? HttpContext.Current.User.Identity.Name
                                                                                            : "Usuario";
                    }
                }
            }
        }
    }
}