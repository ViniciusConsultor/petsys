﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FN.Interfaces.Negocio
{
    public interface IBoletosGeradosAux
    {
        long? ID { get; set; }
        long? ProximoNossoNumero { get; set; }
        long? IDCEDENTE { get; set; }
    }
}
