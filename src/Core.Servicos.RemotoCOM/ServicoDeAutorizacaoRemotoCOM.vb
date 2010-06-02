Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados
Imports Core.Interfaces.Negocio

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeAutorizacaoRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeAutorizacao, IServicoRemoto

    Private ServicoLocal As ServicoDeAutorizacaoLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeAutorizacaoLocal(Credencial)
    End Sub

    Public Sub Modifique(ByVal IDGrupo As Long, ByVal Diretivas As IList(Of IDiretivaDeSeguranca)) Implements IServicoDeAutorizacao.Modifique
        ServicoLocal.Modifique(IDGrupo, Diretivas)
    End Sub

    Public Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca) Implements IServicoDeAutorizacao.ObtenhaDiretivasDeSegurancaDoGrupo
        Return ServicoLocal.ObtenhaDiretivasDeSegurancaDoGrupo(ID)
    End Function

    Public Function ObtenhaModulosDisponiveis() As IList(Of IModulo) Implements IServicoDeAutorizacao.ObtenhaModulosDisponiveis
        Return ServicoLocal.ObtenhaModulosDisponiveis()
    End Function

End Class
