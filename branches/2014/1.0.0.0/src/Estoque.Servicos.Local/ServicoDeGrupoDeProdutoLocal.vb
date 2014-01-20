Imports Estoque.Interfaces.Servicos
Imports Compartilhados
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeGrupoDeProdutoLocal
    Inherits Servico
    Implements IServicoDeGrupoDeProduto

    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Atualizar(ByVal GrupoDeProdutos As IGrupoDeProduto) Implements IServicoDeGrupoDeProduto.Atualizar
        Dim Mapeador As IMapeadorDeGrupoDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.Atualizar(GrupoDeProdutos)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Inserir(ByVal GrupoDeProdutos As IGrupoDeProduto) Implements IServicoDeGrupoDeProduto.Inserir
        Dim Mapeador As IMapeadorDeGrupoDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(GrupoDeProdutos)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaGrupoDeProdutos(ByVal ID As Long) As IGrupoDeProduto Implements IServicoDeGrupoDeProduto.ObtenhaGrupoDeProdutos
        Dim Mapeador As IMapeadorDeGrupoDeProduto
        Dim Grupo As IGrupoDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeProduto)()

        Try
            Grupo = Mapeador.ObtenhaGrupoDeProdutos(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Grupo
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeGrupoDeProduto.Remover
        Dim Mapeador As IMapeadorDeGrupoDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
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

    Public Function ObtenhaGruposDeProdutosPorNome(ByVal Nome As String, _
                                                   ByVal QuantidadeDeRegistros As Integer) As IList(Of IGrupoDeProduto) Implements IServicoDeGrupoDeProduto.ObtenhaGruposDeProdutosPorNome
        Dim Mapeador As IMapeadorDeGrupoDeProduto
        Dim Grupos As IList(Of IGrupoDeProduto)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGrupoDeProduto)()

        Try
            Grupos = Mapeador.ObtenhaGruposDeProdutosPorNome(Nome, QuantidadeDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Grupos
    End Function

End Class
