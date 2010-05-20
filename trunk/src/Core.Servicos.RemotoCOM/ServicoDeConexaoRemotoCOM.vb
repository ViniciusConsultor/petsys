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
Public Class ServicoDeConexaoRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeConexao, IServicoRemoto

    Private ServicoLocal As ServicoDeConexaoLocal

    Public Function ObtenhaConexao() As IConexao Implements IServicoDeConexao.ObtenhaConexao
        Return ServicoLocal.ObtenhaConexao()
    End Function

    Public Sub Configure(ByVal Conexao As IConexao) Implements IServicoDeConexao.Configure
        ServicoLocal.Configure(Conexao)
    End Sub

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeConexaoLocal(Credencial)
    End Sub

End Class