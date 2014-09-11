using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class TemplateDeEmail : ITemplateDeEmail
    {
        public string Template
        {
            get; set;
        }
    }
}
