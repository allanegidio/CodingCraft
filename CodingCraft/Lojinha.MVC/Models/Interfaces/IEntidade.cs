using System;

namespace Lojinha.MVC.Models.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime? DataModificacao { get; set; }
        string UsuarioModificacao { get; set; }
    }
}
