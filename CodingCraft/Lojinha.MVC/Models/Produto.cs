using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Produtos")]
    public class Produto : Entidade
    {
        [Required]
        [StringLength(200)]
        [Index("IX_Produtos_Nome", IsUnique = true)]
        public string Nome { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Estoque> Estoques { get; set; }
    }
}