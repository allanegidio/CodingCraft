using System;

namespace Lojinha.MVC.Models.Interfaces
{
    public interface IEntidadeAuditoria
    {

        DateTime DataModificacao { get; set; }

        string UsuarioModificacao { get; set; }
    }
}