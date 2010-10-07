Namespace Negocio

    <Serializable()> _
    Public Class TipoDeSolicitacao

        Private _ID As Byte
        Private _Descricao As String

        Public Shared Audiencia As TipoDeSolicitacao = New TipoDeSolicitacao(1, "Audiência")
        Public Shared Convite As TipoDeSolicitacao = New TipoDeSolicitacao(2, "Convite")

        Private Sub New(ByVal ID As Byte, ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Private Shared Lista As TipoDeSolicitacao() = {Audiencia, Convite}

        Public ReadOnly Property ID() As Byte
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Byte) As TipoDeSolicitacao
            For Each Item As TipoDeSolicitacao In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDeSolicitacao)
            Return New List(Of TipoDeSolicitacao)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDeSolicitacao).ID = Me.ID
        End Function

    End Class

End Namespace