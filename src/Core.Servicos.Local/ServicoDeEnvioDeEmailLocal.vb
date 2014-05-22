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
                    If GravaHistorico Then GraveHistorico(Assunto, Remetente, DestinatariosEmCopia, DestinatariosEmCopiaOculta, Mensagem, Anexos, Contexto, Now)
                Catch ex As Exception
                    Logger.GetInstancia().Erro("Ocorreu um erro ao tentar enviar um e-mail", ex)
                    Throw
                End Try
            End With
        End If
    End Sub

    Public Function ObtenhaHistoricos(ByVal Filtro As IFiltro, ByVal QuantidadeDeItens As Integer, ByVal OffSet As Integer) As IList(Of IHistoricoDeEmail) Implements IServicoDeEnvioDeEmail.ObtenhaHistoricos
        Dim Mapeador As IMapeadorDeHistoricoDeEmail

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia().CrieObjeto(Of IMapeadorDeHistoricoDeEmail)()

        Try
            Return Mapeador.ObtenhaHistoricos(Filtro, QuantidadeDeItens, OffSet)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Private Sub GraveHistorico(Assunto As String, Remetente As String,
                               DestinatariosEmCopia As IList(Of String),
                               DestinatariosEmCopiaOculta As IList(Of String),
                               Mensagem As String, Anexos As IDictionary(Of String, Stream), Contexto As String, Data As Date)

        Dim Historico As IHistoricoDeEmail = ObtenhaHistorico(Assunto, Data, Remetente, DestinatariosEmCopia, DestinatariosEmCopiaOculta, Mensagem, Contexto)
        Dim Mapeador As IMapeadorDeHistoricoDeEmail

        Mapeador = FabricaGenerica.GetInstancia().CrieObjeto(Of IMapeadorDeHistoricoDeEmail)()

        Try
            Mapeador.Grave(Historico, Anexos)
            
        Catch ex As Exception
            Logger.GetInstancia().Erro("Ocorreu um erro ao tentar gravar o histórico de envio de e-mail", ex)
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Function ObtenhaHistorico(Assunto As String, Data As Date, Remetente As String,
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
            .Data = Data
        End With

        Return Historico
    End Function

    Public Function ObtenhaQuantidadeDeHistoricoDeEmails(Filtro As IFiltro) As Integer Implements IServicoDeEnvioDeEmail.ObtenhaQuantidadeDeHistoricoDeEmails
        Dim Mapeador As IMapeadorDeHistoricoDeEmail

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia().CrieObjeto(Of IMapeadorDeHistoricoDeEmail)()

        Try
            Return Mapeador.ObtenhaQuantidadeDeHistoricoDeEmails(Filtro)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub ReenvieEmail(Configuracao As IConfiguracaoDoSistema, IdHistoricoDoEmail As Long) Implements IServicoDeEnvioDeEmail.ReenvieEmail
        'Dim Mapeador As IMapeadorDeHistoricoDeEmail

        'ServerUtils.setCredencial(MyBase._Credencial)
        'Mapeador = FabricaGenerica.GetInstancia().CrieObjeto(Of IMapeadorDeHistoricoDeEmail)()
        'Dim Historico As IHistoricoDeEmail
        'Dim Anexos As IDictionary(Of String, Stream)

        'Try
        '    Historico = Mapeador.
        'Finally
        '    ServerUtils.libereRecursos()
        'End Try

        'EnviaEmail(Configuracao, )
    End Sub

End Class
