﻿Namespace Negocio

    Public Interface IProduto

        Property ID() As Nullable(Of Long)
        Property CodigoDeBarras() As String
        Property Nome() As String
        Property UnidadeDeMedida() As UnidadeDeMedida
        Property Marca() As IMarcaDeProduto
        ReadOnly Property QuantidadeEmEstoque() As Double
        Property QuantidadeMinimaEmEstoque() As Nullable(Of Double)
        Property ValorDeCusto() As Nullable(Of Double)
        Property ValorDeVendaMinimo() As Nullable(Of Double)
        Property PorcentagemDeLucro() As Nullable(Of Double)
        ReadOnly Property ValorDeVenda() As Double
        Property GrupoDeProduto() As IGrupoDeProduto
        Property Observacoes() As String

    End Interface

End Namespace