Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores
Imports Compartilhados
Imports Compartilhados.Interfaces

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

End Class