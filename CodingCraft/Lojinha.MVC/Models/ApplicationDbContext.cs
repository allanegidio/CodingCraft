using Lojinha.MVC.Models.Auditoria;
using Lojinha.MVC.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        //Auditorias
        public DbSet<CategoriaAuditoria> CategoriasAuditorias { get; set; }

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

            foreach(var entidade in ChangeTracker.Entries().Where(e => e.Entity != null))
            {
                var tipoTabelaAuditoria = entidade.Entity.GetType().GetInterfaces()[1].GenericTypeArguments[0];
                var registroTabelaAuditoria = Activator.CreateInstance(tipoTabelaAuditoria);
                
                if (entidade.State == EntityState.Added)
                {
                    if (entidade.Property(nameof(IEntidadeNaoEditavel.DataCriacao)) != null)
                    {
                        entidade.Property(nameof(IEntidadeNaoEditavel.DataCriacao)).CurrentValue = currentTime;
                    }

                    if (entidade.Property(nameof(IEntidadeNaoEditavel.UsuarioCriacao)) != null)
                    {
                        entidade.Property(nameof(IEntidadeNaoEditavel.UsuarioCriacao)).CurrentValue = HttpContext.Current != null
                                                                                                    ? HttpContext.Current.User.Identity.Name
                                                                                                    : "Usuario";
                    }

                    base.SaveChanges();

                    foreach (var propriedade in entidade.Entity.GetType().BaseType.GetProperties())
                    {
                        registroTabelaAuditoria.GetType()
                                               .GetProperty(propriedade.Name)
                                               .SetValue(registroTabelaAuditoria, entidade.Entity.GetType().GetProperty(propriedade.Name).GetValue(entidade.Entity, null));
                    }

                    Set(registroTabelaAuditoria.GetType()).Add(registroTabelaAuditoria);                    
                }

                if (entidade.State == EntityState.Modified)
                {
                    var tipoTabelaDb = Set(registroTabelaAuditoria.GetType());
                    //var modelAuditoria = //tipoTabelaDb.Single(auditoria => auditoria.????Id == entidade.?????Id)

                    if ((registroTabelaAuditoria as DbEntityEntry).Property(nameof(IEntidade.DataModificacao)) != null)
                    {
                        (registroTabelaAuditoria as DbEntityEntry).Property(nameof(IEntidade.DataModificacao)).CurrentValue = currentTime;
                    }

                    if ((registroTabelaAuditoria as DbEntityEntry).Property(nameof(IEntidade.UsuarioModificacao)) != null)
                    {
                        (registroTabelaAuditoria as DbEntityEntry).Property(nameof(IEntidade.UsuarioModificacao)).CurrentValue = HttpContext.Current != null
                                                                                                                                    ? HttpContext.Current.User.Identity.Name
                                                                                                                                    : "Usuario";
                    }
                }                
            }
        }
    }
}