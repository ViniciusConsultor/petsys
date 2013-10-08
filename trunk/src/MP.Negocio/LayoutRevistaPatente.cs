using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class LayoutRevistaPatente : ILayoutRevistaPatente
    {

        public long Codigo { get; set; }

        public string NomeDoCampo { get; set; }

        public string DescricaoDoCampo { get; set; }

        public string DescricaoResumida { get; set; }

        public long TamanhoDoCampo { get; set; }

        public bool CampoDelimitadorDoRegistro { get; set; }

        public bool CampoIdentificadorDoProcesso { get; set; }

        public bool CampoIdentificadorDeColidencia { get; set; }
    }
}
