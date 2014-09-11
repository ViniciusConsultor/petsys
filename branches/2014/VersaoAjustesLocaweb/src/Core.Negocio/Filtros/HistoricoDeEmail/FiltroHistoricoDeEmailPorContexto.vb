Imports Core.Interfaces.Negocio.Filtros.HistoricoDeEmail
Imports System.Text

Namespace Filtros.HistoricoDeEmail
    <Serializable()> _
    Public Class FiltroHistoricoDeEmailPorContexto
        Inherits Filtro
        Implements IFiltroHistoricoDeEmailPorContexto

        Public Overrides Function ObtenhaQuery() As String
            Dim Sql As StringBuilder = New StringBuilder()

            Sql.Append("SELECT ID, DATA, ASSUNTO, REMETENTE, MENSAGEM, DESTINATARIOSCC, DESTINATARIOSCCO, CONTEXTO, POSSUIANEXO ")
            Sql.Append("FROM NCL_HISTORICOEMAIL ")
            Sql.AppendLine(" WHERE " & ObtenhaFiltroMontado("CONTEXTO", True))
            Return Sql.ToString()
        End Function

    End Class
End Namespace