Namespace Core.Negocio

    Public Interface ICedente
        Inherits IPapelPessoa

        Property ImagemDeCabecalhoDoReciboDoSacado() As String
        Property TipoDeCarteira() As TipoDeCarteira
        Property InicioNossoNumero() As Long
        Property NumeroDaAgencia() As String
        Property NumeroDaConta() As String
        Property TipoDaConta() As Integer
        Property Padrao() As Boolean
        Property NumeroDoBanco() As String

    End Interface

End Namespace