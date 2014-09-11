using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Negocio
{
    [Serializable]
    public class ItemLancamentoFinanceiroRecebimento : ItemLancamentoFinanceiro, IItemLancamentoFinanceiroRecebimento
    {
        public bool FormaDeRecebimentoEhBoleto()
        {
            if (FormaDeRecebimento != null)
                return FormaDeRecebimento.Equals(FormaDeRecebimento.Boleto);

            return false;
        }

        public DateTime? DataDoRecebimento { get; set; }
        public TipoLacamentoFinanceiroRecebimento TipoLacamento { get; set; }

        public FormaDeRecebimento FormaDeRecebimento { get; set; }

        public override TipoLacamentoFinanceiro Tipo()
        {
            return TipoLacamentoFinanceiro.Recebimento;
        }

    }
}
