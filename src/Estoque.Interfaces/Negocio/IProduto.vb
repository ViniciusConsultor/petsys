Namespace Negocio

    Public Interface IProduto

        Property ID() As Nullable(Of Long)
        Property CodigoDeBarras() As String
        Property Nome() As String
        Property Unidade() As String
        Property Marca() As IMarcaDeProduto
        ReadOnly Property QuantidadeEmEstoque() As Integer
        Property QuantidadeMinimaEmEstoque() As Integer
        Property ValorDeCusto() As Double
        Property ValorMinimo() As Double
        Property PorcentagemDeLucro() As Double
        Property ValorDeVenda() As Double
        Property GrupoDeProduto() As IGrupoDeProduto
        Property Observacoes() As String

    End Interface

End Namespace