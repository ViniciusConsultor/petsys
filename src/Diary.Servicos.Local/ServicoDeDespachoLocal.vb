Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio

Public Class ServicoDeDespachoLocal
    Inherits Servico
    Implements IServicoDeDespacho

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Despacho As IDespacho) Implements IServicoDeDespacho.Inserir

    End Sub

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho) Implements IServicoDeDespacho.ObtenhaDespachosDaSolicitacao
        Return Nothing
    End Function

End Class