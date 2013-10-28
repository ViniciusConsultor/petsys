using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IDespachoDePatentes
    {
        long? IdDespachoDePatente { get; set; }
        string Codigo { get; set; }
        string Titulo { get; set; }
        string Situacao { get; set; }
        int? PrazoProvidencia { get; set; }
        string TipoProvidencia { get; set; }
        bool DesativaProcesso { get; set; }
        bool AgendarPagamento { get; set; }
        string Descricao { get; set; }
    }
}
