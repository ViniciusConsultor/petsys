Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoMySQL
    Inherits Conexao
    Implements IConexaoMySQL

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.MYSQL
        End Get
    End Property

End Class