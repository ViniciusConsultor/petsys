Imports Core.Interfaces.Negocio
Imports System.IO

Namespace Mapeadores

    Public Interface IMapeadorDeHistoricoDeEmail

        Sub Grave(Historio As IHistoricoDeEmail)
        Sub Exclua(IdHistorico As Long)
        Function ObtenhaAnexos(IdHistorico as long) As IDictionary(Of String, Stream)

    End Interface


End Namespace