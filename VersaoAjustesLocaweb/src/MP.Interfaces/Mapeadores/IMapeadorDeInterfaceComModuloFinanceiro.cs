using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.FN.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeInterfaceComModuloFinanceiro
    {
        void ItemDeRecebimentoFoiModificado(IItemLancamentoFinanceiroRecebimento itemLancamentoFinanceiro);
        void Insira(long idItemRecebimento, string Conceito, long idConceito, DateTime dataVencimento);
    }
}
