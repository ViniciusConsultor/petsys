Imports Core.Interfaces.Negocio
Imports System.IO

Namespace Mapeadores

    Public Interface IMapeadorDeHistoricoDeEmail

        Sub Grave(Historico As IHistoricoDeEmail)
        Sub GravaAnexos(IdHistorico As Long, Anexos As IDictionary(Of String, Stream))
        Sub Exclua(IdHistorico As Long)
        Function ObtenhaAnexos(IdHistorico As Long) As IDictionary(Of String, Stream)

    End Interface


End Namespace