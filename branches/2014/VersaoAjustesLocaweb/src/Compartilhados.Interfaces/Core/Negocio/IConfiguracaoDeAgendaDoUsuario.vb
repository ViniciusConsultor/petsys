Namespace Core.Negocio

    Public Interface IConfiguracaoDeAgendaDoUsuario

        Property Pessoa() As IPessoaFisica
        Property HorarioDeInicio() As Date
        Property HorarioDeTermino() As Date
        Property IntervaloEntreOsCompromissos() As Date
        Property PessoaPadraoAoAcessarAAgenda() As IPessoaFisica

    End Interface

End Namespace
