Imports Compartilhados.Interfaces
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDePessoaFisicaRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDePessoaFisica, IServicoRemoto

    Private ServicoLocal As ServicoDePessoaFisicaLocal

    Public Sub Inserir(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Inserir
        ServicoLocal.Inserir(Pessoa)
    End Sub

    Public Sub Modificar(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Modificar
        ServicoLocal.Modificar(Pessoa)
    End Sub

    Public Function ObtenhaPessoa(ByVal ID As Long) As IPessoaFisica Implements IServicoDePessoaFisica.ObtenhaPessoa
        Return ServicoLocal.ObtenhaPessoa(ID)
    End Function

    Public Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                    ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica) Implements IServicoDePessoaFisica.ObtenhaPessoasPorNomeComoFiltro
        Return ServicoLocal.ObtenhaPessoasPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDePessoaFisicaLocal(Credencial)
    End Sub

End Class