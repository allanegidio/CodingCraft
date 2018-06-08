using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Lojinha.MVC.Models
{
    [Table("ComprasFornecedores")]
    public class CompraFornecedor : Entidade
    {
        [Key]
        public int CompraFornecedorId { get; set; }

        public int FornecedorId { get; set; }

        public int ContabilidadeId { get; set; }

        [DataType(DataType.Currency)]
        public decimal LastroTotal => CompraFornecedorProdutos?.Sum(cfp => cfp.Total) ?? 0;

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual Contabilidade Contabilidade { get; set; }

        public virtual ICollection<CompraFornecedorProduto> CompraFornecedorProdutos { get; set; }

    }
}