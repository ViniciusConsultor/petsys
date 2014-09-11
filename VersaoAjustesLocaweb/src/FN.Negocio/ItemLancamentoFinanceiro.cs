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

        public bool EstaVencido()
        {
            if (this.Situacao.Equals(Situacao.AguardandoCobranca) || this.Situacao.Equals(Situacao.CobrancaEmAberto) || this.Situacao.Equals(Situacao.CobrancaGerada))
                return Convert.ToInt64(DataDoVencimento.ToString("yyyyMMdd")) <
                   Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));

            return false;
        }

        public bool LacamentoFoiCanceladoOuPago()
        {
            return this.Situacao.Equals(Situacao.Cancelada) || this.Situacao.Equals(Situacao.Paga);
        }

        public long? ID { get; set; }
        public ICliente Cliente { get; set; }
        public double Valor { get; set; }
        public string Observacao { get; set; }
        public DateTime DataDoLancamento { get; set; }
        public DateTime DataDoVencimento { get; set;}
        public Situacao Situacao { get; set; }
        public string Descricao  { get; set; }
        public bool BoletoFoiGeradoColetivamente { get; set; }
        public string NumeroBoletoGerado { get; set; }
    }
}
