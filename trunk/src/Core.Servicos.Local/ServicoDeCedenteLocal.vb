Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeCedenteLocal
    Inherits Servico
    Implements IServicoDeCedente
    
    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(Cedente As ICedente) Implements IServicoDeCedente.Inserir
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Cedente)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(Cedente As ICedente) Implements IServicoDeCedente.Modificar
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Cedente)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(Pessoa As IPessoa) As ICedente Implements IServicoDeCedente.Obtenha
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

        Try
            Return Mapeador.Obtenha(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function Obtenha(ID As Long) As ICedente Implements IServicoDeCedente.Obtenha
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPorNomeComoFiltro(Nome As String, QuantidadeMaximaDeRegistros As Integer) As IList(Of ICedente) Implements IServicoDeCedente.ObtenhaPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

        Try
            Return Mapeador.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ID As Long) Implements IServicoDeCedente.Remover
        Dim Mapeador As IMapeadorDeCedente

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeCedente)()

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
