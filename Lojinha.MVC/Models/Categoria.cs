using Lojinha.MVC.Models.Abstratas;
using Lojinha.MVC.Models.Auditoria;
using Lojinha.MVC.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Categorias")]
    public class Categoria : CategoriaAbstrata, IAuditavel<CategoriaAuditoria>
    {
        [Key]
        public override int CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Categoria_Nome", IsUnique = true)]
        public override string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}