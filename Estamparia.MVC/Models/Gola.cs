using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Golas")]
    public class Gola
    {
        [Key]
        public byte GolaId { get; set; }

        public string Nome { get; set; }
    }
}