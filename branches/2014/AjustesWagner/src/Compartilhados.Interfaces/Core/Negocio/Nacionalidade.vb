Namespace Core.Negocio

    <Serializable()> _
    Public Class Nacionalidade

        Private _ID As Char
        Private _Descricao As String

        Public Shared Brasileira As Nacionalidade = New Nacionalidade("B"c, "Brasileira")
        Public Shared NaturalizadoBrasileiro As Nacionalidade = New Nacionalidade("N"c, "Naturalizado Brasileiro")
        Public Shared Argentina As Nacionalidade = New Nacionalidade("A"c, "Argentina")
        Public Shared Boliviana As Nacionalidade = New Nacionalidade("O"c, "Boliviana")
        Public Shared Chilena As Nacionalidade = New Nacionalidade("C"c, "Chilena")
        Public Shared Paraguaia As Nacionalidade = New Nacionalidade("P"c, "Paraguaia")
        Public Shared Uruguaia As Nacionalidade = New Nacionalidade("U"c, "Uruguaia")
        Public Shared Alema As Nacionalidade = New Nacionalidade("L"c, "Alemã")
        Public Shared Belga As Nacionalidade = New Nacionalidade("E"c, "Belga")
        Public Shared Britanica As Nacionalidade = New Nacionalidade("R"c, "Britânica")
        Public Shared Canadense As Nacionalidade = New Nacionalidade("D"c, "Canadense")
        Public Shared Espanhol As Nacionalidade = New Nacionalidade("S"c, "Espanhik")
        Public Shared NorteAmericana As Nacionalidade = New Nacionalidade("T"c, "Norte Americana (EUA)")
        Public Shared Francesa As Nacionalidade = New Nacionalidade("F"c, "Francesa")
        Public Shared Suica As Nacionalidade = New Nacionalidade("I"c, "Suíça")
        Public Shared Italiana As Nacionalidade = New Nacionalidade("T"c, "Italiana")
        Public Shared Japonesa As Nacionalidade = New Nacionalidade("J"c, "Japonesa")
        Public Shared Chinesa As Nacionalidade = New Nacionalidade("H"c, "Chinesa")
        Public Shared Coreana As Nacionalidade = New Nacionalidade("N"c, "Coreana")
        Public Shared Portuguesa As Nacionalidade = New Nacionalidade("P"c, "Portuguesa")
        Public Shared OutrosLatinoAmericanos As Nacionalidade = New Nacionalidade("L"c, "Outros Latino-Americanos")
        Public Shared OutrosAsiaticos As Nacionalidade = New Nacionalidade("Z"c, "Outros Asiáticos")
        Public Shared Outros As Nacionalidade = New Nacionalidade("K"c, "Outros")

        Private Shared Lista As Nacionalidade() = {Brasileira, NaturalizadoBrasileiro, Argentina, Boliviana, _
                                                   Chilena, Paraguaia, Uruguaia, Alema, Belga, Britanica, Canadense, Espanhol, _
                                                   NorteAmericana, Francesa, Suica, Italiana, Japonesa, Chinesa, Coreana, Portuguesa, _
                                                   OutrosLatinoAmericanos, OutrosAsiaticos, Outros}

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

        Public Shared Function Obtenha(ByVal ID As Char) As Nacionalidade
            For Each Item As Nacionalidade In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of Nacionalidade)
            Return New List(Of Nacionalidade)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, Nacionalidade).ID = Me.ID
        End Function

    End Class

End Namespace