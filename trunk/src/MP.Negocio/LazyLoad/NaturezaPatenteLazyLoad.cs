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
    public class NaturezaPatenteLazyLoad : INaturezaPatenteLazyLoad
    {
        private INaturezaPatente _objetoReal;
        private long? _id;

        public NaturezaPatenteLazyLoad(long ID)
        {
            _id = ID;
        }

        public void CarregueObjetoReal()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
                _objetoReal = servico.obtenhaNaturezaPatentePeloId(_id.Value);
        }

        public long? IdNaturezaPatente
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DescricaoNaturezaPatente
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.DescricaoNaturezaPatente;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.DescricaoNaturezaPatente = value;
            }
        }

        public string SiglaNatureza
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.SiglaNatureza;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.SiglaNatureza = value;
            }
        }

        public int TempoInicioAnos
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TempoInicioAnos;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TempoInicioAnos = value;
            }
        }

        public int QuantidadePagamento
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.QuantidadePagamento;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.QuantidadePagamento = value;
            }
        }

        public int TempoEntrePagamento
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TempoEntrePagamento;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TempoEntrePagamento = value;
            }
        }

        public int SequenciaInicioPagamento
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.SequenciaInicioPagamento;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TempoEntrePagamento = value;
            }
        }

        public bool TemPagamentoIntermediario
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TemPagamentoIntermediario;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TemPagamentoIntermediario = value;
            }
        }

        public int InicioIntermediarioSequencia
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.InicioIntermediarioSequencia;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.InicioIntermediarioSequencia = value;
            }
        }

        public int QuantidadePagamentoIntermediario
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.QuantidadePagamentoIntermediario;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.QuantidadePagamentoIntermediario = value;
            }
        }

        public int TempoEntrePagamentoIntermediario
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TempoEntrePagamentoIntermediario;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TempoEntrePagamentoIntermediario = value;
            }
        }

        public string DescricaoPagamento
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.DescricaoPagamento;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.DescricaoPagamento = value;
            }
        }

        public string DescricaoPagamentoIntermediario
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.DescricaoPagamentoIntermediario;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.DescricaoPagamentoIntermediario = value;
            }
        }

        public bool TemPedidoDeExame
        {
            get
            {
                if (_objetoReal == null) CarregueObjetoReal();
                return _objetoReal.TemPedidoDeExame;
            }
            set
            {
                if (_objetoReal == null) CarregueObjetoReal();
                _objetoReal.TemPedidoDeExame = value;
            }
        }
    }
}
