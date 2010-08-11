Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio

Public Class ServicoDeConexaoLocal
    Inherits Servico
    Implements IServicoDeConexao

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaConexao() As IConexao Implements IServicoDeConexao.ObtenhaConexao
        Dim Conexao As IConexao = Nothing
        Dim Provider As TipoDeProviderConexao

        Provider = TipoDeProviderConexao.Obtenha(CChar(Util.ObtenhaCaminhoDeConfiguracaoDoServicoDeConexao))

        Conexao = Me.ObtenhaInstancia(Provider)
        Conexao.StringDeConexao = Util.ObtenhaStringDeConexao()
        Return Conexao
    End Function

    Private Function ObtenhaInstancia(ByVal Provider As TipoDeProviderConexao) As IConexao
        Dim Instancia As IConexao = Nothing

        If Provider.Equals(TipoDeProviderConexao.ODBC) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoODBC)()
        ElseIf Provider.Equals(TipoDeProviderConexao.OLEBD) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoOLEDB)()
        ElseIf Provider.Equals(TipoDeProviderConexao.SQLSERVER) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoSQLServer)()
        ElseIf Provider.Equals(TipoDeProviderConexao.ORACLE) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoOracle)()
        ElseIf Provider.Equals(TipoDeProviderConexao.SQLITE) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoSQLite)()
        ElseIf Provider.Equals(TipoDeProviderConexao.MYSQL) Then
            Instancia = FabricaGenerica.GetInstancia.CrieObjeto(Of IConexaoMySQL)()
        End If

        If Instancia Is Nothing Then Throw New Exception("Não existe provider instalado para " & Provider.Descricao & ". Procure o suporte.")

        Return Instancia
    End Function

    Public Sub Configure(ByVal Conexao As IConexao) Implements IServicoDeConexao.Configure

    End Sub

End Class