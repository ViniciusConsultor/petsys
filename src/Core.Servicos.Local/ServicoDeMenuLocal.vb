Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores
Imports Compartilhados

Public Class ServicoDeMenuLocal
    Inherits Servico
    Implements IServicoDeMenu

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaMenu() As IMenuComposto Implements IServicoDeMenu.ObtenhaMenu
        Dim Mapeador As IMapeadorDeMenu

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMenu)()

        Try
            Return Mapeador.ObtenhaMenu()
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaFuncoesComCaminhoDoMenu(ByVal NomeDaFuncao As String) As HashSet(Of DTOAjudanteDePesquisaDeMenu) Implements IServicoDeMenu.ObtenhaFuncoesComCaminhoDoMenu
        Dim Mapeador As IMapeadorDeMenu

        ServerUtils.setCredencial(_Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMenu)()

        Try
            Return Mapeador.ObtenhaFuncoesComCaminhoDoMenu(NomeDaFuncao)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class