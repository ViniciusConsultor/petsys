using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ProcedimentosInternos : IProcedimentosInternos
    {
        private long? _idTipoAndamentoInterno;
        private string _descricaoTipo;

        public long? IdTipoAndamentoInterno
        {
            get { return _idTipoAndamentoInterno; }
            set { _idTipoAndamentoInterno = value; }
        }

        public string DescricaoTipo
        {
            get { return _descricaoTipo; }
            set { _descricaoTipo = value; }
        }
    }
}
