Namespace Core.Negocio

    <Serializable()> _
    Public Class StatusDoOperador

        Private _ID As Char
        Private _Descricao As String

        Public Shared Ativo As StatusDoOperador = New StatusDoOperador("A"c, "Ativo")
        Public Shared Inativo As StatusDoOperador = New StatusDoOperador("I"c, "Inativo")
        Public Shared Bloqueado As StatusDoOperador = New StatusDoOperador("B"c, "Bloqueado")

        Private Shared ListaDeStatus As StatusDoOperador() = {Ativo, _
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

        Public Shared Function ObtenhaStatus(ByVal ID As Char) As StatusDoOperador
            For Each Status As StatusDoOperador In ListaDeStatus
                If Status.ID = ID Then
                    Return Status
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodosStatus() As IList(Of StatusDoOperador)
            Return New List(Of StatusDoOperador)(ListaDeStatus)
        End Function

    End Class

End Namespace