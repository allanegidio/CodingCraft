﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("ComprasFornecedoresProdutos")]
    public class CompraFornecedorProduto : Entidade
    {
        [Key]
        public int CompraFornecedorProdutoId { get; set; }

        public int? ProdutoFornecedorId { get; set; }

        public int CompraFornecedorId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total => ProdutoFornecedor?.Preco * Quantidade ?? 0;

        public virtual ProdutoFornecedor ProdutoFornecedor { get; set; }

        public virtual CompraFornecedor CompraFornecedor { get; set; }

    }
}