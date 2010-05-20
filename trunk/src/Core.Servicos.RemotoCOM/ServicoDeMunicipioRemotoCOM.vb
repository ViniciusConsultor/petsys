Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces
Imports Core.Servicos.Local
Imports System.EnterpriseServices

< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeMunicipioRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeMunicipio, IServicoRemoto

    Private ServicoLocal As ServicoDeMunicipioLocal

    Public Sub Excluir(ByVal Id As Long) Implements IServicoDeMunicipio.Excluir
        ServicoLocal.Excluir(Id)
    End Sub

    Public Sub Inserir(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Inserir
        ServicoLocal.Inserir(Municipio)
    End Sub

    Public Sub Modificar(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Modificar
        ServicoLocal.Modificar(Municipio)
    End Sub

    Public Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio Implements IServicoDeMunicipio.ObtenhaMunicipio
        Return ServicoLocal.ObtenhaMunicipio(Id)
    End Function

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeMunicipioLocal(Credencial)
    End Sub

    Public Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio) Implements IServicoDeMunicipio.ObtenhaMunicipiosPorNomeComoFiltro
        Return ServicoLocal.ObtenhaMunicipiosPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

End Class