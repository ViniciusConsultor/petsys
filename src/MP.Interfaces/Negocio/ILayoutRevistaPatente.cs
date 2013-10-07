using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ILayoutRevistaPatente
    {
        long Codigo { get; set; }
        string Identificador { get; set; }
        string DescricaoIdentificador { get; set; }
        string DescricaoResumida { get; set; }
        int TipoDeIdentificador { get; set; }
        long TamanhoDoCampo { get; set; }
        bool CampoDelimitadorDoRegistro { get; set; }
        bool CampoIdentificadorDoProcesso { get; set; }
        bool CampoIdentificadorDeColidencia { get; set; }
    }
}
