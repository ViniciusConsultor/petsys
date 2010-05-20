Namespace Core.Negocio

    <Serializable()> _
    Public Class Sexo

        Private _ID As Char
        Private _Descricao As String

        Public Shared Masculino As Sexo = New Sexo("M"c, "Masculino")
        Public Shared Feminino As Sexo = New Sexo("F"c, "Feminino")

        Private Shared ListaDeSexo As Sexo() = {Masculino, Feminino}

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

        Public Shared Function ObtenhaSexo(ByVal ID As Char) As Sexo
            For Each Sexo As Sexo In ListaDeSexo
                If Sexo.ID = ID Then
                    Return Sexo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodosSexo() As IList(Of Sexo)
            Return New List(Of Sexo)(ListaDeSexo)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, Sexo).ID = Me.ID
        End Function

    End Class

End Namespace