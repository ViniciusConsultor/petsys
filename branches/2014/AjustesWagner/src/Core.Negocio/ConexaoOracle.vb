Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoOracle
    Inherits Conexao
    Implements IConexaoOracle

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.ORACLE
        End Get
    End Property

End Class
