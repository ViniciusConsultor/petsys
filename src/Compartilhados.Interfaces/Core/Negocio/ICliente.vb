Namespace Core.Negocio

    Public Interface ICliente
        Inherits IPapelPessoa

        Property GrupoDeAtividade As IGrupoDeAtividade
        Property NumeroDoRegistro As String
        Property DataDoRegistro As Nullable(Of Date)

        Property DataDoCadastro() As Nullable(Of Date)
        Property InformacoesAdicionais() As String
        Property FaixaSalarial() As Nullable(Of Double)
        Property PorcentagemDeDescontoAutomatico() As Nullable(Of Double)
        Property ValorMaximoParaCompras() As Nullable(Of Double)
        Property SaldoParaCompras() As Nullable(Of Double)
        Property PossuiCobranca As Boolean

    End Interface

End Namespace