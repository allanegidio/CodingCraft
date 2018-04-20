using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Estoque")]
    public class Estoque : Entidade
    {
        [Key]
        public int EstoqueId { get; set; }

        [Index("UIX_Estoque_ProdutoId_LojaId", IsUnique = true, Order = 1)]
        public int ProdutoId { get; set; }

        [Index("UIX_Estoque_ProdutoId_LojaId", IsUnique = true, Order = 2)]
        public int LojaId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Loja Loja { get; set; }
    }
}