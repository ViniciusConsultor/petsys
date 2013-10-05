using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IInventor : IPapelPessoa
    {
        Nullable<DateTime> DataDoCadastro { get; set; }
        string InformacoesAdicionais { get; set; }
    }
}
