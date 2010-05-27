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

    Public Sub AtualizarProduto(ByVal Produto As IProduto) Implements IServicoDeProduto.AtualizarProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        ValidaRegras(Produto, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.AtualizarProduto(Produto)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub InserirProduto(ByVal Produto As IProduto) Implements IServicoDeProduto.InserirProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        ValidaRegras(Produto, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.InserirProduto(Produto)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaProduto(ByVal ID As Long) As IProduto Implements IServicoDeProduto.ObtenhaProduto
        Dim Mapeador As IMapeadorDeProduto
        Dim Produto As IProduto = Nothing

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()

        Try
            Produto = Mapeador.ObtenhaProduto(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Produto
    End Function

    Public Function ObtenhaProduto(ByVal CodigoDeBarras As String) As IProduto Implements IServicoDeProduto.ObtenhaProduto
        Dim Mapeador As IMapeadorDeProduto
        Dim Produto As IProduto = Nothing

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()

        Try
            Produto = Mapeador.ObtenhaProduto(CodigoDeBarras)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Produto
    End Function

    Public Function ObtenhaProdutos(ByVal Nome As String, ByVal QuantidadeDeRegistros As Integer) As IList(Of IProduto) Implements IServicoDeProduto.ObtenhaProdutos
        Dim Mapeador As IMapeadorDeProduto
        Dim Produtos As IList(Of IProduto)

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()

        Try
            Produtos = Mapeador.ObtenhaProdutos(Nome, QuantidadeDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try

        Return Produtos
    End Function

    Public Sub RemoverProduto(ByVal ID As Long) Implements IServicoDeProduto.RemoverProduto
        Dim Mapeador As IMapeadorDeProduto

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeProduto)()
        'ValidaRegras(Grupo, Mapeador)
        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemoverProduto(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub ValidaRegras(ByVal Produto As IProduto, ByVal Mapeador As IMapeadorDeProduto)

    End Sub

End Class