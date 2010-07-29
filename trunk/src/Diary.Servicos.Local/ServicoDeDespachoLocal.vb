Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeDespachoLocal
    Inherits Servico
    Implements IServicoDeDespacho

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Despacho As IDespacho) Implements IServicoDeDespacho.Inserir
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Despacho)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho) Implements IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        Try
            Return Mapeador.ObtenhaDespachosDaSolicitacao(IDSolicitacao)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal DataInicial As Date, ByVal DataFinal As Date?) As System.Collections.Generic.IList(Of Interfaces.Negocio.IDespacho) Implements Interfaces.Servicos.IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Return Nothing
    End Function

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal Tipo As Interfaces.Negocio.TipoDeDespacho) As System.Collections.Generic.IList(Of Interfaces.Negocio.IDespacho) Implements Interfaces.Servicos.IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Return Nothing
    End Function

End Class