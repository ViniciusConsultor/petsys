using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class DespachoDeMarcas : IDespachoDeMarcas
    {
        private long? _idDespacho;
        private int _codigoDespacho;
        private string _detalheDespacho;
        private long? _idSituacaoProcesso;
        private bool _registro;

        public long? IdDespacho
        {
            get { return _idDespacho; }
            set { _idDespacho = value; }
        }

        public int CodigoDespacho
        {
            get { return _codigoDespacho; }
            set { _codigoDespacho = value; }
        }

        public string DetalheDespacho
        {
            get { return _detalheDespacho; }
            set { _detalheDespacho = value; }
        }

        public long? IdSituacaoProcesso
        {
            get { return _idSituacaoProcesso; }
            set { _idSituacaoProcesso = value; }
        }

        public bool Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }
    }
}
