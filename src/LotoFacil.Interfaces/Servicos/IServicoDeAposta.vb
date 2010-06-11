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
    End Interface

End Namespace