using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IProcurador : IPapelPessoa
    {
        string MatriculaAPI { get; set; }
        string SiglaOrgaoProfissional { get; set; }
        string NumeroRegistroProfissional { get; set; }
        DateTime? DataRegistroProfissional { get; set; }
        string ObservacaoContato { get; set; }
    }
}
