using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class TipoDePatente : ITipoDePatente
    {
        private long? _idTipoDePatente;
        private string _descricaoTipoDePatente;
        private string _siglaTipo;
        private int _tempoInicioAnos;
        private int _quantidadePagamento;
        private int _tempoEntrePagamento;
        private int _sequenciaInicioPagamento;
        private bool _temPagamentoIntermediario;
        private int _inicioIntermediarioSequencia;
        private int _quantidadePagamentoIntermediario;
        private int _tempoEntrePagamentoIntermediario;
        private string _descricaoPagamento;
        private string _descricaoPagamentoIntermediario;
        private bool _temPedidoDeExame;
        

        public long? IdTipoDePatente
        {
            get { return _idTipoDePatente; }
            set { _idTipoDePatente = value; }
        }

        public string DescricaoTipoDePatente
        {
            get { return _descricaoTipoDePatente; }
            set { _descricaoTipoDePatente = value; }
        }

        public string SiglaTipo
        {
            get { return _siglaTipo; }
            set { _siglaTipo = value; }
        }

        public int TempoInicioAnos
        {
            get { return _tempoInicioAnos; }
            set { _tempoInicioAnos = value; }
        }

        public int QuantidadePagamento
        {
            get { return _quantidadePagamento; }
            set { _quantidadePagamento = value; }
        }

        public int TempoEntrePagamento
        {
            get { return _tempoEntrePagamento; }
            set { _tempoEntrePagamento = value; }
        }

        public int SequenciaInicioPagamento
        {
            get { return _sequenciaInicioPagamento; }
            set { _sequenciaInicioPagamento = value; }
        }

        public bool TemPagamentoIntermediario
        {
            get { return _temPagamentoIntermediario; }
            set { _temPagamentoIntermediario = value; }
        }

        public int InicioIntermediarioSequencia
        {
            get { return _inicioIntermediarioSequencia; }
            set { _inicioIntermediarioSequencia = value; }
        }

        public int QuantidadePagamentoIntermediario
        {
            get { return _quantidadePagamentoIntermediario; }
            set { _quantidadePagamentoIntermediario = value; }
        }

        public int TempoEntrePagamentoIntermediario
        {
            get { return _tempoEntrePagamentoIntermediario; }
            set { _tempoEntrePagamentoIntermediario = value; }
        }

        public string DescricaoPagamento
        {
            get { return _descricaoPagamento; }
            set { _descricaoPagamento = value; }
        }

        public string DescricaoPagamentoIntermediario
        {
            get { return _descricaoPagamentoIntermediario; }
            set { _descricaoPagamentoIntermediario = value; }
        }

        public bool TemPedidoDeExame
        {
            get { return _temPedidoDeExame; }
            set { _temPedidoDeExame = value; }
        }
    }
}
