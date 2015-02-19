﻿Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Servicos.Local
Imports Core.Interfaces.Negocio

Public Class ServicoDeMenuRemoting
    Inherits ServicoRemoto
    Implements IServicoDeMenu

    Private _ServicoLocal As ServicoDeMenuLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeMenuLocal(Credencial)
    End Sub

    Public Function ObtenhaMenu() As IMenuComposto Implements IServicoDeMenu.ObtenhaMenu
        Return _ServicoLocal.ObtenhaMenu
    End Function

    Public Function ObtenhaFuncoesComCaminhoDoMenu(ByVal NomeDaFuncao As String) As HashSet(Of DTOAjudanteDePesquisaDeMenu) Implements IServicoDeMenu.ObtenhaFuncoesComCaminhoDoMenu
        Return _ServicoLocal.ObtenhaFuncoesComCaminhoDoMenu(NomeDaFuncao)
    End Function
End Class