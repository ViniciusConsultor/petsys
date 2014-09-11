Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface ISenha

        Sub SenhaEhValida(ByVal SenhaInformada As String)
        ReadOnly Property DataDeCadastro() As Date
    End Interface

End Namespace