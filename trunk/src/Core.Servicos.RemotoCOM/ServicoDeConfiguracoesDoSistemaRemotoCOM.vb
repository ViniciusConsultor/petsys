Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Servicos

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeConfiguracoesDoSistemaRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeConfiguracoesDoSistema, IServicoRemoto

    Private ServicoLocal As ServicoDeConfiguracoesDoSistemaLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeConfiguracoesDoSistemaLocal(Credencial)
    End Sub

    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IServicoDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Return ServicoLocal.ObtenhaConfiguracaoDoSistema()
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IServicoDeConfiguracoesDoSistema.Salve
        ServicoLocal.Salve(Configuracao)
    End Sub
End Class
