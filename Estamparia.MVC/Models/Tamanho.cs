using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estamparia.MVC.Models
{
    [Table("Tamanhos")]
    public class Tamanho
    {
        [Key]
        public byte TamanhoId { get; set; }
        
        public string Nome { get; set; }
    }
}