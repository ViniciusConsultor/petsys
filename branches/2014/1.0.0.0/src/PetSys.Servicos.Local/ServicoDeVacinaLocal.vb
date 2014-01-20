Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeVacinaLocal
    Inherits Servico
    Implements IServicoDeVacina

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeVacina.Excluir
        Dim Mapeador As IMapeadorDeVacina

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

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

    Public Sub Inserir(ByVal Vacina As IVacina) Implements IServicoDeVacina.Inserir
        Dim Mapeador As IMapeadorDeVacina

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Vacina)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaVacina(ByVal ID As Long) As IVacina Implements IServicoDeVacina.ObtenhaVacina
        Dim Mapeador As IMapeadorDeVacina

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

        Try
            Return Mapeador.ObtenhaVacina(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaVacinas(ByVal IDs As IList(Of Long)) As IList(Of IVacina) Implements IServicoDeVacina.ObtenhaVacinas
        Dim Mapeador As IMapeadorDeVacina

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

        Try
            Return Mapeador.ObtenhaVacinas(IDs)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaVacinasDoAnimal(ByVal IdAnimal As Long) As IList(Of IVacina) Implements IServicoDeVacina.ObtenhaVacinasDoAnimal
        Dim Mapeador As IMapeadorDeVacina

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVacina)()

        Try
            Return Mapeador.ObtenhaVacinasDoAnimal(IdAnimal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class
