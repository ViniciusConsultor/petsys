using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.Documento;
using Compartilhados.Interfaces.Core.Negocio.Telefone;
using Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class Procurador : PapelPessoa, IProcurador
    {
        private IPessoa pessoa;

        public Procurador(IPessoa Pessoa) : base(Pessoa)
        {
            this.pessoa = Pessoa;
        }

        public string MatriculaAPI { get; set; }

        public string SiglaOrgaoProfissional { get; set; }

        public string NumeroRegistroProfissional { get; set; }

        public DateTime? DataRegistroProfissional { get; set; }

        public string ObservacaoContato { get; set; }

        public IPessoa Pessoa
        {
            get { return pessoa; }
        }
    }
}
