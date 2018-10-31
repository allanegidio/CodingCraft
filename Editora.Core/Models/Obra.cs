using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Editora.Core.Models
{
    [Table("Obras")]
    public class Obra
    {
        [Key]
        public int ObraId { get; set; }

        public int AutorId { get; set; }

        [Required]
        [StringLength(500)]
        [Index("IUQ_Obras_Titulo", IsUnique = true)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        public virtual Autor Autor { get; set; }
    }
}