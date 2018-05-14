using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Lojas")]
    public class Loja : Entidade
    {
        [Key]
        public int LojaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Loja_Nome", IsUnique = true)]
        public string Nome { get; set; }

        public virtual ICollection<ProdutoLoja> ProdutosLoja { get; set; }

        public virtual ICollection<VendaLoja> VendasLoja { get; set; }
    }
}