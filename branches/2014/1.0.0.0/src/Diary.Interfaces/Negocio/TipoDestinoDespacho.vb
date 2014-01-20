Namespace Negocio

    <Serializable()> _
    Public Class TipoDestinoDespacho

        Private _ID As Byte
        Private _Descricao As String

        Public Shared Compromisso As TipoDestinoDespacho = New TipoDestinoDespacho(1, "Compromisso")
        Public Shared Tarefa As TipoDestinoDespacho = New TipoDestinoDespacho(2, "Tarefa")
        Public Shared Lembrete As TipoDestinoDespacho = New TipoDestinoDespacho(3, "Lembrete")

        Private Sub New(ByVal ID As Byte, ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Private Shared Lista As TipoDestinoDespacho() = {Compromisso, Tarefa, Lembrete}

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

        Public Shared Function Obtenha(ByVal ID As Byte) As TipoDestinoDespacho
            For Each Item As TipoDestinoDespacho In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDestinoDespacho)
            Return New List(Of TipoDestinoDespacho)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDestinoDespacho).ID = Me.ID
        End Function

    End Class

End Namespace