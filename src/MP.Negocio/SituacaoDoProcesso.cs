using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
   public class SituacaoDoProcesso : ISituacaoDoProcesso
    {
        private long? _idSituacaoPeocesso;
        private string _descricaoSituacao;

        public long? IdSituacaoProcesso
        {
            get { return _idSituacaoPeocesso; }
            set { _idSituacaoPeocesso = value; }
        }

        public string DescricaoSituacao
        {
            get { return _descricaoSituacao; }
            set { _descricaoSituacao = value; }
        }
    }
}
