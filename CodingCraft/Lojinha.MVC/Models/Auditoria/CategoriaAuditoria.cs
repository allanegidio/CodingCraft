using Lojinha.MVC.Models.Abstratas;
using Lojinha.MVC.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models.Auditoria
{
    [Table("CategoriasAuditoria")]
    public class CategoriaAuditoria : CategoriaAbstrata, IEntidade
    {
        [Key]
        public int CategoriaAuditoriaId { get; set; }

        public virtual DateTime? DataModificacao { get; set; }

        public virtual string UsuarioModificacao { get; set; }
    }
}