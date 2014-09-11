using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class TipoDeProcedimentoInterno : ITipoDeProcedimentoInterno
    {
        public long? Id
        {get; set; }

        public string Descricao
        {get; set; }
    }
}
