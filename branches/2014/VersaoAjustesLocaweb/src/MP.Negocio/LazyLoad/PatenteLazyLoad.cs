using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;
using MP.Negocio.Repositorios;

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
            _objetoReal = RepositorioDePatente.obtenhaInstancia().ObtenhaPatente(_id);
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

        public IList<IInventor> Inventores
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Inventores;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Inventores = value;
            }
        }


        public IList<ICliente> Clientes
        {
            get 
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Clientes;
            }
            set 
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Clientes = value;
            }
        }

        public IList<IRadicalPatente> Radicais
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Radicais;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Radicais = value;
            }
        }

        public IList<ITitular> Titulares
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


        public string ClassificacoesConcatenadas
        {
            get
            {
                string classificacoesConcatenadas = string.Empty;

                foreach (IClassificacaoPatente classificacaoPatente in Classificacoes)
                    classificacoesConcatenadas += classificacaoPatente.Classificacao + ", ";

                if (string.IsNullOrEmpty(classificacoesConcatenadas))
                    return classificacoesConcatenadas;

                return classificacoesConcatenadas.Substring(0, classificacoesConcatenadas.Length - 2);
            }
        }

        public IManutencao Manutencao
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Manutencao;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Manutencao = value;
            }
        }

        public bool PatenteEhDeDesenhoIndutrial()
        {
            if (_objetoReal == null) CarregueObjetoReal();
            return _objetoReal.PatenteEhDeDesenhoIndutrial();
        }

        public string Imagem
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.Imagem;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.Imagem = value;
            }
        }
    }
}
