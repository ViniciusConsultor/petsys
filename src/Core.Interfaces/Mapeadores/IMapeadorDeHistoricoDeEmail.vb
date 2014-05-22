Imports Core.Interfaces.Negocio
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeHistoricoDeEmail

        Sub Grave(Historico As IHistoricoDeEmail, Anexos As IDictionary(Of String, Stream))
        Sub Exclua(IdHistorico As Long)
        Function ObtenhaAnexos(IdHistorico As Long) As IDictionary(Of String, StreamReader)
        Function ObtenhaHistoricos(Filtro As IFiltro, QuantidadeDeItens As Integer, OffSet As Integer) As IList(Of IHistoricoDeEmail)
        Function ObtenhaQuantidadeDeHistoricoDeEmails(Filtro As IFiltro) As Integer
        Function ObtenhaHistorico(IDHistorico As Long) As IHistoricoDeEmail
    End Interface


End Namespace