Imports Core.Interfaces.Negocio
Imports Compartilhados

<Serializable()> _
Public Class Senha
    Implements ISenha

    Private _SenhaCriptografada As String
    Private _DataDeCadastro As Date

    Public Sub New(ByVal SenhaCriptografada As String, _
                   ByVal DataDeCadatro As Date)
        _SenhaCriptografada = SenhaCriptografada
        _DataDeCadastro = DataDeCadatro
    End Sub

    Public Sub SenhaEhValida(ByVal SenhaInformada As String) Implements ISenha.SenhaEhValida
        If Not _SenhaCriptografada.Equals(SenhaInformada) Then
            Throw New BussinesException("Senha inválida.")
        End If
    End Sub

    Public ReadOnly Property DataDeCadastro() As Date Implements ISenha.DataDeCadastro
        Get
            Return _DataDeCadastro
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return _SenhaCriptografada
    End Function

End Class