Public Class UtilitarioDeData

    Public Shared Function ObtenhaDiaDaSemanaDiaDoMesMesAnoEmStr(ByVal Data As Date) As String
        Return String.Concat(DiaDaSemanaPortugues.Obtenha(Data.DayOfWeek).Nome, ", ", Data.Day, " de ", MesEmPortugues.Obtenha(Data.Month).Nome, " de ", Data.Year)
    End Function

    Private Class DiaDaSemanaPortugues

        Public Shared Domingo As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Sunday, "Domingo")
        Public Shared Segunda As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Monday, "Segunda-feira")
        Public Shared Terca As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Tuesday, "Terça-feira")
        Public Shared Quarta As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Wednesday, "Quarta-feira")
        Public Shared Quinta As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Thursday, "Quinta-feira")
        Public Shared Sexta As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Friday, "Sexta-feira")
        Public Shared Sabado As DiaDaSemanaPortugues = New DiaDaSemanaPortugues(DayOfWeek.Saturday, "Sábado")

        Private _DiaDaSemanaDotNet As DayOfWeek
        Private _Nome As String

        Private Shared DiasDaSemana As DiaDaSemanaPortugues() = {Domingo, Segunda, Terca, Quarta, Quinta, Sexta, Sabado}

        Private Sub New(ByVal DiaDaSemanaDotNet As DayOfWeek, ByVal Nome As String)
            _DiaDaSemanaDotNet = DiaDaSemanaDotNet
            _Nome = Nome
        End Sub

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public Shared Function Obtenha(ByVal DiaDaSemanaDotNet As DayOfWeek) As DiaDaSemanaPortugues
            For Each Item As DiaDaSemanaPortugues In DiasDaSemana
                If Item._DiaDaSemanaDotNet.Equals(DiaDaSemanaDotNet) Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function
    End Class

    Private Class MesEmPortugues

        Public Shared Janeiro As MesEmPortugues = New MesEmPortugues(1, "Janeiro")
        Public Shared Fevereiro As MesEmPortugues = New MesEmPortugues(2, "Fevereiro")
        Public Shared Marco As MesEmPortugues = New MesEmPortugues(3, "Março")
        Public Shared Abril As MesEmPortugues = New MesEmPortugues(4, "Abril")
        Public Shared Maio As MesEmPortugues = New MesEmPortugues(5, "Maio")
        Public Shared Junho As MesEmPortugues = New MesEmPortugues(6, "Junho")
        Public Shared Julho As MesEmPortugues = New MesEmPortugues(7, "Julho")
        Public Shared Agosto As MesEmPortugues = New MesEmPortugues(8, "Agosto")
        Public Shared Setembro As MesEmPortugues = New MesEmPortugues(9, "Setembro")
        Public Shared Outubro As MesEmPortugues = New MesEmPortugues(10, "Outubro")
        Public Shared Novembro As MesEmPortugues = New MesEmPortugues(11, "Novembro")
        Public Shared Dezembro As MesEmPortugues = New MesEmPortugues(12, "Dezembro")

        Private _MesDotNet As Integer
        Private _Nome As String

        Private Shared Meses As MesEmPortugues() = {Janeiro, Fevereiro, Marco, Abril, Maio, Junho, Julho, Agosto, Setembro, Outubro, Novembro, Dezembro}

        Private Sub New(ByVal MesDotNet As Integer, ByVal Nome As String)
            _MesDotNet = MesDotNet
            _Nome = Nome
        End Sub

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public ReadOnly Property MesDotNet() As Integer
            Get
                Return _MesDotNet
            End Get
        End Property

        Public Shared Function Obtenha(ByVal MesDotMes As Integer) As MesEmPortugues
            For Each Item As MesEmPortugues In Meses
                If Item.MesDotNet = MesDotMes Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function
    End Class

End Class