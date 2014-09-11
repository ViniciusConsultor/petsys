Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoSQLServer
    Inherits Conexao
    Implements IConexaoSQLServer

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.SQLSERVER
        End Get
    End Property

End Class
