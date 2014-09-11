Namespace Core.Negocio

    Public Interface IEmpresa
        Inherits IPapelPessoa

        Property DataDaAtivacao() As Date
        Function PodeUtilizarOSistema() As Boolean

    End Interface

End Namespace