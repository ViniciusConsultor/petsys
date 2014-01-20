Namespace Core.Negocio

    <Serializable()> _
    Public Class Raca

        Private _ID As Char
        Private _Descricao As String

        Public Shared Indigena As Raca = New Raca("I"c, "Indígena")
        Public Shared Branca As Raca = New Raca("B"c, "Branca")
        Public Shared Preta As Raca = New Raca("P"c, "Preta")
        Public Shared Amarela As Raca = New Raca("A"c, "Amarela")
        Public Shared Parda As Raca = New Raca("R"c, "Parda")

        Private Shared ListaDeRaca As Raca() = {Indigena, Branca, Preta, _
                                                Amarela, Parda}

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

        Public Shared Function ObtenhaRaca(ByVal ID As Char) As Raca
            For Each Raca As Raca In ListaDeRaca
                If Raca.ID = ID Then
                    Return Raca
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of Raca)
            Return New List(Of Raca)(ListaDeRaca)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, Raca).ID = Me.ID
        End Function

    End Class

End Namespace