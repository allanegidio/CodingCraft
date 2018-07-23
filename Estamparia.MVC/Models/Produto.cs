using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public Guid ProdutoId { get; set; }

        public Guid CategoriaId { get; set; }

        [Required]
        [Index("IUQ_Produtos_Nome", IsUnique = true)]
        [StringLength(100)]
        public string Nome { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}