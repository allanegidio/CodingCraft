using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("CompraFornecedor")]
    public class CompraFornecedor : Entidade
    {
        [Key]
        public int CompraFornecedorId { get; set; }

        public decimal Lastro { get; set; }
        public DateTime Data { get; set; }
        
        public decimal Prejuizo { get; private set; }
        public decimal Lucro { get; private set; }

        public virtual ICollection<CompraFornecedorProduto> Produtos { get; set; }
    }
}