﻿Imports Compartilhados.Interfaces.Core.Negocio

Namespace FN.Negocio

    Public Interface IItemLancamentoFinanceiro

        Property ID As Nullable(Of Long)
        Property Cliente As ICliente
        Property Valor As Double
        Property Observacao As String
        Property DataDoLancamento As Date
        Property DataDoVencimento As Date
        Function Tipo() As TipoLacamentoFinanceiro
        Property Situacao As Situacao
        Property Descricao As String
        Function EstaVencido() As Boolean
        Function LacamentoFoiCanceladoOuPago() As Boolean

        Property BoletoFoiGeradoColetivamente As Boolean
        Property NumeroBoletoGerado As String

    End Interface

End Namespace