using System.ComponentModel.DataAnnotations;

namespace Lojinha.MVC.Models
{
    public class ProdutoFornecedor : Produto
    {

        [Key]
        public int ProdutoFornecedorId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public int CompraFornecedorProdutoId { get; set; }

        public CompraFornecedorProduto CompraFornecedorProduto { get; set; }
    }
}