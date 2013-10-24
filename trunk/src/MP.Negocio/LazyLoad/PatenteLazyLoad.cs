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
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TituloPatente;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TituloPatente = value;
            }
        }

        public INaturezaPatente NaturezaPatente
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.NaturezaPatente;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.NaturezaPatente = value;
            }
        }

        public bool ObrigacaoGerada
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.ObrigacaoGerada;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.ObrigacaoGerada = value;
            }
        }

        public DateTime? DataCadastro
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.DataCadastro;

            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.DataCadastro = value;
            }
        }

        public string Observacao
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Observacao;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Observacao = value;
            }
        }

        public string Resumo
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Resumo;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Resumo = value;
            }
        }

        public int QuantidadeReivindicacao
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.QuantidadeReivindicacao;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.QuantidadeReivindicacao = value;
            }
        }

        public IList<IAnuidadePatente> Anuidades
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Anuidades;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Anuidades = value;
            }
        }

        public IList<IClassificacaoPatente> Classificacoes
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Classificacoes;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Classificacoes = value;

            }
        }

        public IList<IPrioridadeUnionistaPatente> PrioridadesUnionista
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.PrioridadesUnionista;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.PrioridadesUnionista = value;
            }
        }

        public IList<ITitularPatente> Titulares
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Titulares;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Titulares = value;
            }
        }


        public IList<ICliente> Clientes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
