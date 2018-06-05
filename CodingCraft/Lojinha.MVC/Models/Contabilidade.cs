using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lojinha.MVC.Models
{
    public class Contabilidade : Entidade
    {
        public int ContabilidadeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        public decimal Lastro => VendasLojas?.Sum(vl => vl.TotalVenda) - ComprasFornecedores?.Sum(cf => cf.LastroTotal) ?? 0;

        public virtual ICollection<VendaLoja> VendasLojas { get; set; }

        public virtual ICollection<CompraFornecedor> ComprasFornecedores { get; set; }
    }
}