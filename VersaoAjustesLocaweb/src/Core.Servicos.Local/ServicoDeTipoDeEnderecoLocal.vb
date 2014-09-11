Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeTipoDeEnderecoLocal
    Inherits Servico
    Implements IServicoDeTipoDeEndereco

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Insira(ByVal TipoDeEndereco As ITipoDeEndereco) Implements IServicoDeTipoDeEndereco.Insira
        Dim Mapeador As IMapeadorDeTipoDeEndereco

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeTipoDeEndereco)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Insira(TipoDeEndereco)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal TipoDeEndereco As ITipoDeEndereco) Implements IServicoDeTipoDeEndereco.Modificar
        Dim Mapeador As IMapeadorDeTipoDeEndereco

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeTipoDeEndereco)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(TipoDeEndereco)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal ID As Long) As ITipoDeEndereco Implements IServicoDeTipoDeEndereco.Obtenha
        Dim Mapeador As IMapeadorDeTipoDeEndereco

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeTipoDeEndereco)()


        Try
            Return Mapeador.Obtenha(ID)

        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPorNome(ByVal Filtro As String, ByVal Quantidade As Integer) As IList(Of ITipoDeEndereco) Implements IServicoDeTipoDeEndereco.ObtenhaPorNome
        Dim Mapeador As IMapeadorDeTipoDeEndereco

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeTipoDeEndereco)()


        Try
            Return Mapeador.ObtenhaPorNome(Filtro, Quantidade)

        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeTipoDeEndereco.Remover
        Dim Mapeador As IMapeadorDeTipoDeEndereco

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeTipoDeEndereco)()

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