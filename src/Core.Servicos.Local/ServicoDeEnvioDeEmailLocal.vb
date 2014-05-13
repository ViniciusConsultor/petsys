Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Net.Mail
Imports Compartilhados.Fabricas
Imports System.Net
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Mapeadores

Public Class ServicoDeEnvioDeEmailLocal
    Inherits Servico
    Implements IServicoDeEnvioDeEmail

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub EnviaEmail(Configuracao As IConfiguracaoDoSistema, _
                          Assunto As String, Remetente As String,
                          DestinatariosEmCopia As IList(Of String),
                          DestinatariosEmCopiaOculta As IList(Of String),
                          Mensagem As String, Anexos As IDictionary(Of String, Stream), Contexto As String,
                          GravaHistorico As Boolean) Implements IServicoDeEnvioDeEmail.EnviaEmail

        If Not Configuracao Is Nothing AndAlso Not Configuracao.ConfiguracaoDeEmailDoSistema Is Nothing Then
            Dim Gerenciador As SmtpClient

            With Configuracao.ConfiguracaoDeEmailDoSistema
                Gerenciador = New SmtpClient(.ServidorDeSaidaDeEmail, .Porta)

                If .RequerAutenticacao Then
                    Gerenciador.Credentials = New NetworkCredential(.UsuarioDeAutenticacaoDoServidorDeSaida, _
                                                                    AjudanteDeCriptografia.Descriptografe(.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida))
                End If

                Gerenciador.EnableSsl = .HabilitarSSL

                Dim MensagemDeEmail As MailMessage = New MailMessage
                MensagemDeEmail.From = New MailAddress(Remetente)

                For Each Destinatario As String In DestinatariosEmCopia
                    MensagemDeEmail.CC.Add(New MailAddress(Destinatario))
                Next

                If Not DestinatariosEmCopiaOculta Is Nothing AndAlso DestinatariosEmCopiaOculta.Count > 0 Then
                    For Each DestinatarioOculto As String In DestinatariosEmCopiaOculta
                        MensagemDeEmail.Bcc.Add(New MailAddress(DestinatarioOculto))
                    Next
                End If

                MensagemDeEmail.IsBodyHtml = True
                MensagemDeEmail.Subject = Assunto
                MensagemDeEmail.Body = Mensagem

                If Not Anexos Is Nothing AndAlso Anexos.Count > 0 Then
                    For Each item As KeyValuePair(Of String, Stream) In Anexos
                        Dim anexo = New Attachment(item.Value, item.Key)

                        MensagemDeEmail.Attachments.Add(anexo)
                    Next
                End If

                Try
                    Gerenciador.Send(MensagemDeEmail)
                    If GravaHistorico Then GraveHistorico(Assunto, Remetente, DestinatariosEmCopia, DestinatariosEmCopiaOculta, Mensagem, Anexos, Contexto)
                Catch ex As Exception
                    Logger.GetInstancia().Erro("Ocorreu um erro ao tentar enviar um e-mail", ex)
                    Throw
                End Try
            End With
        End If
    End Sub

    Private Sub GraveHistorico(Assunto As String, Remetente As String,
                               DestinatariosEmCopia As IList(Of String),
                               DestinatariosEmCopiaOculta As IList(Of String),
                               Mensagem As String, Anexos As IDictionary(Of String, Stream), Contexto As String)

        Dim Historico As IHistoricoDeEmail = ObtenhaHistorico(Assunto, Remetente, DestinatariosEmCopia, DestinatariosEmCopiaOculta, Mensagem, Contexto)
        Dim Mapeador As IMapeadorDeHistoricoDeEmail

        Mapeador = FabricaGenerica.GetInstancia().CrieObjeto(Of IMapeadorDeHistoricoDeEmail)()

        Try
            Mapeador.Grave(Historico)
            Mapeador.GravaAnexos(Historico.ID.Value, Anexos)

        Catch ex As Exception
            Logger.GetInstancia().Erro("Ocorreu um erro ao tentar gravar o histórico de envio de e-mail", ex)
            Throw
        End Try
    End Sub

    Private Function ObtenhaHistorico(Assunto As String, Remetente As String,
                                     DestinatariosEmCopia As IList(Of String),
                                     DestinatariosEmCopiaOculta As IList(Of String),
                                     Mensagem As String, Contexto As String) As IHistoricoDeEmail
        Dim Historico As IHistoricoDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IHistoricoDeEmail)()

        With Historico
            .Assunto = Assunto
            .Contexto = Contexto
            .DestinatariosEmCopia = DestinatariosEmCopia
            .DestinatariosEmCopiaOculta = DestinatariosEmCopiaOculta
            .Mensagem = Mensagem
            .Remetente = Remetente
        End With

        Return Historico
    End Function

End Class
