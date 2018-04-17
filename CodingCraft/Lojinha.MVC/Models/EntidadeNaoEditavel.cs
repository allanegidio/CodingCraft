using Lojinha.MVC.Models.Interfaces;
using System;

namespace Lojinha.MVC.Models
{
    public class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        public virtual DateTime DataCriacao { get; set; }
        public virtual string UsuarioCriacao { get; set; }
    }
}