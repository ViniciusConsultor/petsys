using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IDespachoDeMarcas
    {
        long? IdDespacho { get; set; }
        int CodigoDespacho { get; set; }
        string DetalheDespacho { get; set; }
        long? IdSituacaoProcesso { get; set; }
        string Registro { get; set; }
    }
}
