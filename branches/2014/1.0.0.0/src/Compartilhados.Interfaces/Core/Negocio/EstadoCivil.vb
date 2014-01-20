Namespace Core.Negocio

    <Serializable()> _
    Public Class EstadoCivil

        Private _ID As Char
        Private _Descricao As String

        Public Shared Solteiro As EstadoCivil = New EstadoCivil("S"c, "Solteiro")
        Public Shared Casado As EstadoCivil = New EstadoCivil("C"c, "Casado")
        Public Shared SeparadoJudicialmente As EstadoCivil = New EstadoCivil("E"c, "Separado judicialmente")
        Public Shared Divorciado As EstadoCivil = New EstadoCivil("D"c, "Divorciado")
        Public Shared Viuvo As EstadoCivil = New EstadoCivil("V"c, "Viúvo")
        Public Shared Outros As EstadoCivil = New EstadoCivil("O"c, "Outros")
        Public Shared Ignorado As EstadoCivil = New EstadoCivil("I"c, "Ignorado")

        Private Shared ListaDeEstadosCivis As EstadoCivil() = {Solteiro, Casado, SeparadoJudicialmente, _
                                                               Divorciado, Viuvo, Outros, Ignorado}

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

        Public Shared Function ObtenhaEstadoCivil(ByVal ID As Char) As EstadoCivil
            For Each EstadoCivil As EstadoCivil In ListaDeEstadosCivis
                If EstadoCivil.ID = ID Then
                    Return EstadoCivil
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of EstadoCivil)
            Return New List(Of EstadoCivil)(ListaDeEstadosCivis)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, EstadoCivil).ID = Me.ID
        End Function

    End Class

End Namespace
