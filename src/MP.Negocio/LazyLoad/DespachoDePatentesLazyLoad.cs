using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class DespachoDePatentesLazyLoad : IDespachoDePatentesLazyLoad
    {
        private IDespachoDePatentes _ObjetoReal;
        private long _ID;

        public DespachoDePatentesLazyLoad(long Id)
        {
            _ID = Id;
        }

        private void VerifiqueSeObjetoEstaCarregado()
        {
            if (_ObjetoReal == null)
                CarregueObjetoReal();
        }

        public void CarregueObjetoReal()
        {
            _ObjetoReal =
                Repositorios.RepositorioDeDespachoDePatentes.obtenhaInstancia().obtenhaDespachoDePatentesPeloId(_ID);
        }

        public long? IdDespachoDePatente
        {
            get { return _ID; }
            set { _ID = value.Value; }
        }

        public string Codigo
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Codigo;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Codigo = value;
            }
        }

        public string Titulo
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Titulo;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Titulo   = value;
            }
        }

        public string Situacao
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Situacao;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Situacao = value;
            }
        }

        public int? PrazoProvidencia
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.PrazoProvidencia;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.PrazoProvidencia = value;
            }
        }

        public string TipoProvidencia
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TipoProvidencia;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TipoProvidencia = value;
            }
        }

        public bool DesativaProcesso
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DesativaProcesso;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DesativaProcesso = value;
            }
        }

        public bool AgendarPagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.AgendarPagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.AgendarPagamento = value;
            }
        }

        public string Descricao
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Descricao;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Descricao = value;
            }
        }

       
    }
}
