Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeFornecedorLocal
    Inherits Servico
    Implements IServicoDeFornecedor

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Cliente As IFornecedor) Implements IServicoDeFornecedor.Inserir
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Cliente)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Cliente As IFornecedor) Implements IServicoDeFornecedor.Modificar
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Cliente)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IFornecedor Implements IServicoDeFornecedor.Obtenha
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

        Try
            Return Mapeador.Obtenha(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function Obtenha(ByVal ID As Long) As IFornecedor Implements IServicoDeFornecedor.Obtenha
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IFornecedor) Implements IServicoDeFornecedor.ObtenhaPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

        Try
            Return Mapeador.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeFornecedor.Remover
        Dim Mapeador As IMapeadorDeFornecedor

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeFornecedor)()

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
