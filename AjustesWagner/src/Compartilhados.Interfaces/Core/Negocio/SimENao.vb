Namespace Core.Negocio

    <Serializable()> _
     Public Class SimENao

        Private _ID As Char
        Private _Descricao As String

        Public Shared Sim As SimENao = New SimENao("S"c, "Sim")
        Public Shared Nao As SimENao = New SimENao("N"c, "Não")
        
        Private Shared Lista As SimENao() = {Sim, Nao}

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

        Public Shared Function Obtenha(ByVal ID As Char) As SimENao
            For Each Item As SimENao In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of SimENao)
            Return New List(Of SimENao)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, SimENao).ID = Me.ID
        End Function
    End Class

End Namespace