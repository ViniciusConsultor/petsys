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
Public Class ServicoDeClienteRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeCliente, IServicoRemoto

    Private ServicoLocal As ServicoDeClienteLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeClienteLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Cliente As ICliente) Implements IServicoDeCliente.Inserir
        ServicoLocal.Inserir(Cliente)
    End Sub

    Public Sub Modificar(ByVal Cliente As ICliente) Implements IServicoDeCliente.Modificar
        ServicoLocal.Modificar(Cliente)
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As ICliente Implements IServicoDeCliente.Obtenha
        Return ServicoLocal.Obtenha(Pessoa)
    End Function

    Public Function Obtenha(ByVal ID As Long) As ICliente Implements IServicoDeCliente.Obtenha
        Return ServicoLocal.Obtenha(ID)
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente) Implements IServicoDeCliente.ObtenhaPorNomeComoFiltro
        Return ServicoLocal.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeCliente.Remover
        ServicoLocal.Remover(ID)
    End Sub

End Class