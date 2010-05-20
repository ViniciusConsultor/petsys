Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Core.Servicos.Local
Imports System.EnterpriseServices
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio


< _
EventTrackingEnabled(), _
Transaction(TransactionOption.NotSupported), _
Synchronization(SynchronizationOption.Required) _
> _
Public Class ServicoDeGrupoRemotoCOM
    Inherits ServicedComponent
    Implements IServicoDeGrupo, IServicoRemoto

    Private ServicoLocal As ServicoDeGrupoLocal

    Public Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, _
                                                   ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo) Implements IServicoDeGrupo.ObtenhaGruposPorNomeComoFiltro
        Return ServicoLocal.ObtenhaGruposPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Modificar(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Modificar
        ServicoLocal.Modificar(Grupo)
    End Sub

    Public Overloads Function ObtenhaGrupo(ByVal ID As Long) As IGrupo Implements IServicoDeGrupo.ObtenhaGrupo
        Return ServicoLocal.ObtenhaGrupo(ID)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeGrupo.Remover
        ServicoLocal.Remover(ID)
    End Sub

    Public Sub SetaCredencial(ByVal Credencial As ICredencial) Implements IServicoRemoto.SetaCredencial
        ServicoLocal = New ServicoDeGrupoLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Inserir
        ServicoLocal.Inserir(Grupo)
    End Sub

End Class