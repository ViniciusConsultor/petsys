using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IEvento
    {
        string Descricao { get; set; }
        DateTime Data { get; set; }
    }
}
