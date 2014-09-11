Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Servicos.Local

Public Class ServicoDeConexaoRemoting
    Inherits ServicoRemoto
    Implements IServicoDeConexao

    Private _ServicoLocal As ServicoDeConexaoLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeConexaoLocal(Credencial)
    End Sub

    Public Sub Configure(ByVal Conexao As IConexao) Implements IServicoDeConexao.Configure
        _ServicoLocal.Configure(Conexao)
    End Sub

    Public Function ObtenhaConexao() As IConexao Implements IServicoDeConexao.ObtenhaConexao
        Return _ServicoLocal.ObtenhaConexao()
    End Function

    Public Function ObtenhaConexao(ByVal Provider As TipoDeProviderConexao, ByVal StringDeConexao As String, ByVal UtilizaUppercase As Boolean) As IConexao Implements IServicoDeConexao.ObtenhaConexao
        Return _ServicoLocal.ObtenhaConexao(Provider, StringDeConexao, UtilizaUppercase)
    End Function

End Class
