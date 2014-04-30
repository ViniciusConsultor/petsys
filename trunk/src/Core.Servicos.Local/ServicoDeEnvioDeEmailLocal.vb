Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports System.IO
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Net.Mail
Imports Compartilhados.Fabricas
Imports System.Net

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
                Catch ex As Exception
                    Logger.GetInstancia().Erro("Ocorreu um erro ao tentar enviar um e-mail", ex)
                    Throw
                End Try

            End With
        End If
    End Sub
End Class
