using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MP.Client.MP
{
    [Serializable]
    public class DTOProcessoDeMarca
    {

        public string IdProcesso { get; set; }
        public string NumeroProcesso { get; set; }
        public string DescricaoMarca { get; set; }
        public string Classe { get; set; }
        public string DataDeposito { get; set; }
        public string Apresentacao { get; set; }
        public string Natureza { get; set; }
        public string CPFCNPJ { get; set; }
        public string Cliente { get; set; }
        public string DataDoCadastro { get; set; }
        public string IdCliente { get; set; }

    }
}