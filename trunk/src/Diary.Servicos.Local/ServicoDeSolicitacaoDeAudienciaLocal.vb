Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Diary.Interfaces.Mapeadores

Public Class ServicoDeSolicitacaoDeAudienciaLocal
    Inherits Servico
    Implements IServicoDeSolicitacaoDeAudiencia

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.Inserir
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(SolicitacaoDeAudiencia)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.Modificar
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(SolicitacaoDeAudiencia)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacaoDeAudiencia
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        Try
            Return Mapeador.ObtenhaSolicitacaoDeAudiencia(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeAudiencia(TrazApenasAtivas)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal TrazApenasAtivas As Boolean, _
                                                   ByVal DataInicio As Date, _
                                                   ByVal DataFim As Date) As IList(Of ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeAudiencia(TrazApenasAtivas, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeSolicitacaoDeAudiencia.Remover
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

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

    Public Sub Finalizar(ByVal ID As Long) Implements IServicoDeSolicitacaoDeAudiencia.Finalizar
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

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

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeAudiencia Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacaoPorCodigo
        Dim Mapeador As IMapeadorDeSolicitacaoDeAudiencia

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeAudiencia)()

        Try
            Return Mapeador.ObtenhaSolicitacaoPorCodigo(Codigo)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class