Namespace Core.Negocio

    Public Interface ICliente
        Inherits IPapelPessoa

        Property DataDoCadastro() As Nullable(Of Date)
        Property InformacoesAdicionais() As String
        Property FaixaSalarial() As Nullable(Of Double)
        Property PorcentagemDeDescontoAutomatico() As Nullable(Of Double)
        Property ValorMaximoParaCompras() As Nullable(Of Double)
        Property SaldoParaCompras() As Nullable(Of Double)

    End Interface

End Namespace