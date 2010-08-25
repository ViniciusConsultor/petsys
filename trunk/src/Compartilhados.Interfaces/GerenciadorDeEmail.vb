Imports System.Net.Mail
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports System.Net

Public Class GerenciadorDeEmail

    Public Shared Sub EnviaEmail(ByVal Assunto As String, _
                                 ByVal Remetente As String, _
                                 ByVal Destinatario As String, _
                                 ByVal Mensagem As String)
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
                                                                    .SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida)
                End If

                Gerenciador.EnableSsl = .HabilitarSSL

                Dim MensagemDeEmail As MailMessage = New MailMessage
                MensagemDeEmail.From = New MailAddress(Remetente)

                MensagemDeEmail.To.Add(New MailAddress(Destinatario))
                'Caso precise enviar com cópia
                'MensagemDeEmail.To.Add(New MailAddress("email2@provedor.com.br", "Destinatário 2"))

                MensagemDeEmail.Subject = Assunto
                MensagemDeEmail.Body = Mensagem

                Gerenciador.Send(MensagemDeEmail)
            End With
        End If
    End Sub

End Class
