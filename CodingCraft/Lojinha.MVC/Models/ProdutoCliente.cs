using System.ComponentModel.DataAnnotations;

namespace Lojinha.MVC.Models
{
    public class ProdutoCliente : Produto
    {
        [Key]
        public int ProdutoClienteId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
    }
}