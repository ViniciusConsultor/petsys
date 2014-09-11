Namespace Visual

    <Serializable()> _
    Public Class TipoAtalho

        Private _ID As Char
        Private _Descricao As String

        Public Shared Sistema As TipoAtalho = New TipoAtalho("0"c, "SISTEMA")
        Public Shared Externo As TipoAtalho = New TipoAtalho("1"c, "EXTERNO")

        Private Shared Lista As TipoAtalho() = {Sistema, Externo}

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

        Public Shared Function Obtenha(ByVal ID As Char) As TipoAtalho
            For Each Tipo As TipoAtalho In Lista
                If Tipo.ID = ID Then
                    Return Tipo
                End If
            Next

            Return Nothing
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, TipoAtalho).ID = Me.ID
        End Function



    End Class

End Namespace