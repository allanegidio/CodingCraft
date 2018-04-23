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

        public decimal Lastro { get; set; }

        public decimal Lucro { get; set; }

        public decimal Prejuizo { get; set; }

        public DateTime Data { get; set; }

        public decimal TotalCompra { get; set; }

        public virtual ICollection<CompraFornecedorProduto> CompraFornecedorProdutos { get; set; }

        public void CalcularTotalCompra()
        {
            foreach(var produto in CompraFornecedorProdutos)
            {
                TotalCompra += produto.Total;
            }
        }

    }
}