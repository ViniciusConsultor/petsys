Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio

Public Class ServicoDeGrupoLocal
    Inherits Servico
    Implements IServicoDeGrupo

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, _
                                                   ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo) Implements IServicoDeGrupo.ObtenhaGruposPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeGrupo
        Dim Grupos As IList(Of IGrupo)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()

        Try
            Grupos = Mapeador.ObtenhaGruposPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Grupos
    End Function

    Public Sub Inserir(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Inserir
        Dim Mapeador As IMapeadorDeGrupo

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()
        ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Grupo)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Modificar
        Dim Mapeador As IMapeadorDeGrupo

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()
        ValidaRegras(Grupo, Mapeador)

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Grupo)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Overloads Function ObtenhaGrupo(ByVal ID As Long) As IGrupo Implements IServicoDeGrupo.ObtenhaGrupo
        Dim Mapeador As IMapeadorDeGrupo
        Dim Grupo As IGrupo

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()

        Try
            Grupo = Mapeador.ObtenhaGrupo(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Grupo
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeGrupo.Remover
        Dim Mapeador As IMapeadorDeGrupo

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupo)()
        ValidaSePodeSerExcluido(ID, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.Excluir(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub ValidaRegras(ByVal Grupo As IGrupo, _
                             ByVal Mapeador As IMapeadorDeGrupo)

        If Mapeador.Existe(Grupo) Then
            Throw New BussinesException("Já existe um grupo cadastrado com este nome.")
        End If
    End Sub

    Private Sub ValidaSePodeSerExcluido(ByVal Id As Long, _
                                        ByVal Mapeador As IMapeadorDeGrupo)
        If Mapeador.EstaSendoUtilizado(Id) Then
            Throw New BussinesException("Grupo não pode ser excluído, pois o mesmo está sendo utilizado.")
        End If
    End Sub

End Class