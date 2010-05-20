Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores

Public Class ServicoDeConfiguracaoDeAgendaLocal
    Inherits Servico
    Implements IServicoDeConfiguracaoDeAgenda

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Excluir(ByVal ID As Long) Implements IServicoDeConfiguracaoDeAgenda.Excluir
        Dim Mapeador As IMapeadorDeConfiguracaoDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracaoDeAgenda)()

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

    Public Sub Inserir(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.Inserir
        Dim Mapeador As IMapeadorDeConfiguracaoDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracaoDeAgenda)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Inserir(ConfiguracaoDeAgenda)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.Modificar
        Dim Mapeador As IMapeadorDeConfiguracaoDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracaoDeAgenda)()

        Try
            ServerUtils.BeginTransaction()
            Mapeador.Modificar(ConfiguracaoDeAgenda)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaConfiguracao(ByVal ID As Long) As IConfiguracaoDeAgenda Implements IServicoDeConfiguracaoDeAgenda.ObtenhaConfiguracao
        Dim Mapeador As IMapeadorDeConfiguracaoDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracaoDeAgenda)()

        Try
            Return Mapeador.ObtenhaConfiguracao(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaConfiguracoesPorNomeComoFiltro(ByVal Nome As String, _
                                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda) Implements IServicoDeConfiguracaoDeAgenda.ObtenhaConfiguracoesPorNomeComoFiltro
        Dim Mapeador As IMapeadorDeConfiguracaoDeAgenda

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracaoDeAgenda)()

        Try
            Return Mapeador.ObtenhaConfiguracoesPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class
