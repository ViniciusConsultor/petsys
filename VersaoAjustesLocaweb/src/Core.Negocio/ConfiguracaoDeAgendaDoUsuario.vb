Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
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

    Private _Pessoa As IPessoaFisica
    Public Property Pessoa() As IPessoaFisica Implements IConfiguracaoDeAgendaDoUsuario.Pessoa
        Get
            Return _Pessoa
        End Get
        Set(ByVal value As IPessoaFisica)
            _Pessoa = value
        End Set
    End Property

    Private _PessoaPadraoAoAcessarAAgenda As IPessoaFisica
    Public Property PessoaPadraoAoAcessarAAgenda() As IPessoaFisica Implements IConfiguracaoDeAgendaDoUsuario.PessoaPadraoAoAcessarAAgenda
        Get
            Return _PessoaPadraoAoAcessarAAgenda
        End Get
        Set(ByVal value As IPessoaFisica)
            _PessoaPadraoAoAcessarAAgenda = value
        End Set
    End Property
End Class
