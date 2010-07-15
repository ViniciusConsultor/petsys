Imports Compartilhados
Imports Diary.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeDespacho
        Inherits IServico

        Sub Inserir(ByVal Despacho As IDespacho)
        Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho)

    End Interface

End Namespace