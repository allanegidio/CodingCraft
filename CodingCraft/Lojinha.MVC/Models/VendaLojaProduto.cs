using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("VendasLojasProdutos")]
    public class VendaLojaProduto : Entidade
    {
        [Key]
        public int VendaLojaProdutoId { get; set; }

        public int? ProdutoLojaId { get; set; }

        public int VendaLojaId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public virtual ProdutoLoja ProdutoLoja { get; set; }

        public virtual VendaLoja VendaLoja { get; set; }
    }
}