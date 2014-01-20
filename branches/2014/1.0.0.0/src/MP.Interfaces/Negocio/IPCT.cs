using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IPCT
    {
        string Numero { get; set; }
        Nullable<DateTime> DataDoDeposito { get; set; }
        string NumeroWO { get; set; }
        Nullable<DateTime> DataDaPublicacao { get; set; }
    }
}
