Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoSQLite
    Inherits Conexao
    Implements IConexaoSQLite

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.SQLITE
        End Get
    End Property

End Class
