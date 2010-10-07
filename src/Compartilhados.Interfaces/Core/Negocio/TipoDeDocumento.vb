Namespace Core.Negocio

    <Serializable()> _
    Public Class TipoDeDocumento

        Private _ID As Short
        Private _Descricao As String

        Public Shared CPF As TipoDeDocumento = New TipoDeDocumento(1S, "CPF")
        Public Shared RG As TipoDeDocumento = New TipoDeDocumento(2S, "RG")
        Public Shared CNPJ As TipoDeDocumento = New TipoDeDocumento(3S, "CNPJ")
        Public Shared IE As TipoDeDocumento = New TipoDeDocumento(4S, "Inscrição Estadual")
        Public Shared IM As TipoDeDocumento = New TipoDeDocumento(5S, "Inscrição Municipal")

        Private Shared Lista As TipoDeDocumento() = {CPF, RG, CNPJ, IE}

        Private Sub New(ByVal ID As Short, _
                        ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Public ReadOnly Property ID() As Short
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Short) As TipoDeDocumento
            For Each Tipo As TipoDeDocumento In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDeDocumento)
            Return New List(Of TipoDeDocumento)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDeDocumento).ID = Me.ID
        End Function

    End Class

End Namespace