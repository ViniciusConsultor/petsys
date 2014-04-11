Namespace Core.Negocio

    Public Interface ICedente
        Inherits IPapelPessoa

        Property ImagemDeCabecalhoDoReciboDoSacado() As String
        Property TipoDeCarteira() As String
        Property InicioNossoNumero() As Long

    End Interface

End Namespace