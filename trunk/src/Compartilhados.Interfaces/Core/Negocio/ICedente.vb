Namespace Core.Negocio

    Public Interface ICedente
        Inherits IPapelPessoa

        Property ImagemDeCabecalhoDoReciboDoSacado() As String
        Property TipoDeCarteira() As String
        Property InicioNossoNumero() As Double

    End Interface

End Namespace