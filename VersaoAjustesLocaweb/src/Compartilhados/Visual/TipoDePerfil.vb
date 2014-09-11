Namespace Visual

    <Serializable()> _
    Public Class TipoDePerfil

        Private _ID As Char
        Private _Descricao As String

        Public Shared Padrao As TipoDePerfil = New TipoDePerfil("0"c, "PADRÃO")
        Public Shared Usuario As TipoDePerfil = New TipoDePerfil("1"c, "USUÁRIO")

        Private Shared Lista As TipoDePerfil() = {Padrao, Usuario}

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

        Public Shared Function Obtenha(ByVal ID As Char) As TipoDePerfil
            For Each Tipo As TipoDePerfil In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoDePerfil).ID = Me.ID
        End Function

    End Class

End Namespace