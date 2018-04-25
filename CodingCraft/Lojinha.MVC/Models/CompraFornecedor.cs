using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ComprasFornecedores")]
    public class CompraFornecedor : Entidade
    {
        [Key]
        public int CompraFornecedorId { get; set; }

        public int FornecedorId { get; set; }

        public decimal LastroTotal { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual ICollection<CompraFornecedorProduto> CompraFornecedorProdutos { get; set; }

    }
}