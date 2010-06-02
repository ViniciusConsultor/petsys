Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeMenuRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeMenu, IServicoRemoto

    Private ServicoLocal As ServicoDeMenuLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeMenuLocal(Credencial)
    End Sub

    Public Function ObtenhaMenu() As IMenuComposto Implements IServicoDeMenu.ObtenhaMenu
        Return ServicoLocal.ObtenhaMenu()
    End Function

End Class