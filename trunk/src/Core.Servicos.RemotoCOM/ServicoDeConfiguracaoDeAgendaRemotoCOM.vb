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
Public Class ServicoDeConfiguracaoDeAgendaRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeConfiguracaoDeAgenda, IServicoRemoto

    Private ServicoLocal As ServicoDeConfiguracaoDeAgendaLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeConfiguracaoDeAgendaLocal(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeConfiguracaoDeAgenda.Excluir
        ServicoLocal.Excluir(ID)
    End Sub

    Public Sub Inserir(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.Inserir
        ServicoLocal.Inserir(ConfiguracaoDeAgenda)
    End Sub

    Public Sub Modificar(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.Modificar
        ServicoLocal.Modificar(ConfiguracaoDeAgenda)
    End Sub

    Public Function ObtenhaConfiguracao(ByVal ID As Long) As IConfiguracaoDeAgenda Implements IServicoDeConfiguracaoDeAgenda.ObtenhaConfiguracao
        Return ServicoLocal.ObtenhaConfiguracao(ID)
    End Function

    Public Function ObtenhaConfiguracoesPorNomeComoFiltro(ByVal Nome As String, _
                                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.ObtenhaConfiguracoesPorNomeComoFiltro
        Return ServicoLocal.ObtenhaConfiguracoesPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

End Class