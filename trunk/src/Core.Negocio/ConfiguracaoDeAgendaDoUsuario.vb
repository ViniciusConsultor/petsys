Imports Compartilhados.Interfaces.Core.Negocio

Public Class ConfiguracaoDeAgendaDoUsuario
    Implements IConfiguracaoDeAgendaDoUsuario

    Private _HorarioDeInicio As Date
    Public Property HorarioDeInicio() As Date Implements IConfiguracaoDeAgendaDoUsuario.HorarioDeInicio
        Get
            Return _HorarioDeInicio
        End Get
        Set(ByVal value As Date)
            _HorarioDeInicio = value
        End Set
    End Property

    Private _HorarioDeTermino As Date
    Public Property HorarioDeTermino() As Date Implements IConfiguracaoDeAgendaDoUsuario.HorarioDeTermino
        Get
            Return _HorarioDeTermino
        End Get
        Set(ByVal value As Date)
            _HorarioDeTermino = value
        End Set
    End Property

    Private _IntervaloEntreOsCompromisso As Date
    Public Property IntervaloEntreOsCompromissos() As Date Implements IConfiguracaoDeAgendaDoUsuario.IntervaloEntreOsCompromissos
        Get
            Return _IntervaloEntreOsCompromisso
        End Get
        Set(ByVal value As Date)
            _IntervaloEntreOsCompromisso = value
        End Set
    End Property

    Private _Pessoa As IPessoa
    Public Property Pessoa() As IPessoa Implements IConfiguracaoDeAgendaDoUsuario.Pessoa
        Get
            Return _Pessoa
        End Get
        Set(ByVal value As IPessoa)
            _Pessoa = value
        End Set
    End Property

    Private _PessoaPadraoAoAcessarAAgenda As IPessoa
    Public Property PessoaPadraoAoAcessarAAgenda() As IPessoa Implements IConfiguracaoDeAgendaDoUsuario.PessoaPadraoAoAcessarAAgenda
        Get
            Return _PessoaPadraoAoAcessarAAgenda
        End Get
        Set(ByVal value As IPessoa)
            _PessoaPadraoAoAcessarAAgenda = value
        End Set
    End Property
End Class
