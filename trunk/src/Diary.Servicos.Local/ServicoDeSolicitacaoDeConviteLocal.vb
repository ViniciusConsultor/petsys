Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeSolicitacaoDeConviteLocal
    Inherits Servico
    Implements IServicoDeSolicitacaoDeConvite

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Finalizar(ByVal ID As Long) Implements IServicoDeSolicitacaoDeConvite.Finalizar
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

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

    Public Sub Inserir(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite) Implements IServicoDeSolicitacaoDeConvite.Inserir
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        SolicitacaoDeConvite.EstaConsistente()
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(SolicitacaoDeConvite)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite) Implements IServicoDeSolicitacaoDeConvite.Modificar
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(SolicitacaoDeConvite)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaSolicitacaoDeConvite(ByVal ID As Long) As ISolicitacaoDeConvite Implements IServicoDeSolicitacaoDeConvite.ObtenhaSolicitacaoDeConvite
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        Try
            Return Mapeador.ObtenhaSolicitacaoDeConvite(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeConvite Implements IServicoDeSolicitacaoDeConvite.ObtenhaSolicitacaoPorCodigo
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        Try
            Return Mapeador.ObtenhaSolicitacaoPorCodigo(Codigo)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeConvite) Implements IServicoDeSolicitacaoDeConvite.ObtenhaSolicitacoesDeConvite
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeConvite(TrazApenasAtivas)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean, _
                                                 ByVal DataInicio As Date, _
                                                 ByVal DataFim As Date) As IList(Of ISolicitacaoDeConvite) Implements IServicoDeSolicitacaoDeConvite.ObtenhaSolicitacoesDeConvite
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

        Try
            Return Mapeador.ObtenhaSolicitacoesDeConvite(TrazApenasAtivas, DataInicio, DataFim)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeSolicitacaoDeConvite.Remover
        Dim Mapeador As IMapeadorDeSolicitacaoDeConvite

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSolicitacaoDeConvite)()

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