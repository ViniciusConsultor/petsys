Imports Compartilhados.Interfaces.Core.Negocio

Namespace FN.Negocio

    Public Interface IItemLancamentoFinanceiroRecebimento
        Inherits IItemLancamentoFinanceiro

        Property DataDoRecebimento As Nullable(Of Date)
        Property TipoLacamento As TipoLacamentoFinanceiroRecebimento

    End Interface

End Namespace

