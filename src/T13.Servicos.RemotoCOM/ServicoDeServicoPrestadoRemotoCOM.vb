Imports System.EnterpriseServices
Imports Compartilhados
Imports T13.Interfaces.Servicos
Imports T13.Servicos.Local
Imports T13.Interfaces.Negocio

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeServicoPrestadoRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeServicoPrestado, IServicoRemoto

    Private ServicoLocal As ServicoDeServicoPrestadoLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeServicoPrestadoLocal(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeServicoPrestado.Excluir
        ServicoLocal.Excluir(ID)
    End Sub

    Public Sub Inserir(ByVal Servico As IServicoPrestado) Implements IServicoDeServicoPrestado.Inserir
        ServicoLocal.Inserir(Servico)
    End Sub

    Public Sub Modificar(ByVal Sevico As IServicoPrestado) Implements IServicoDeServicoPrestado.Modificar
        ServicoLocal.Modificar(Sevico)
    End Sub

    Public Function ObtenhaServico(ByVal ID As Long) As IServicoPrestado Implements IServicoDeServicoPrestado.ObtenhaServico
        Return ServicoLocal.ObtenhaServico(ID)
    End Function

    Public Function ObtenhaServicoPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As System.Collections.Generic.IList(Of Interfaces.Negocio.IServicoPrestado) Implements Interfaces.Servicos.IServicoDeServicoPrestado.ObtenhaServicoPorNomeComoFiltro
        Return ServicoLocal.ObtenhaServicoPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

End Class
