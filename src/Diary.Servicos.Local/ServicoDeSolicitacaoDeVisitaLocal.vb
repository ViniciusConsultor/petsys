Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeSolicitacaoDeVisitaLocal
    Inherits Servico
    Implements IServicoDeSolicitacaoDeVisita

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita) Implements IServicoDeSolicitacaoDeVisita.Inserir
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(SolicitacaoDeVisita)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita) Implements IServicoDeSolicitacaoDeVisita.Modificar
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(SolicitacaoDeVisita)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaSolicitacaoDeVisita(ByVal ID As Long) As ISolicitacaoDeVisita Implements IServicoDeSolicitacaoDeVisita.ObtenhaSolicitacaoDeVisita
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        Try
            Return Mapeador.ObtenhaSolicitacaoDeVisita(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeVisita) Implements IServicoDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeVisita(ConsiderarSolicitacoesFinalizadas)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                   ByVal DataInicio As Date, _
                                                   ByVal DataFim As Date) As IList(Of ISolicitacaoDeVisita) Implements IServicoDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeVisita(ConsiderarSolicitacoesFinalizadas, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeSolicitacaoDeVisita.Remover
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

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

    Public Sub Finalizar(ByVal ID As Long) Implements IServicoDeSolicitacaoDeVisita.Finalizar
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Finalizar(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeVisita Implements IServicoDeSolicitacaoDeVisita.ObtenhaSolicitacaoPorCodigo
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        Try
            Return Mapeador.ObtenhaSolicitacaoPorCodigo(Codigo)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                ByVal IDContato As Long) As IList(Of ISolicitacaoDeVisita) Implements IServicoDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Mapeador As IMapeadorDeSolicitacaoDeVisita

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeVisita)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeVisita(ConsiderarSolicitacoesFinalizadas, IDContato)
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Function

End Class
