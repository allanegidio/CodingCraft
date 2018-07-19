using Lojinha.MVC.Models.Abstratas;
using Lojinha.MVC.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models.Auditoria
{
    [Table("CategoriasAuditoria")]
    public sealed class CategoriaAuditoria : CategoriaAbstrata, IEntidadeAuditoria
    {
        [Key]
        public int CategoriaAuditoriaId { get; set; }

        public DateTime DataModificacao { get; set; }

        public string UsuarioModificacao { get; set; }
    }
}