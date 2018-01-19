using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Produtos_Nome", IsUnique = true)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<ProdutoLoja> ProdutoLoja { get; set; }

    }
}