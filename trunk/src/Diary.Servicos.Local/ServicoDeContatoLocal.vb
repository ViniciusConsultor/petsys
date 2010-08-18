Imports Diary.Interfaces.Servicos
Imports Compartilhados
Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Imports Diary.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos

Public Class ServicoDeContatoLocal
    Inherits Servico
    Implements IServicoDeContato

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Contato As IContato) Implements IServicoDeContato.Inserir
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Contato)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Contato As IContato) Implements IServicoDeContato.Modificar
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Contato)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal Pessoa As IPessoa) As IContato Implements IServicoDeContato.Obtenha
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        Try
            Return Mapeador.Obtenha(Pessoa)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IContato) Implements IServicoDeContato.ObtenhaPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        Try
            Return Mapeador.ObtenhaPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeContato.Remover
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remover(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw New BussinesException("O contato não pode ser removido pois o mesmo está sendo utilizado em outras funcionalidades do sistema.")
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function Obtenha(ByVal ID As Long) As Interfaces.Negocio.IContato Implements Interfaces.Servicos.IServicoDeContato.Obtenha
        Dim Mapeador As IMapeadorDeContato

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeContato)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class