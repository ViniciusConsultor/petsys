using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public abstract class ItemLancamentoFinanceiro : IItemLancamentoFinanceiro
    {
        public abstract TipoLacamentoFinanceiro Tipo();
        public long? ID { get; set; }
        public ICliente Cliente { get; set; }
        public double Valor { get; set; }
        public string Observacao { get; set; }
        public DateTime DataDoLancamento { get; set; }
        public DateTime DataDoVencimento { get; set;}
        public Situacao Situacao { get; set; }
        public string Descricao  { get; set; }
        public long IDBOLETO { get; set; }
    }
}
