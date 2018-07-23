using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public Guid CategoriaId { get; set; }

        [Required]
        [Index("IUQ_Produtos_Nome", IsUnique = true)]
        [StringLength(100)]
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}