Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces
Imports Core.Servicos.Local
Imports System.EnterpriseServices

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeGeracaoDeIDRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeGeracaoDeID, IServicoRemoto

    Private ServicoLocal As ServicoDeGeracaoDeIDLocal

    Public Sub ArmazeneNumeroHigh(ByVal NumeroHigh As Integer) Implements IServicoDeGeracaoDeID.ArmazeneNumeroHigh
        ServicoLocal.ArmazeneNumeroHigh(NumeroHigh)
    End Sub

    Public Function ObtenhaNumeroHigh() As Integer Implements IServicoDeGeracaoDeID.ObtenhaNumeroHigh
        Return ServicoLocal.ObtenhaNumeroHigh
    End Function

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeGeracaoDeIDLocal(Credencial)
    End Sub

End Class