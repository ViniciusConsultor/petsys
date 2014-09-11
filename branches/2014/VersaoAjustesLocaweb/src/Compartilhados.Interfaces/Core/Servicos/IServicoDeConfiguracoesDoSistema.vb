Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeConfiguracoesDoSistema
        Inherits IServico

        Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema
        Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema)

    End Interface

End Namespace