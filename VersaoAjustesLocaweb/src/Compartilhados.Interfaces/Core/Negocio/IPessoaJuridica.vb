Imports Compartilhados.Interfaces.Core.Negocio.Documento

Namespace Core.Negocio

    Public Interface IPessoaJuridica
        Inherits IPessoa

        Property NomeFantasia() As String
        Property Logomarca As String
    End Interface

End Namespace