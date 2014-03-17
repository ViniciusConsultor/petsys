using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Negocio
{
    public class ItemLancamentoFinanceiroRecebimento : ItemLancamentoFinanceiro, IItemLancamentoFinanceiroRecebimento
    {
        public DateTime? DataDoRecebimento { get; set; }
        public TipoLacamentoFinanceiroRecebimento TipoLacamento { get; set; }

        public FormaDeRecebimento FormaDeRecebimento { get; set; }

        public override TipoLacamentoFinanceiro Tipo()
        {
            return TipoLacamentoFinanceiro.Recebimento;
        }
    }
}
