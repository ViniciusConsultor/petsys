using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class TipoDePatente : ITipoDePatente
    {
        private long? _idTipoPatente;
        private string _descricaoTipoDePatente;
        private char _siglaTipo;
        private int _tempoInicioAnos;
        private int _quantidadePagto;
        private int _tempoEntrePagto;
        private int _sequenciaInicioPagto;
        private bool _temPagtoIntermediario; // tipo bit
        private int _inicioIntermedSequencia;
        private int _quantidadePagtoIntermed;
        private int _tempoEntrePagtoIntermed;
        private string _descricaoPagto;
        private string _descricaoPagtoIntermed;
        private bool _temPedExame; // tipo bit


        public long? IdTipoPatente
        {
            get { return _idTipoPatente; }
            set { _idTipoPatente = value; }
        }

        public string DescricaoTipoDePatente
        {
            get { return _descricaoTipoDePatente; }
            set { _descricaoTipoDePatente = value; }
        }

        public char SiglaTipo
        {
            get { return _siglaTipo; }
            set { _siglaTipo = value; }
        }

        public int TempoInicioAnos
        {
            get { return _tempoInicioAnos; }
            set { _tempoInicioAnos = value; }
        }

        public int QuantidadePagto
        {
            get { return _quantidadePagto; }
            set { _quantidadePagto = value; }
        }

        public int TempoEntrePagto
        {
            get { return _tempoEntrePagto; }
            set { _tempoEntrePagto = value; }
        }

        public int SequenciaInicioPagto
        {
            get { return _sequenciaInicioPagto; }
            set { _sequenciaInicioPagto = value; }
        }

        public bool TemPagtoIntermediario
        {
            get { return _temPagtoIntermediario; }
            set { _temPagtoIntermediario = value; }
        }

        public int InicioIntermedSequencia
        {
            get { return _inicioIntermedSequencia; }
            set { _inicioIntermedSequencia = value; }
        }

        public int QuantidadePagtoIntermed
        {
            get { return _quantidadePagtoIntermed; }
            set { _quantidadePagtoIntermed = value; }
        }

        public int TempoEntrePagtoIntermed
        {
            get { return _tempoEntrePagtoIntermed; }
            set { _tempoEntrePagtoIntermed = value; }
        }

        public string DescricaoPagto
        {
            get { return _descricaoPagto; }
            set { _descricaoPagto = value; }
        }

        public string DescricaoPagtoIntermed
        {
            get { return _descricaoPagtoIntermed; }
            set { _descricaoPagtoIntermed = value; }
        }

        public bool TemPedExame
        {
            get { return _temPedExame; }
            set { _temPedExame = value; }
        }
    }
}
