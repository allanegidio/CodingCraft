using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lojinha.MVC.Models
{
    public class CompraCliente : Entidade
    {
        [Key]
        public int CompraClienteId { get; set; }

        public DateTime Data { get; set; }

        public virtual ICollection<CompraClienteProduto> CompraClienteProdutos { get; set; }
    }
}