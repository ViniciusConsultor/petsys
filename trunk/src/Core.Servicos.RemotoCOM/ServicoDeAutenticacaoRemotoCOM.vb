Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeAutenticacaoRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeAutenticacao, IServicoRemoto

    Private ServicoLocal As ServicoDeAutenticacaoLocal

    Public Sub SetaCredencial(ByVal Credencial As Compartilhados.ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeAutenticacaoLocal(Credencial)
    End Sub

    Public Function FacaLogon(ByVal LoginInformado As String, ByVal SenhaInformada As String) As IOperador Implements IServicoDeAutenticacao.FacaLogon
        Return ServicoLocal.FacaLogon(LoginInformado, SenhaInformada)
    End Function

End Class
