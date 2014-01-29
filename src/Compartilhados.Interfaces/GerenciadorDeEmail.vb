Imports System.Net.Mail
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports System.Net
Imports System.IO

Public Class GerenciadorDeEmail

    Public Shared Sub EnviaEmail(ByVal Assunto As String, _
                                 ByVal Remetente As String, _
                                 ByVal DestinatariosEmCopia As IList(Of String), _
                                 ByVal DestinatariosEmCopiaOculta As IList(Of String), _
                                 ByVal Mensagem As String,
                                 ByVal Anexos As IDictionary(Of String, Stream))
        Dim Configuracao As IConfiguracaoDoSistema

        Using ServicoDeConfiguracao As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = ServicoDeConfiguracao.ObtenhaConfiguracaoDoSistema()
        End Using

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

                If Not DestinatariosEmCopiaOculta Is Nothing AndAlso DestinatariosEmCopiaOculta.Count = 0 Then
                    For Each DestinatarioOculto As String In DestinatariosEmCopia
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
