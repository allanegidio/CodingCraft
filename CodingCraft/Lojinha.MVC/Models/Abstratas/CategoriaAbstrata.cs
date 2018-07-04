using Lojinha.MVC.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.MVC.Models.Abstratas
{
    public abstract class CategoriaAbstrata : IEntidadeNaoEditavel
    {
        
        public virtual int CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IX_Categoria_Nome", IsUnique = true)]
        public virtual string Nome { get; set; }

        public virtual DateTime DataCriacao { get; set; }

        public virtual string UsuarioCriacao { get; set; }
    }
}