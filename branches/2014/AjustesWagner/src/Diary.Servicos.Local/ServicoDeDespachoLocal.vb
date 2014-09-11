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

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal DataInicial As Date, ByVal DataFinal As Date?) As IList(Of IDespacho) Implements IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        Try
            Return Mapeador.ObtenhaDespachosDaSolicitacao(IDSolicitacao, DataInicial, DataFinal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal Tipo As TipoDeDespacho) As IList(Of IDespacho) Implements IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        Try
            Return Mapeador.ObtenhaDespachosDaSolicitacao(IDSolicitacao, Tipo)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub RemovaDespachoAssociadoACompromisso(ByVal IDCompromisso As Long) Implements IServicoDeDespacho.RemovaDespachoAssociadoACompromisso
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaDespachoAssociadoACompromisso(IDCompromisso)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub RemovaDespachoAssociadoALembrete(ByVal IDLembrete As Long) Implements IServicoDeDespacho.RemovaDespachoAssociadoALembrete
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaDespachoAssociadoALembrete(IDLembrete)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub RemovaDespachoAssociadoATarefa(ByVal IDTarefa As Long) Implements IServicoDeDespacho.RemovaDespachoAssociadoATarefa
        Dim Mapeador As IMapeadorDeDespacho

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeDespacho)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaDespachoAssociadoATarefa(IDTarefa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class