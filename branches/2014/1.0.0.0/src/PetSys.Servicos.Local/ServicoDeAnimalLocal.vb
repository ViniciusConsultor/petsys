Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeAnimalLocal
    Inherits Servico
    Implements IServicoDeAnimal

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Animal As IAnimal) Implements IServicoDeAnimal.Inserir
        Dim Mapeador As IMapeadorDeAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAnimal)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Animal)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Animal As IAnimal) Implements IServicoDeAnimal.Modificar
        Dim Mapeador As IMapeadorDeAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAnimal)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(Animal)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaAnimaisPorNomeComoFiltro(ByVal Nome As String, _
                                                    ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IAnimal) Implements IServicoDeAnimal.ObtenhaAnimaisPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAnimal)()

        Try
            Return Mapeador.ObtenhaAnimaisPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaAnimal(ByVal ID As Long) As IAnimal Implements IServicoDeAnimal.ObtenhaAnimal
        Dim Mapeador As IMapeadorDeAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAnimal)()

        Try
            Return Mapeador.ObtenhaAnimal(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeAnimal.Excluir
        Dim Mapeador As IMapeadorDeAnimal

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeAnimal)()

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