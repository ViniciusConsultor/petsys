Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface IVeterinario
        Inherits IPapelPessoa

        Property CRMV() As String
        Property UF() As UF

    End Interface

End Namespace