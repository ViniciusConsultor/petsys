Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeAutorizacaoLocal
    Inherits Servico
    Implements IServicoDeAutorizacao

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaModulosDisponiveis() As IList(Of IModulo) Implements IServicoDeAutorizacao.ObtenhaModulosDisponiveis
        Dim Mapeador As IMapeadorDeAutorizacao
        Dim Modulos As IList(Of IModulo)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAutorizacao)()

        Try
            Modulos = Mapeador.ObtenhaModulosDisponiveis()
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Modulos
    End Function

    Public Sub Modifique(ByVal IDGrupo As Long, ByVal Diretivas As IList(Of IDiretivaDeSeguranca)) Implements IServicoDeAutorizacao.Modifique
        Dim Mapeador As IMapeadorDeAutorizacao

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAutorizacao)()
        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modifique(IDGrupo, Diretivas)
            ServerUtils.CommitTransaction()
        Catch ex As Exception
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca) Implements IServicoDeAutorizacao.ObtenhaDiretivasDeSegurancaDoGrupo
        Dim Mapeador As IMapeadorDeAutorizacao

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAutorizacao)()

        Try
            Return Mapeador.ObtenhaDiretivasDeSegurancaDoGrupo(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class