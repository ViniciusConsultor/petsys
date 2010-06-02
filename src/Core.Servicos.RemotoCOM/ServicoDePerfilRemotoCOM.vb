Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Visual

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDePerfilRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDePerfil, IServicoRemoto

    Private ServicoLocal As ServicoDePerfilLocal

    Public Function Obtenha(ByVal Usuario As Usuario) As Perfil Implements IServicoDePerfil.Obtenha
        Return ServicoLocal.Obtenha(Usuario)
    End Function

    Public Sub Remova(ByVal Usuario As Usuario) Implements IServicoDePerfil.Remova
        ServicoLocal.Remova(Usuario)
    End Sub

    Public Sub Salve(ByVal Usuario As Usuario, ByVal Perfil As Perfil) Implements IServicoDePerfil.Salve
        ServicoLocal.Salve(Usuario, Perfil)
    End Sub

    Public Sub SalveAtalhos(ByVal Usuario As Usuario, _
                            ByVal Atalhos As IList(Of Atalho)) Implements IServicoDePerfil.SalveAtalhos
        ServicoLocal.SalveAtalhos(Usuario, Atalhos)
    End Sub

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDePerfilLocal(Credencial)
    End Sub

End Class