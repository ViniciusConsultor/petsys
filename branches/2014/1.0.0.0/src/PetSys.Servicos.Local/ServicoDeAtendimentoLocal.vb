Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeAtendimentoLocal
    Inherits Servico
    Implements IServicoDeAtendimento

    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Insira(ByVal Atendimento As IAtendimento) Implements IServicoDeAtendimento.Insira
        Dim Mapeador As IMapeadorDeAtendimento

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimento)()

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

    Public Sub Modificar(ByVal Atendimento As IAtendimento) Implements IServicoDeAtendimento.Modificar
        Dim Mapeador As IMapeadorDeAtendimento

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimento)()

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

    Public Function ObtenhaAtendimento(ByVal ID As Long) As IAtendimento Implements IServicoDeAtendimento.ObtenhaAtendimento
        Dim Mapeador As IMapeadorDeAtendimento

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimento)()

        Try
            Return Mapeador.ObtenhaAtendimento(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaAtendimentos(ByVal Animal As IAnimal) As IList(Of IAtendimento) Implements IServicoDeAtendimento.ObtenhaAtendimentos
        Dim Mapeador As IMapeadorDeAtendimento

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimento)()

        Try
            Return Mapeador.ObtenhaAtendimentos(Animal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeAtendimento.Excluir
        Dim Mapeador As IMapeadorDeAtendimento

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAtendimento)()

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
End Class
