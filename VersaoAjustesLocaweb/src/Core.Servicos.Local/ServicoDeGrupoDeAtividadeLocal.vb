Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeGrupoDeAtividadeLocal
    Inherits Servico
    Implements IServicoDeGrupoDeAtividade

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Insira(ByVal GrupoDeAtividade As IGrupoDeAtividade) Implements IServicoDeGrupoDeAtividade.Insira
        Dim Mapeador As IMapeadorDeGrupoDeAtividade

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeAtividade)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Insira(GrupoDeAtividade)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal GrupoDeAtividade As IGrupoDeAtividade) Implements IServicoDeGrupoDeAtividade.Modificar
        Dim Mapeador As IMapeadorDeGrupoDeAtividade

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeAtividade)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(GrupoDeAtividade)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal ID As Long) As IGrupoDeAtividade Implements IServicoDeGrupoDeAtividade.Obtenha
        Dim Mapeador As IMapeadorDeGrupoDeAtividade

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeAtividade)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of IGrupoDeAtividade) Implements IServicoDeGrupoDeAtividade.ObtenhaPorNome
        Dim Mapeador As IMapeadorDeGrupoDeAtividade

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeAtividade)()

        Try
            Return Mapeador.ObtenhaPorNome(Filtro, Quantidade)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeGrupoDeAtividade.Remover
        Dim Mapeador As IMapeadorDeGrupoDeAtividade

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeAtividade)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remover(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class
