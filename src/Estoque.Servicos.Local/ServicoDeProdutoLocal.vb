Imports Compartilhados
Imports Estoque.Interfaces.Servicos
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeProdutoLocal
    Inherits Servico
    Implements IServicoDeProduto

    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub AtualizarMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IServicoDeProduto.AtualizarMarcaDeProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.AtualizarMarcaDeProduto(Marca)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub InserirMarcaDeProduto(ByVal Marca As IMarcaDeProduto) Implements IServicoDeProduto.InserirMarcaDeProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.InserirMarcaDeProduto(Marca)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaMarcaDeProduto(ByVal ID As Long) As IMarcaDeProduto Implements IServicoDeProduto.ObtenhaMarcaDeProduto
        Dim Mapeador As IMapeadorDeProduto
        Dim Marca As IMarcaDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()

        Try
            Marca = Mapeador.ObtenhaMarcaDeProduto(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Marca
    End Function

    Public Function ObtenhaMarcasDeProdutosPorNome(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IMarcaDeProduto) Implements IServicoDeProduto.ObtenhaMarcasDeProdutosPorNome
        Dim Mapeador As IMapeadorDeProduto
        Dim Marcas As IList(Of IMarcaDeProduto)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()

        Try
            Marcas = Mapeador.ObtenhaMarcasDeProdutosPorNome(Nome, QuantidadeDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Marcas
    End Function

    Public Sub RemoverMarcaDeProduto(ByVal ID As Long) Implements IServicoDeProduto.RemoverMarcaDeProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemoverMarcaDeProduto(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class
