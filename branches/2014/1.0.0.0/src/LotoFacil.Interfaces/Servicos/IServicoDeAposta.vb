Imports LotoFacil.Interfaces.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeAposta
        Inherits IServico

        Function ConfiraAposta(ByVal Aposta As IAposta, _
                               ByVal DezenasSorteadas As IList(Of IDezena)) As IDictionary(Of IJogo, Short)
        Function ConfiraApostas(ByVal Apostas As IList(Of IAposta), _
                                ByVal DezenasSorteadas As IList(Of IDezena)) As IDictionary(Of IAposta, IDictionary(Of IJogo, Short))
        Function GereJogos(ByVal DezenasEscolhidas As IList(Of IDezena)) As IList(Of IJogo)
        Function ObtenhaTempoGastoParaGerarOsJogos() As String
        Function GraveAposta(ByVal Aposta As IAposta) As String
        Function ObtenhaAposta(ByVal NumeroDoConcurso As Integer) As IAposta
        Function ObtenhaAposta(ByVal ID As Long) As IAposta
        Function ObtenhaApostas(ByVal NomeAposta As String, ByVal QuantidadeDeItens As Integer) As IList(Of IAposta)
    End Interface

End Namespace