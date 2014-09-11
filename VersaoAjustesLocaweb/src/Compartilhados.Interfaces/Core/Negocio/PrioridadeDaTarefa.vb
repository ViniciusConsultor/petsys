Namespace Core.Negocio

    <Serializable()> _
    Public Class PrioridadeDaTarefa

        Private _ID As Char
        Private _Descricao As String

        Public Shared Baixa As PrioridadeDaTarefa = New PrioridadeDaTarefa("B"c, "Baixa")
        Public Shared Normal As PrioridadeDaTarefa = New PrioridadeDaTarefa("N"c, "Normal")
        Public Shared Alta As PrioridadeDaTarefa = New PrioridadeDaTarefa("A"c, "Alta")
        
        Private Shared Lista As PrioridadeDaTarefa() = {baixa, normal, alta}

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

        Public Shared Function Obtenha(ByVal ID As Char) As PrioridadeDaTarefa
            For Each Item As PrioridadeDaTarefa In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of PrioridadeDaTarefa)
            Return New List(Of PrioridadeDaTarefa)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, PrioridadeDaTarefa).ID = Me.ID
        End Function
    End Class

End Namespace