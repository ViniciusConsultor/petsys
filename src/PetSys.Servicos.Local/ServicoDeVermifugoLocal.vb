Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeVermifugoLocal
    Inherits Servico
    Implements IServicoDeVermifugo

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeVermifugo.Excluir
        Dim Mapeador As IMapeadorDeVermifugos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

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

    Public Sub Inserir(ByRef Vermifugo As IVermifugo) Implements IServicoDeVermifugo.Inserir
        Dim Mapeador As IMapeadorDeVermifugos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(Vermifugo)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaVermifugo(ByVal ID As Long) As IVermifugo Implements IServicoDeVermifugo.ObtenhaVermifugo
        Dim Mapeador As IMapeadorDeVermifugos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

        Try
            Return Mapeador.ObtenhaVermifugo(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaVermifugos(ByVal IDs As IList(Of Long)) As IList(Of IVermifugo) Implements IServicoDeVermifugo.ObtenhaVermifugos
        Dim Mapeador As IMapeadorDeVermifugos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

        Try
            Return Mapeador.ObtenhaVermifugos(IDs)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaVermifugosDoAnimal(ByVal IdAnimal As Long) As IList(Of IVermifugo) Implements IServicoDeVermifugo.ObtenhaVermifugosDoAnimal
        Dim Mapeador As IMapeadorDeVermifugos

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeVermifugos)()

        Try
            Return Mapeador.ObtenhaVermifugosDoAnimal(IdAnimal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class