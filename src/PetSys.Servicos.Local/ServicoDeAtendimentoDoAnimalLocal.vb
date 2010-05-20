Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeAtendimentoDoAnimalLocal
    Inherits Servico
    Implements IServicoDeAtendimentoDoAnimal

    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Insira(ByVal Atendimento As IAtendimentoDoAnimal) Implements IServicoDeAtendimentoDoAnimal.Insira
        Dim Mapeador As IMapeadorDeAtendimentoDoAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimentoDoAnimal)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Insira(Atendimento)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Atendimento As IAtendimentoDoAnimal) Implements IServicoDeAtendimentoDoAnimal.Modificar
        Dim Mapeador As IMapeadorDeAtendimentoDoAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimentoDoAnimal)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(Atendimento)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimentoDoAnimal Implements IServicoDeAtendimentoDoAnimal.ObtenhaAtendimento
        Dim Mapeador As IMapeadorDeAtendimentoDoAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimentoDoAnimal)()

        Try
            Return Mapeador.ObtenhaAtendimento(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimentoDoAnimal) Implements IServicoDeAtendimentoDoAnimal.ObtenhaAtendimentos
        Dim Mapeador As IMapeadorDeAtendimentoDoAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimentoDoAnimal)()

        Try
            Return Mapeador.ObtenhaAtendimentos(Animal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class