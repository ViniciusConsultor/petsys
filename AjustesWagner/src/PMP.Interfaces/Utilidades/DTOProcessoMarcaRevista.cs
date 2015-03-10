using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMP.Interfaces.Utilidades
{
    [Serializable]
    public class DTOProcessoMarcaRevista
    {
        public int NumeroDaRevista { get; set; }
        public DateTime DataDePublicacaoDaRevista { get; set; }
        public string NumeroProcessoDeMarca { get; set; }
        public Nullable<DateTime> DataDoDeposito { get; set; }
        public Nullable<DateTime> DataDaConcessao { get; set; }
        public Nullable<DateTime> DataDaVigencia { get; set; }
        public string CodigoDoDespacho { get; set; }
        public string NomeDoDespacho { get; set; }
        public string Titular { get; set; }
        public string PaisTitular { get; set; }
        public string UFTitular { get; set; }
        public string Marca { get; set; }
        public string Apresentacao { get; set; }
        public string Natureza { get; set; }
        public string EdicaoClasseViena { get; set; }
        public string CodigoClasseViena { get; set; }
        public string CodigoClasseNacional { get; set; }
        public string CodigoSubClasseNacional { get; set; }
        public string Procurador { get; set; }
        public string CodigoClasseNice { get; set; }
    }
}
