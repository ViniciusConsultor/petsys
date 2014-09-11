Imports Core.Servicos.Local
Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeGrupoRemoting
    Inherits ServicoRemoto
    Implements IServicoDeGrupo

    Private _ServicoLocal As ServicoDeGrupoLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeGrupoLocal(Credencial)
    End Sub

    Public Sub Inserir(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Inserir
        _ServicoLocal.Inserir(Grupo)
    End Sub

    Public Sub Modificar(ByVal Grupo As IGrupo) Implements IServicoDeGrupo.Modificar
        _ServicoLocal.Modificar(Grupo)
    End Sub

    Public Function ObtenhaGrupo(ByVal ID As Long) As IGrupo Implements IServicoDeGrupo.ObtenhaGrupo
        Return _ServicoLocal.ObtenhaGrupo(ID)
    End Function

    Public Function ObtenhaGruposPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IGrupo) Implements IServicoDeGrupo.ObtenhaGruposPorNomeComoFiltro
        Return _ServicoLocal.ObtenhaGruposPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDeGrupo.Remover
        _ServicoLocal.Remover(ID)
    End Sub

End Class
