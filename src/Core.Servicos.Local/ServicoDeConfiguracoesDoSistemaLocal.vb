Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos

Public Class ServicoDeConfiguracoesDoSistemaLocal
    Inherits Servico
    Implements IServicoDeConfiguracoesDoSistema

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaConfiguracaoDoSistema() As IConfiguracaoDoSistema Implements IServicoDeConfiguracoesDoSistema.ObtenhaConfiguracaoDoSistema
        Dim Mapeador As IMapeadorDeConfiguracoesDoSistema

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracoesDoSistema)()

        Return Mapeador.ObtenhaConfiguracaoDoSistema()
    End Function

    Public Sub Salve(ByVal Configuracao As IConfiguracaoDoSistema) Implements IServicoDeConfiguracoesDoSistema.Salve
        Dim Mapeador As IMapeadorDeConfiguracoesDoSistema

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeConfiguracoesDoSistema)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Salve(Configuracao)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class