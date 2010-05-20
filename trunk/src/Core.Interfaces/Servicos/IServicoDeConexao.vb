Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeConexao
        Inherits IServico

        Function ObtenhaConexao() As IConexao
        Sub Configure(ByVal Conexao As IConexao)

    End Interface

End Namespace