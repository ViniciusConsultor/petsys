Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Servicos.Local
Imports Core.Interfaces.Negocio

Public Class ServicoDeAutorizacaoRemoting
    Inherits ServicoRemoto
    Implements IServicoDeAutorizacao

    Private _ServicoLocal As ServicoDeAutorizacaoLocal

    Public Sub Modifique(ByVal IDGrupo As Long, ByVal Diretivas As IList(Of IDiretivaDeSeguranca)) Implements IServicoDeAutorizacao.Modifique
        _ServicoLocal.Modifique(IDGrupo, Diretivas)
    End Sub

    Public Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca) Implements IServicoDeAutorizacao.ObtenhaDiretivasDeSegurancaDoGrupo
        Return _ServicoLocal.ObtenhaDiretivasDeSegurancaDoGrupo(ID)
    End Function

    Public Function ObtenhaModulosDisponiveis() As IList(Of IModulo) Implements IServicoDeAutorizacao.ObtenhaModulosDisponiveis
        Return _ServicoLocal.ObtenhaModulosDisponiveis
    End Function

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeAutorizacaoLocal(Credencial)
    End Sub

End Class