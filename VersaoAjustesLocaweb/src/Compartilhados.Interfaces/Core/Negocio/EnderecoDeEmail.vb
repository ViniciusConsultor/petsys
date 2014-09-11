Namespace Core.Negocio

    <Serializable()> _
    Public Class EnderecoDeEmail
        Private ReadOnly _EnderecoDeEmail As String

        Private Sub New(ByVal EnderecoDeEmail As String)
            _EnderecoDeEmail = EnderecoDeEmail
        End Sub

        Public Shared Widening Operator CType(ByVal EnderecoDeEmail As String) As EnderecoDeEmail
            Return New EnderecoDeEmail(EnderecoDeEmail)
        End Operator

        Public Overrides Function ToString() As String
            Return _EnderecoDeEmail
        End Function

    End Class

End Namespace