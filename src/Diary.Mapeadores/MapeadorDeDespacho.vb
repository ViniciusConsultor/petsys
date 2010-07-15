Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio

Public Class MapeadorDeDespacho
    Implements IMapeadorDeDespacho

    Public Sub Inserir(ByVal Despacho As IDespacho) Implements IMapeadorDeDespacho.Inserir

    End Sub

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho) Implements IMapeadorDeDespacho.ObtenhaDespachosDaSolicitacao
        Return Nothing
    End Function

End Class