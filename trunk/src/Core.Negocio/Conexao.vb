Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

<Serializable()> _
Public MustInherit Class Conexao
    Implements IConexao

    Private _StringDeConexao As String
    Private _SistemaUtilizaSQLUpperCase As Boolean

    Public Property StringDeConexao() As String Implements IConexao.StringDeConexao
        Get
            Return _StringDeConexao
        End Get
        Set(ByVal value As String)
            _StringDeConexao = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Provider() As TipoDeProviderConexao Implements IConexao.Provider

    Public Property SistemaUtilizaSQLUpperCase() As Boolean Implements IConexao.SistemaUtilizaSQLUpperCase
        Get
            Return _SistemaUtilizaSQLUpperCase
        End Get
        Set(ByVal value As Boolean)
            _SistemaUtilizaSQLUpperCase = value
        End Set
    End Property

End Class