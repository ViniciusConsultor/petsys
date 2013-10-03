using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.Documento;
using Compartilhados.Interfaces.Core.Negocio.Telefone;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class Procurador : IProcurador
    {

        public DateTime? DataDeNascimento
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public EstadoCivil EstadoCivil
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Foto
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public GrauDeInstrucao GrauDeInstrucao
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Nacionalidade Nacionalidade
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IMunicipio Naturalidade
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string NomeDaMae
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string NomeDoPai
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Raca Raca
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Sexo Sexo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AdicioneDocumento(IDocumento Documento)
        {
            throw new NotImplementedException();
        }

        public void AdicioneTelefone(ITelefone Telefone)
        {
            throw new NotImplementedException();
        }

        public void AdicioneTelefones(IList<ITelefone> Telefones)
        {
            throw new NotImplementedException();
        }

        public IDadoBancario DadoBancario
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEndereco Endereco
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public EnderecoDeEmail EnderecoDeEmail
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long? ID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Nome
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDocumento ObtenhaDocumento(TipoDeDocumento TipoDocumento)
        {
            throw new NotImplementedException();
        }

        public IList<ITelefone> ObtenhaTelefones(TipoDeTelefone TipoTelefone)
        {
            throw new NotImplementedException();
        }

        public string Site
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<ITelefone> Telefones
        {
            get { throw new NotImplementedException(); }
        }

        public TipoDePessoa Tipo
        {
            get { throw new NotImplementedException(); }
        }

        public string MatriculaAPI
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string SiglaOrgaoProfissional
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string NumeroRegistroProfissional
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? DataRegistroProfissional
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ObservacaoContato
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
