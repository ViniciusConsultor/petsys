using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Evento : IEvento
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
