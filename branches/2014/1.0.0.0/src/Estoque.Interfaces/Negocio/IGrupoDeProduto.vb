Namespace Negocio

    Public Interface IGrupoDeProduto

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property PorcentagemDeComissao() As Nullable(Of Double)

    End Interface

End Namespace