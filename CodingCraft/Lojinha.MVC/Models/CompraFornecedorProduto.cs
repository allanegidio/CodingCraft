using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ProdutoFornecedor")]
    public class CompraFornecedorProduto : Entidade
    {
        [Key]
        public int CompraFornecedorProdutoId { get; set; }

        public int ProdutoFornecedorId { get; set; }

        public int Quantidade { get; set; }

        public decimal Total => ProdutoFornecedor.Preco * Quantidade;

        public ProdutoFornecedor ProdutoFornecedor { get; set; }
    }
}