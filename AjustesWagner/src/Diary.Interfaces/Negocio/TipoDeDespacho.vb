Namespace Negocio

    <Serializable()> _
    Public Class TipoDeDespacho

        Private _ID As Byte
        Private _Descricao As String

        Public Shared Telegrama As TipoDeDespacho = New TipoDeDespacho(1, "Telegrama")
        Public Shared Agendar As TipoDeDespacho = New TipoDeDespacho(2, "Agendar")
        Public Shared Mensagem As TipoDeDespacho = New TipoDeDespacho(3, "Mensagem")
        Public Shared Representante As TipoDeDespacho = New TipoDeDespacho(4, "Representante")
        Public Shared Lembrente As TipoDeDespacho = New TipoDeDespacho(5, "Lembrete")
        Public Shared Presente As TipoDeDespacho = New TipoDeDespacho(6, "Presente")
        Public Shared Remarcar As TipoDeDespacho = New TipoDeDespacho(7, "Remarcar")
        Public Shared Outros As TipoDeDespacho = New TipoDeDespacho(8, "Outros")

        Private Sub New(ByVal ID As Byte, ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Private Shared Lista As TipoDeDespacho() = {Telegrama, Agendar, Mensagem, _
                                                Representante, Lembrente, Presente, Remarcar, Outros}

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

        Public Shared Function Obtenha(ByVal ID As Byte) As TipoDeDespacho
            For Each Item As TipoDeDespacho In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of TipoDeDespacho)
            Return New List(Of TipoDeDespacho)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDeDespacho).ID = Me.ID
        End Function

    End Class

End Namespace