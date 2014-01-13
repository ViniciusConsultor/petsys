using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Titular : PapelPessoa, ITitular
    {
        public Titular(IPessoa pessoa) : base(pessoa) { }

        public DateTime? DataDoCadastro { get; set; }

        public string InformacoesAdicionais { get; set; }
    }
}
