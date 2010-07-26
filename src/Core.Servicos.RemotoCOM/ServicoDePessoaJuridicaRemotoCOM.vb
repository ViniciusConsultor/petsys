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
Public Class ServicoDePessoaJuridicaRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDePessoaJuridica, IServicoRemoto

    Private ServicoLocal As ServicoDePessoaJuridicaLocal

    Public Sub Inserir(ByVal Pessoa As IPessoaJuridica) Implements IServicoDePessoaJuridica.Inserir
        ServicoLocal.Inserir(Pessoa)
    End Sub

    Public Sub Modificar(ByVal Pessoa As IPessoaJuridica) Implements IServicoDePessoaJuridica.Modificar
        ServicoLocal.Modificar(Pessoa)
    End Sub

    Public Function ObtenhaPessoa(ByVal ID As Long) As IPessoaJuridica Implements IServicoDePessoaJuridica.ObtenhaPessoa
        Return ServicoLocal.ObtenhaPessoa(ID)
    End Function

    Public Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                    ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaJuridica) Implements IServicoDePessoaJuridica.ObtenhaPessoasPorNomeComoFiltro
        Return ServicoLocal.ObtenhaPessoasPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDePessoaJuridicaLocal(Credencial)
    End Sub

    Public Sub Remover(ByVal Pessoa As IPessoaJuridica) Implements IServicoDePessoaJuridica.Remover
        ServicoLocal.Remover(Pessoa)
    End Sub

End Class