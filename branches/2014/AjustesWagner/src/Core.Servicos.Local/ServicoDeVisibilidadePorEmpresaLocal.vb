Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeVisibilidadePorEmpresaLocal
    Inherits Servico
    Implements IServicoDeVisibilidadePorEmpresa

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa)) Implements IServicoDeVisibilidadePorEmpresa.Inserir
        Dim Mapeador As IMapeadorDeVisibilidadePorEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVisibilidadePorEmpresa)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(IDOperador, EmpresasVisiveis)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modifique(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa)) Implements IServicoDeVisibilidadePorEmpresa.Modifique
        Dim Mapeador As IMapeadorDeVisibilidadePorEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVisibilidadePorEmpresa)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modifique(IDOperador, EmpresasVisiveis)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(IDOperador As Long) As IList(Of IEmpresa) Implements IServicoDeVisibilidadePorEmpresa.Obtenha
        Dim Mapeador As IMapeadorDeVisibilidadePorEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)

        Try
            Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVisibilidadePorEmpresa)()
            Return Mapeador.Obtenha(IDOperador)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remova(IDOperador As Long) Implements IServicoDeVisibilidadePorEmpresa.Remova
        Dim Mapeador As IMapeadorDeVisibilidadePorEmpresa

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVisibilidadePorEmpresa)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remova(IDOperador)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class