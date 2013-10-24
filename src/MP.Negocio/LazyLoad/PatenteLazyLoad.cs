using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class PatenteLazyLoad : IPatenteLazyLoad
    {
        private IPatente _objetoReal;
        private long _id;

        public PatenteLazyLoad(long ID)
        {
            _id = ID;
        }

        public void CarregueObjetoReal()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                _objetoReal = servico.ObtenhaPatente(_id);
            }
        }

        public long Identificador
        {
            get { return _id; }
            set { _id = value; }
        }

        public string TituloPatente
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public INaturezaPatente NaturezaPatente
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool ObrigacaoGerada
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DateTime? DataCadastro
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Observacao
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Resumo
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int QuantidadeReivindicacao
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<IAnuidadePatente> Anuidades
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<IClassificacaoPatente> Classificacoes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<IPrioridadeUnionistaPatente> PrioridadesUnionista
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<ITitularPatente> Titulares
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public IList<ICliente> Clientes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
