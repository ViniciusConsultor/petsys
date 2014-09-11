using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class NaturezaPatente : INaturezaPatente
    {
        public long? IdNaturezaPatente { get; set; }

        public string DescricaoNaturezaPatente { get; set; }

        public string SiglaNatureza { get; set; }

        public int TempoInicioAnos { get; set; }

        public int QuantidadePagamento { get; set; }

        public int TempoEntrePagamento { get; set; }

        public int SequenciaInicioPagamento { get; set; }

        public bool TemPagamentoIntermediario { get; set; }

        public int InicioIntermediarioSequencia { get; set; }

        public int QuantidadePagamentoIntermediario { get; set; }

        public int TempoEntrePagamentoIntermediario { get; set; }

        public string DescricaoPagamento { get; set; }

        public string DescricaoPagamentoIntermediario { get; set; }

        public bool TemPedidoDeExame { get; set; }

        public bool EhNaturezaDeDesenhoIndustrial()
        {
            if (string.IsNullOrEmpty(SiglaNatureza)) return false;

            return SiglaNatureza.ToUpper().Equals("DI") ||
                   SiglaNatureza.ToUpper().Equals("MI") ||
                   SiglaNatureza.ToUpper().Equals("30") ||
                   SiglaNatureza.ToUpper().Equals("31") ||
                   SiglaNatureza.ToUpper().Equals("32");
        }
    }
}
