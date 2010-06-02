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
Public Class ServicoDeLancamentoDeServicosRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeLancamentoDeServicos, IServicoRemoto

    Private ServicoLocal As ServicoDeLancamentoDeServicosLocal

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeLancamentoDeServicosLocal(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeLancamentoDeServicos.Excluir
        ServicoLocal.Excluir(ID)
    End Sub

    Public Sub Inserir(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.Inserir
        ServicoLocal.Inserir(Lancamento)
    End Sub

    Public Sub Modificar(ByVal Lancamento As ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.Modificar
        ServicoLocal.Modificar(Lancamento)
    End Sub

    Public Function ObtenhaLancamento(ByVal ID As Long) As ILacamentoDeServicosPrestados Implements IServicoDeLancamentoDeServicos.ObtenhaLancamento
        Return ServicoLocal.ObtenhaLancamento(ID)
    End Function

    Public Function ObtenhaLancamentosTardio(ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Return ServicoLocal.ObtenhaLancamentosTardio(DataInicio, DataFim)
    End Function

    Public Function ObtenhaLancamentosTardio(ByVal IDCliente As Long) As IList(Of ILacamentoDeServicosPrestados) Implements IServicoDeLancamentoDeServicos.ObtenhaLancamentosTardio
        Return ServicoLocal.ObtenhaLancamentosTardio(IDCliente)
    End Function

    Public Function ObtenhaProximoNumeroDisponivel() As Long Implements IServicoDeLancamentoDeServicos.ObtenhaProximoNumeroDisponivel
        Return ServicoLocal.ObtenhaProximoNumeroDisponivel
    End Function

End Class