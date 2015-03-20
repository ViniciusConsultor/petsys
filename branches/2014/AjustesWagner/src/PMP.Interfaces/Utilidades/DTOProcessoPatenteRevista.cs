// -----------------------------------------------------------------------
// <copyright file="DTOProcessoPatenteRevista.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PMP.Interfaces.Utilidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class DTOProcessoPatenteRevista
    {
        public string ID { get; set; }
        public int NumeroDaRevista { get; set; }
        public DateTime DataDePublicacaoDaRevista { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime? DataDoDeposito { get; set; }
        public string Classificacao { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string NumeroDepositoPCT { get; set; }
        public IList<string>  Depositantes { get; set; }
        public IList<string> Invetores { get; set; }
        public string NumeroDaPrioridade { get; set; }
        public DateTime? DataDaPrioridade { get; set; }
        public string PaisDaPrioridade { get; set; }

    }
}
