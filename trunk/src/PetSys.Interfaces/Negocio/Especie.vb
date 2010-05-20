Namespace Negocio

    <Serializable()> _
    Public Class Especie

        Private _ID As Char
        Private _Descricao As String

        Public Shared Canina As Especie = New Especie("C"c, "Canina")
        Public Shared Felina As Especie = New Especie("F"c, "Felina")
        Public Shared Equina As Especie = New Especie("E"c, "Equina")
        Public Shared Bovina As Especie = New Especie("B"c, "Bovina")
        Public Shared Repteis As Especie = New Especie("R"c, "Repteis")
        Public Shared Passaros As Especie = New Especie("P"c, "Pássaros")
        Public Shared Outros As Especie = New Especie("O"c, "Outros")

        Private Shared Lista As Especie() = {Canina, Felina, Equina, Bovina, Repteis, Passaros, Outros}

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

        Public Shared Function Obtenha(ByVal ID As Char) As Especie
            For Each Especie As Especie In Lista
                If Especie.ID = ID Then
                    Return Especie
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of Especie)
            Return New List(Of Especie)(Lista)
        End Function

    End Class

End Namespace