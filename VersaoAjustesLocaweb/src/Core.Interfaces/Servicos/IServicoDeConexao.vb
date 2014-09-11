Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeConexao
        Inherits IServico

        Function ObtenhaConexao() As IConexao
        Sub Configure(ByVal Conexao As IConexao)
        Function ObtenhaConexao(ByVal Provider As TipoDeProviderConexao, ByVal StringDeConexao As String, ByVal UtilizaUppercase As Boolean) As IConexao

    End Interface

End Namespace