Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeEnvioDeEmail
        Inherits IServico

        Sub EnviaEmail(Configuracao As IConfiguracaoDoSistema, _
                       ByVal Assunto As String, _
                       ByVal Remetente As String, _
                       ByVal DestinatariosEmCopia As IList(Of String), _
                       ByVal DestinatariosEmCopiaOculta As IList(Of String), _
                       ByVal Mensagem As String,
                       ByVal Anexos As IDictionary(Of String, Stream), _
                       ByVal Contexto As String, _
                       ByVal GravaHistorico As Boolean)
        Function ObtenhaHistoricos(Filtro As IFiltro, QuantidadeDeItens As Integer, OffSet As Integer) As IList(Of IHistoricoDeEmail)
        Function ObtenhaQuantidadeDeHistoricoDeEmails(Filtro As IFiltro) As Integer
        Sub ReenvieEmail(Configuracao As IConfiguracaoDoSistema,
                         IdHistoricoDoEmail As Long)

    End Interface

End Namespace