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
    End Interface

End Namespace