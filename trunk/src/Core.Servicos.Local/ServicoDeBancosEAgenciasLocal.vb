Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class ServicoDeBancosEAgenciasLocal
    Inherits Servico
    Implements IServicoDeBancosEAgencias

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub InsiraAgencia(ByVal Agencia As IAgencia) Implements IServicoDeBancosEAgencias.InsiraAgencia
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.InsiraAgencia(Agencia)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub InsiraBanco(ByVal Banco As IBanco) Implements IServicoDeBancosEAgencias.InsiraBanco
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.InsiraBanco(Banco)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub ModifiqueAgencia(ByVal Agencia As IAgencia) Implements IServicoDeBancosEAgencias.ModifiqueAgencia
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueAgencia(Agencia)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub ModifiqueBanco(ByVal Banco As IBanco) Implements IServicoDeBancosEAgencias.ModifiqueBanco
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.ModifiqueBanco(Banco)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaAgencias(ByVal IDBanco As Long) As IList(Of IAgencia) Implements IServicoDeBancosEAgencias.ObtenhaAgencias
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        Try
            Return Mapeador.ObtenhaAgencias(IDBanco)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

   Public Sub RemovaAgencia(ByVal ID As Long) Implements IServicoDeBancosEAgencias.RemovaAgencia
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaAgencia(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub RemovaBanco(ByVal ID As Long) Implements IServicoDeBancosEAgencias.RemovaBanco
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.RemovaBanco(ID)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaAgencia(ByVal IDBanco As Long, ByVal IDAgencia As Long) As IAgencia Implements IServicoDeBancosEAgencias.ObtenhaAgencia
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        Try
            Return Mapeador.ObtenhaAgencia(IDBanco, IDAgencia)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaBancosPorNomeComoFiltro(ByVal Nome As String, _
                                                   ByVal Quantidade As Integer) As IList(Of IBanco) Implements IServicoDeBancosEAgencias.ObtenhaBancosPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        Try
            Return Mapeador.ObtenhaBancosPorNomeComoFiltro(Nome, Quantidade)
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Function

    Public Function ObtenhaBanco(ByVal ID As Long) As IBanco Implements IServicoDeBancosEAgencias.ObtenhaBanco
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        Try
            Return Mapeador.ObtenhaBanco(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaAgenciasPorNomeComoFiltro(ByVal Banco As IBanco, _
                                                     ByVal NomeDaAgencia As String, _
                                                     ByVal Quantidade As Integer) As IList(Of IAgencia) Implements IServicoDeBancosEAgencias.ObtenhaAgenciasPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeBancosEAgencias

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeBancosEAgencias)()

        Try
            Return Mapeador.ObtenhaAgenciasPorNomeComoFiltro(Banco, NomeDaAgencia, Quantidade)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class