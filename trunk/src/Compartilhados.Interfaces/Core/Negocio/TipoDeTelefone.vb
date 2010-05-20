Namespace Core.Negocio

    <Serializable()> _
    Public Class TipoDeTelefone

        Private _ID As Short
        Private _Descricao As String

        Public Shared Residencial As TipoDeTelefone = New TipoDeTelefone(1S, "Residêncial")
        Public Shared Comercial As TipoDeTelefone = New TipoDeTelefone(2S, "Comercial")
        Public Shared Recado As TipoDeTelefone = New TipoDeTelefone(3S, "Recado")
        Public Shared Celular As TipoDeTelefone = New TipoDeTelefone(4S, "Celular")

        Private Shared Lista As TipoDeTelefone() = {Residencial, Comercial, _
                                                    Recado, Celular}

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

        Public Shared Function Obtenha(ByVal ID As Short) As TipoDeTelefone
            For Each Tipo As TipoDeTelefone In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDeTelefone)
            Return New List(Of TipoDeTelefone)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDeTelefone).ID = Me.ID
        End Function

    End Class

End Namespace