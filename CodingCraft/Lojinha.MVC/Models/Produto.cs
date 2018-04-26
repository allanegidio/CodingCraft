using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Produtos")]
    public class Produto : Entidade
    {
        [Key]
        public int ProdutoId { get; set; }

        public int CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Produtos_Nome", IsUnique = true)]
        public string Nome { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<ProdutoLoja> ProdutoLojas { get; set; }

        public virtual ICollection<ProdutoFornecedor> ProdutoFornecedores { get; set; }
    }
}