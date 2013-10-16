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
    public class TipoDePatenteLazyLoad : ITipoDePatenteLazyLoad
    {
        private ITipoDePatente _ObjetoReal;
        private long _ID;

        public TipoDePatenteLazyLoad(long Id)
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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
                _ObjetoReal = servico.obtenhaTipoDePatentePeloId(_ID);
        }

        public long? IdTipoDePatente
        {
            get { return _ID; }
            set
            {
                _ID =  value.Value;
            }
        }

        public string DescricaoTipoDePatente
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoTipoDePatente;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoTipoDePatente = value;
            }
        }

        public string SiglaTipo
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.SiglaTipo;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.SiglaTipo = value;
            }
        }

        public int TempoInicioAnos
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TempoInicioAnos;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TempoInicioAnos = value;
            }
        }

        public int QuantidadePagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.QuantidadePagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.QuantidadePagamento = value;
            }
        }

        public int TempoEntrePagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TempoEntrePagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TempoEntrePagamento = value;
            }
        }

        public int SequenciaInicioPagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.SequenciaInicioPagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.SequenciaInicioPagamento = value;
            }
        }

        public bool TemPagamentoIntermediario
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TemPagamentoIntermediario;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TemPagamentoIntermediario = value;
            }
        }

        public int InicioIntermediarioSequencia
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.InicioIntermediarioSequencia;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.InicioIntermediarioSequencia = value;
            }
        }

        public int QuantidadePagamentoIntermediario
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.QuantidadePagamentoIntermediario;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.QuantidadePagamentoIntermediario = value;
            }
        }

        public int TempoEntrePagamentoIntermediario
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TempoEntrePagamentoIntermediario;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TempoEntrePagamentoIntermediario = value;
            }
        }

        public string DescricaoPagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoPagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoPagamento = value;
            }
        }

        public string DescricaoPagamentoIntermediario
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoPagamentoIntermediario;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoPagamentoIntermediario = value;
            }
        }

        public bool TemPedidoDeExame
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TemPedidoDeExame;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TemPedidoDeExame = value;
            }
        }
    }
}
