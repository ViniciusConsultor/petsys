using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IDespachoDeMarcas
    {
        long? IdDespacho { get; set; }
        string CodigoDespacho { get; set; }
        string DetalheDespacho { get; set; }
        SituacaoDoProcessoDeMarca SituacaoProcesso { get; set; }
        bool Registro { get; set; }
    }
}
