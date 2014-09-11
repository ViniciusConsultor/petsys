Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoODBC
    Inherits Conexao
    Implements IConexaoODBC

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.ODBC
        End Get
    End Property

End Class