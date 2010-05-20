Imports Compartilhados
Imports Core.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeConfiguracoesDoSistema
        Inherits IServico

        Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema
        Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema)

    End Interface

End Namespace