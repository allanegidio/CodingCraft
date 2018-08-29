using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Estampas")]
    public class Estampa
    {
        [Key]
        public int EstampaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Produtos_Nome", IsUnique = true)]
        public string Nome { get; set; }
    }
}