using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Lojinha.MVC.Models
{
    [Table("VendasLojas")]
    public class VendaLoja : Entidade
    {
        [Key]
        public int VendaLojaId { get; set; }

        public int LojaId { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalVenda => VendaLojaProdutos?.Sum(vlp => vlp.Total) ?? 0;

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public virtual Loja Loja { get; set; }

        public virtual ICollection<VendaLojaProduto> VendaLojaProdutos { get; set; }
    }
}