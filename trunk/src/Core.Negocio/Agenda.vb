Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class Agenda
    Implements IAgenda

    Private _HorarioDeInicio As Date
    Public Property HorarioDeInicio() As Date Implements IAgenda.HorarioDeInicio
        Get
            Return _HorarioDeInicio
        End Get
        Set(ByVal value As Date)
            _HorarioDeInicio = value
        End Set
    End Property

    Private _HorarioDeTermino As Date
    Public Property HorarioDeTermino() As Date Implements IAgenda.HorarioDeTermino
        Get
            Return _HorarioDeTermino
        End Get
        Set(ByVal value As Date)
            _HorarioDeTermino = value
        End Set
    End Property

    Private _Pessoa As IPessoa
    Public Property Pessoa() As IPessoa Implements IAgenda.Pessoa
        Get
            Return _Pessoa
        End Get
        Set(ByVal value As IPessoa)
            _Pessoa = value
        End Set
    End Property

    Private _IntervaloEntreOsCompromissos As Date
    Public Property IntervaloEntreOsCompromissos() As Date Implements IAgenda.IntervaloEntreOsCompromissos
        Get
            Return _IntervaloEntreOsCompromissos
        End Get
        Set(ByVal value As Date)
            _IntervaloEntreOsCompromissos = value
        End Set
    End Property

End Class