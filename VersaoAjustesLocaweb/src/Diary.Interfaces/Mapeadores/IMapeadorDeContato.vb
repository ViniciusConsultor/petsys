Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeContato

        Function Obtenha(ByVal Pessoa As IPessoa) As IContato
        Function Obtenha(ByVal ID As Long) As IContato
        Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IContato)
        Sub Inserir(ByVal Contato As IContato)
        Sub Modificar(ByVal Contato As IContato)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace