Namespace Core.Negocio

    <Serializable()> _
    Public Class StatusDoGrupo

        Private _ID As Char
        Private _Descricao As String

        Public Shared Ativo As StatusDoGrupo = New StatusDoGrupo("A"c, "Ativo")
        Public Shared Inativo As StatusDoGrupo = New StatusDoGrupo("I"c, "Inativo")
        Public Shared Bloqueado As StatusDoGrupo = New StatusDoGrupo("B"c, "Bloqueado")

        Private Shared ListaDeStatus As StatusDoGrupo() = {Ativo, _
                                                           Inativo, _
                                                           Bloqueado}
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

        Public Shared Function ObtenhaStatus(ByVal ID As Char) As StatusDoGrupo
            For Each Status As StatusDoGrupo In ListaDeStatus
                If Status.ID = ID Then
                    Return Status
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodosStatus() As IList(Of StatusDoGrupo)
            Return New List(Of StatusDoGrupo)(ListaDeStatus)
        End Function
    End Class

End Namespace