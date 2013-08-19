<Serializable()> _
Public Class Credencial
    Implements ICredencial
    
    Private _Conexao As IConexao
    Private _Usuario As Usuario
    Private _EmpresaLogada As EmpresaVisivel

    Public Sub New(ByVal Conexao As IConexao, _
                   ByVal Usuario As Usuario, _
                   EmpresaLogada As EmpresaVisivel)
        _Usuario = Usuario
        _Conexao = Conexao
        _EmpresaLogada = EmpresaLogada
    End Sub

    Public ReadOnly Property Conexao() As IConexao Implements ICredencial.Conexao
        Get
            Return _Conexao
        End Get
    End Property

    Public ReadOnly Property Usuario() As Usuario Implements ICredencial.Usuario
        Get
            Return _Usuario
        End Get
    End Property

    Public ReadOnly Property EmpresaLogada As EmpresaVisivel Implements ICredencial.EmpresaLogada
        Get
            Return _EmpresaLogada
        End Get
    End Property
End Class