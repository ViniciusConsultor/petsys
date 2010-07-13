Namespace Core.Negocio

    <Serializable()> _
    Public Class StatusDaTarefa

        Private _ID As Char
        Private _Descricao As String

        Public Shared NaoIniciada As StatusDaTarefa = New StatusDaTarefa("N"c, "NÃO INICIADA")
        Public Shared EmAndamento As StatusDaTarefa = New StatusDaTarefa("E"c, "EM ANDAMENTO")
        Public Shared Concluida As StatusDaTarefa = New StatusDaTarefa("C"c, "CONCLUÍDA")

        Private Shared Lista As StatusDaTarefa() = {NaoIniciada, EmAndamento, Concluida}

        Private Sub New(ByVal ID As Char, _
                        ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Public ReadOnly Property ID() As Char
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Char) As StatusDaTarefa
            For Each Item As StatusDaTarefa In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of StatusDaTarefa)
            Return New List(Of StatusDaTarefa)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, StatusDaTarefa).ID = Me.ID
        End Function

    End Class

End Namespace