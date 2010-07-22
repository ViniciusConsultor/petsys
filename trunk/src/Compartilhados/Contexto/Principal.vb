Imports Compartilhados.Visual

<Serializable()> _
Public Class Principal

    Private _Conexao As IConexao
    Private _Usuario As Usuario
    Private _Perfil As Perfil

    Public Property Conexao() As IConexao
        Get
            Return _Conexao
        End Get
        Set(ByVal value As IConexao)
            _Conexao = value
        End Set
    End Property

    Public Property Usuario() As Usuario
        Get
            Return _Usuario
        End Get
        Set(ByVal value As Usuario)
            _Usuario = value
        End Set
    End Property

    Public Property Perfil() As Perfil
        Get
            Return _Perfil
        End Get
        Set(ByVal value As Perfil)
            _Perfil = value
        End Set
    End Property

    Public Function EstaAutenticado() As Boolean
        Return Not IsNothing(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario)
    End Function

    Public Function EstaAutorizado(ByVal DiretivaDeAutorizacao As String) As Boolean
        'TODO: melhorar este comportamento= --> quando for passado vazio ou nothing o sistema vai autorizar
        If Not String.IsNullOrEmpty(DiretivaDeAutorizacao) Then
            Return _Usuario.ContemItem(DiretivaDeAutorizacao)
        End If
        Return True
    End Function

End Class