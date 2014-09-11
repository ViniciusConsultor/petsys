Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Core.Interfaces

Public Class ServicoDePaisLocal
    Inherits Servico
    Implements IServicoDePais

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal Id As Long) Implements IServicoDePais.Excluir
        Dim Mapeador As IMapeadorDePais

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePais)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Excluir(Id)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Inserir(ByVal Pais As IPais) Implements IServicoDePais.Inserir
        Dim Mapeador As IMapeadorDePais

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePais)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Pais)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Pais As IPais) Implements IServicoDePais.Modificar
        Dim Mapeador As IMapeadorDePais

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePais)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Modificar(Pais)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaPais(ByVal Id As Long) As IPais Implements IServicoDePais.ObtenhaPais
        Dim Mapeador As IMapeadorDePais

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePais)()

        Try
            Return Mapeador.ObtenhaPais(Id)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPaisesPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPais) Implements IServicoDePais.ObtenhaPaisesPorNomeComoFiltro
        Dim Mapeador As IMapeadorDePais

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePais)()

        Try
            Return Mapeador.ObtenhaPaisesPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class
