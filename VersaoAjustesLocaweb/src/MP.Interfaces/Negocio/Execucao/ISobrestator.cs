using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio.Execucao
{
    public interface ISobrestator
    {
        long? IdSobrestator { get; set; }
        string processo { get; set; }
        string marca { get; set; }
        long? IdLeitura { get; set; }
    }
}
