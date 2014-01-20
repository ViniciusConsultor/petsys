Namespace Negocio

    <Serializable()> _
    Public Class SexoDoAnimal

        Private _ID As Char
        Private _Descricao As String

        Public Shared Macho As SexoDoAnimal = New SexoDoAnimal("M"c, "Macho")
        Public Shared Femea As SexoDoAnimal = New SexoDoAnimal("F"c, "Fêmea")

        Private Shared Lista As SexoDoAnimal() = {Macho, _
                                                Femea}

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

        Public Shared Function Obtenha(ByVal ID As Char) As SexoDoAnimal
            For Each Sexo As SexoDoAnimal In Lista
                If Sexo.ID = ID Then
                    Return Sexo
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of SexoDoAnimal)
            Return New List(Of SexoDoAnimal)(Lista)
        End Function

    End Class

End Namespace