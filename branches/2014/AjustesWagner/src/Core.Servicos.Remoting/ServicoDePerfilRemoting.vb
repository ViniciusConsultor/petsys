Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Visual

Public Class ServicoDePerfilRemoting
    Inherits ServicoRemoto
    Implements IServicoDePerfil

    Private _ServicoLocal As ServicoDePerfilLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDePerfilLocal(Credencial)
    End Sub

    Public Function Obtenha(ByVal Usuario As Usuario) As Perfil Implements IServicoDePerfil.Obtenha
        Return _ServicoLocal.Obtenha(Usuario)
    End Function

    Public Sub Remova(ByVal Usuario As Usuario) Implements IServicoDePerfil.Remova
        _ServicoLocal.Remova(Usuario)
    End Sub

    Public Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil) Implements IServicoDePerfil.Salve
        _ServicoLocal.Salve(Usuario, Perfil)
    End Sub

    Public Sub SalveAtalhos(ByVal Usuario As Usuario, ByVal Atalhos As IList(Of Atalho)) Implements IServicoDePerfil.SalveAtalhos
        _ServicoLocal.SalveAtalhos(Usuario, Atalhos)
    End Sub

End Class
