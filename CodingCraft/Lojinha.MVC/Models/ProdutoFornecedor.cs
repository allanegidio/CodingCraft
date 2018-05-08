using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ProdutosFornecedores")]
    public class ProdutoFornecedor : Entidade
    {
        [Key]
        public int ProdutoFornecedorId { get; set; }

        [Index("UIX_ProdutosFornecedores_ProdutoId_FornecedorId", IsUnique = true, Order = 1)]
        public int ProdutoId { get; set; }

        [Index("UIX_ProdutosFornecedores_ProdutoId_FornecedorId", IsUnique = true, Order = 2)]
        public int FornecedorId { get; set; }

        public decimal Preco { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual Produto Produto { get; set; }

    }
}