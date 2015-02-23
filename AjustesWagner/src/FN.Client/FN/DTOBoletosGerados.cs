using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FN.Client.FN
{
    [Serializable]
    public class DTOBoletosGerados
    {
        public string ID { get; set; }
        public string NumeroBoleto { get; set; }
        public string NossoNumero { get; set; }
        public string Cliente { get; set; }
        public string Valor { get; set; }
        public string DataGeracao { get; set; }
        public string DataVencimento { get; set; }
        public string Observacao { get; set; }
        public string Cedente { get; set; }
        public string Instrucoes { get; set; }
        public string StatusBoleto { get; set; }
        public string EhBoletoAvulso { get; set; }
        public bool EstaVencido { get; set; }
    }
}