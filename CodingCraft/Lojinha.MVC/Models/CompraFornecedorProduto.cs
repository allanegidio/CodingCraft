using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ComprasFornecedoresProdutos")]
    public class CompraFornecedorProduto
    {
        [Key]
        public int CompraFornecedorProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public decimal Total { get; set; }

        public int ProdutoFornecedorId { get; set; }

        public virtual ProdutoFornecedor ProdutoFornecedor { get; set; }

    }
}