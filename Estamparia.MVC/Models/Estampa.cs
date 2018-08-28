using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Estampas")]
    public class Estampa
    {
        [Key]
        public int EstampaId { get; set; }

        public string Nome { get; set; }
    }
}