Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

<Serializable()> _
Public MustInherit Class Conexao
    Implements IConexao

    Private _StringDeConexao As String

    Public Property StringDeConexao() As String Implements IConexao.StringDeConexao
        Get
            Return _StringDeConexao
        End Get
        Set(ByVal value As String)
            _StringDeConexao = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Provider() As TipoDeProviderConexao Implements IConexao.Provider

End Class