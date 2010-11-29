Namespace Core.Negocio

    Public Interface IConfiguracaoDeAgendaDoUsuario

        Property Pessoa() As IPessoa
        Property HorarioDeInicio() As Date
        Property HorarioDeTermino() As Date
        Property IntervaloEntreOsCompromissos() As Date
        Property PessoaPadraoAoAcessarAAgenda() As IPessoa

    End Interface

End Namespace
