Namespace Core.Negocio

    <Serializable()> _
    Public Class DiaDaSemana

        Private _Dia As DayOfWeek
        Private _Descricao As String

        Public Shared Domingo As DiaDaSemana = New DiaDaSemana(DayOfWeek.Sunday, "Domingo")
        Public Shared Segunda As DiaDaSemana = New DiaDaSemana(DayOfWeek.Monday, "Segunda-feira")
        Public Shared Terca As DiaDaSemana = New DiaDaSemana(DayOfWeek.Tuesday, "Terça-feira")
        Public Shared Quarta As DiaDaSemana = New DiaDaSemana(DayOfWeek.Wednesday, "Quarta-feira")
        Public Shared Quinta As DiaDaSemana = New DiaDaSemana(DayOfWeek.Thursday, "Quinta-feira")
        Public Shared Sexta As DiaDaSemana = New DiaDaSemana(DayOfWeek.Friday, "Sexta-feira")
        Public Shared Sabado As DiaDaSemana = New DiaDaSemana(DayOfWeek.Saturday, "Sábado")

        Private Sub New(ByVal Dia As DayOfWeek, ByVal Descricao As String)
            _Dia = Dia
            _Descricao = Descricao
        End Sub

        Private Shared Lista As DiaDaSemana() = {Domingo, Segunda, Terca, _
                                                 Quarta, Quinta, Sexta, Sabado}

        Public ReadOnly Property Dia() As DayOfWeek
            Get
                Return _Dia
            End Get
        End Property

        Public ReadOnly Property IDDoDia() As Short
            Get
                Return CShort(Dia)
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Short) As DiaDaSemana
            For Each DiaDaSemana As DiaDaSemana In Lista
                If DiaDaSemana.Dia = ID Then
                    Return DiaDaSemana
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of DiaDaSemana)
            Return New List(Of DiaDaSemana)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, DiaDaSemana).Dia = Me.Dia
        End Function


    End Class

End Namespace