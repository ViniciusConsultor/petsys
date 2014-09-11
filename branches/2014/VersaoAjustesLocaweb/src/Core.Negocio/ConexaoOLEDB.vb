Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class ConexaoOLEDB
    Inherits Conexao
    Implements IConexaoOLEDB

    Public Overrides ReadOnly Property Provider() As TipoDeProviderConexao
        Get
            Return TipoDeProviderConexao.OLEBD
        End Get
    End Property

End Class