using System;

namespace Lojinha.MVC.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        string UsuarioCriacao { get; set; }
    }
}
