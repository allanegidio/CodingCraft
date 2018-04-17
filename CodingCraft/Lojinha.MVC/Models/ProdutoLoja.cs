using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ProdutosLojas")]
    public class ProdutoLoja : Entidade
    {
        [Key]
        public int ProdutoLojaId { get; set; }

        [Index("UIX_ProdutosLojas_ProdutoId_LojaId", IsUnique = true, Order = 1)]
        public int ProdutoId { get; set; }

        [Index("UIX_ProdutosLojas_ProdutoId_LojaId", IsUnique = true, Order = 2)]
        public int LojaId { get; set; }

        [Required]
        public int Estoque { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Loja Loja { get; set; }
    }
}