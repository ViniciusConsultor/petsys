Imports T13.Interfaces.Servicos
Imports T13.Interfaces.Mapeadores
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports T13.Interfaces.Negocio

Public Class ServicoDeServicoPrestadoLocal
    Inherits Servico
    Implements IServicoDeServicoPrestado

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeServicoPrestado.Excluir
        Dim Mapeador As IMapeadorDeServicosPrestados

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Excluir(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Inserir(ByVal Servico As IServicoPrestado) Implements IServicoDeServicoPrestado.Inserir
        Dim Mapeador As IMapeadorDeServicosPrestados

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Servico)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Sevico As IServicoPrestado) Implements IServicoDeServicoPrestado.Modificar
        Dim Mapeador As IMapeadorDeServicosPrestados

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(Sevico)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaServico(ByVal ID As Long) As IServicoPrestado Implements IServicoDeServicoPrestado.ObtenhaServico
        Dim Mapeador As IMapeadorDeServicosPrestados

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

        Try

            Return Mapeador.ObtenhaServico(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaServicoPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IServicoPrestado) Implements IServicoDeServicoPrestado.ObtenhaServicoPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeServicosPrestados

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeServicosPrestados)()

        Try

            Return Mapeador.ObtenhaServicoPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class
