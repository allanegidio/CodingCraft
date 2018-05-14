using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Fornecedores")]
    public class Fornecedor : Entidade
    {
        [Key]
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Fornecedor_Nome", IsUnique = true)]
        public string Nome { get; set; }

        public virtual ICollection<ProdutoFornecedor> ProdutosFornecedor { get; set; }

        public virtual ICollection<CompraFornecedor> FornecedorCompras { get; set; }
    }
}