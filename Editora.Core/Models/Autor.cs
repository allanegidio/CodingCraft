using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Editora.Core.Models
{
    [Table("Autores")]
    public class Autor
    {
        [Key]
        public int AutorId { get; set; }

        [StringLength(300)]
        [Index("IUQ_Autores_Nome", IsUnique = true)]
        public string Nome { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }
    }
}