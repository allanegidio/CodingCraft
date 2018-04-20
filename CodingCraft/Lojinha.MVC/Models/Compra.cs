using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models
{
    [Table("Compras")]
    public class Compra : Entidade
    {
        [Key]
        public int CompraId { get; set; }

        public decimal Lastro { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; private set; }
        public bool ExistePrejuizo { get; private set; }
        public decimal LastroDeLucro { get; }

        public virtual ICollection<Produto> Produtos { get; set; }

        public void CalcularPrejuizo()
        {
            if (Total > Lastro)
                ExistePrejuizo = true;
        }

        public void CalcularLucro()
        {
            foreach(var produto in Produtos)
            {
                Total += produto.Preco;
            }
        }
    }
}