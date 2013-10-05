using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class Inventor: PapelPessoa, IInventor
    {
        public Inventor(IPessoa pessoa) : base(pessoa)
        {
        }

        public DateTime? DataDoCadastro
        {
            get;
            set;
        }

        public string InformacoesAdicionais
        {
            get; set; 
        }
    }
}
