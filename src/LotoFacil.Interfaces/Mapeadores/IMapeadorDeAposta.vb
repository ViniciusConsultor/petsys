Imports LotoFacil.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeAposta

        Sub GraveAposta(ByVal Aposta As IAposta)
        Function ObtenhaAposta(ByVal NumeroDoConcurso As Integer) As IAposta
        Function ObtenhaAposta(ByVal ID As Long) As IAposta
        Function ObtenhaApostas(ByVal NomeAposta As String, ByVal QuantidadeDeItens As Integer) As IList(Of IAposta)

    End Interface

End Namespace