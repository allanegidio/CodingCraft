﻿using Lojinha.MVC.Models.Interfaces;
using System;

namespace Lojinha.MVC.Models
{
    public abstract class Entidade : EntidadeNaoEditavel, IEntidade
    {
        public DateTime? DataModificacao { get; set; }
        public string UsuarioModificacao { get; set; }
    }
}