Imports Core.Interfaces.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ConfiguracaoDeAgenda
    Implements IConfiguracaoDeAgenda

    Private _HoraDeInicio As Date
    Private _HoraDeTermino As Date
    Private _IntervaloEntreHorarios As Date
    Private _ID As Nullable(Of Long)
    Private _Nome As String
    Private _PrimeiroDiaDaSemana As DiaDaSemana

    Public Function ObtenhaHorariosDaAgenda() As IList(Of Date) Implements IConfiguracaoDeAgenda.ObtenhaHorariosDaAgenda
        Dim DiaDaSemana As DayOfWeek = Now.DayOfWeek
        Dim Horarios As IList(Of Date)

        Horarios = New List(Of Date)

        Dim HorarioParaALista As Date = HoraDeInicio

        While HorarioParaALista = HoraDeTermino
            HorarioParaALista.AddHours(_IntervaloEntreHorarios.Hour)
            HorarioParaALista.AddMinutes(_IntervaloEntreHorarios.Minute)
            Horarios.Add(HorarioParaALista)
        End While

        Return Horarios
    End Function

    Public Property HoraDeInicio() As Date Implements IConfiguracaoDeAgenda.HoraDeInicio
        Get
            Return _HoraDeInicio
        End Get
        Set(ByVal value As Date)
            _HoraDeInicio = value
        End Set
    End Property

    Public Property HoraDeTermino() As Date Implements IConfiguracaoDeAgenda.HoraDeTermino
        Get
            Return _HoraDeTermino
        End Get
        Set(ByVal value As Date)
            _HoraDeTermino = value
        End Set
    End Property

    Public Property ID() As Long? Implements IConfiguracaoDeAgenda.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Nome() As String Implements IConfiguracaoDeAgenda.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property IntervaloEntreHorarios() As Date Implements IConfiguracaoDeAgenda.IntervaloEntreHorarios
        Get
            Return _IntervaloEntreHorarios
        End Get
        Set(ByVal value As Date)
            _IntervaloEntreHorarios = value
        End Set
    End Property

    Public Property PrimeiroDiaDaSemana() As DiaDaSemana Implements IConfiguracaoDeAgenda.PrimeiroDiaDaSemana
        Get
            Return _PrimeiroDiaDaSemana
        End Get
        Set(ByVal value As DiaDaSemana)
            _PrimeiroDiaDaSemana = value
        End Set
    End Property

End Class