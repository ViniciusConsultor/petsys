Namespace Core.Negocio

    Public Interface IAgenda

        Property Pessoa() As IPessoa
        Property HorarioDeInicio() As Date
        Property HorarioDeTermino() As Date
        Property IntervaloEntreOsCompromissos() As Date

    End Interface

End Namespace