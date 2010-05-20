Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Public Class ServicoDeVeterinarioLocal
    Inherits Servico
    Implements IServicoDeVeterinario

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Veterinario As IVeterinario) Implements IServicoDeVeterinario.Inserir
        Dim Mapeador As IMapeadorDeVeterinario

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVeterinario)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Veterinario)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Veterinario As IVeterinario) Implements IServicoDeVeterinario.Modificar
        Dim Mapeador As IMapeadorDeVeterinario

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVeterinario)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(Veterinario)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IVeterinario Implements IServicoDeVeterinario.Obtenha
        Dim Mapeador As IMapeadorDeVeterinario

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVeterinario)()

        Return Mapeador.Obtenha(Pessoa)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeVeterinario.Remover
        Dim Mapeador As IMapeadorDeVeterinario

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVeterinario)()

        Try
            ServerUtils.BeginTransaction()
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