﻿Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces
Imports Telerik.Web.UI
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos

Partial Public Class frmPainelDeControle
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
            MostreConfiguracao()
        End If
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.010"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub ExibaTelaInicial()
        cboTipoDeServidor.Items.Clear()

        For Each Tipo As TipoDeServidorDeEmail In TipoDeServidorDeEmail.ObtenhaTodos
            cboTipoDeServidor.Items.Add(New RadComboBoxItem(Tipo.Descricao, Tipo.ID))
        Next
    End Sub

    Private Sub MostreConfiguracao()
        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Not Configuracao Is Nothing Then
            chkNotificarErrosNaAplicacaoAutomaticamente.Checked = Configuracao.NotificarErrosAutomaticamente
            txtDestinatarioDeNotificacaoDeErro.Text = Configuracao.DestinatarioDaNotificaoDeErros

            If Not Configuracao.ConfiguracaoDeEmailDoSistema Is Nothing Then
                cboTipoDeServidor.SelectedValue = Configuracao.ConfiguracaoDeEmailDoSistema.TipoDoServidor.ID
                txtServidorDeSaidaDeEmail.Text = Configuracao.ConfiguracaoDeEmailDoSistema.ServidorDeSaidaDeEmail
                txtPorta.Text = Configuracao.ConfiguracaoDeEmailDoSistema.Porta.ToString
                chkSSL.Checked = Configuracao.ConfiguracaoDeEmailDoSistema.HabilitarSSL
                chkRequerAutenticacao.Checked = Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao

                If Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao Then
                    txtUsuarioAutenticacaoServidorDeSaida.Text = Configuracao.ConfiguracaoDeEmailDoSistema.UsuarioDeAutenticacaoDoServidorDeSaida
                    txtSenhaUsuarioAutenticacaoServidorDeSaida.Text = Configuracao.ConfiguracaoDeEmailDoSistema.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida
                End If

                txtRemetente.Text = Configuracao.ConfiguracaoDeEmailDoSistema.EmailRemetente
            End If

            Dim ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema

            ConfiguracaoDeAgendaDoSistema = Configuracao.ConfiguracaoDeAgendaDoSistema

            With ConfiguracaoDeAgendaDoSistema
                chkApresentarLinhasCabecalhoAgenda.Checked = .ApresentarLinhasNoCabecalhoDaAgenda
                chkApresentarLinhasRodapeAgenda.Checked = .ApresentarLinhasNoRodapeDaAgenda
                chkApresentarLinhasTextoCompromisso.Checked = .TextoDoCompromissoEntreLinhas
                chkApresentarLinhasTextoLembrete.Checked = .TextoDeLembretesEntreLinhas
                chkApresentarLinhasTextoTarefas.Checked = .TextoDeTarefasEntreLinhas
                txtCabecalhoAgenda.Text = .TextoCabecalhoDaAgenda
                txtCompromissos.Text = .TextoCompromissos
                txtTextoLembretes.Text = .TextoLembretes
                txtTextoTarefas.Text = .TextoTarefas
            End With
        End If

    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
                Call btnSalva_Click()
        End Select
    End Sub

    Private Function ValidaDados() As String
        If chkNotificarErrosNaAplicacaoAutomaticamente.Checked Then
            If String.IsNullOrEmpty(txtDestinatarioDeNotificacaoDeErro.Text) Then Return "O e-mail do destinatário da notificação de erros deve ser informado."
            Return ValidaDadosObrigatoriosDoEmail()
        End If

        If ExisteAlgumCampoDeEmailComValores() Then ValidaDadosObrigatoriosDoEmail()

        If String.IsNullOrEmpty(txtCabecalhoAgenda.Text) Then Return "O texto do cabeçalho da agenda deve ser informado."
        
        Return Nothing
    End Function

    Private Function ValidaDadosObrigatoriosDoEmail() As String
        If String.IsNullOrEmpty(txtServidorDeSaidaDeEmail.Text) Then Return "O servidor de saída de e-mail deve ser informado."
        If txtPorta.Value Is Nothing Then Return "A porta do servidor de saída de e-mail deve ser informada."

        If chkRequerAutenticacao.Checked Then
            If String.IsNullOrEmpty(txtUsuarioAutenticacaoServidorDeSaida.Text) Then Return "O usuário de autenticação do servidor de saída deve ser informado."
            If String.IsNullOrEmpty(txtSenhaUsuarioAutenticacaoServidorDeSaida.Text) Then Return "A senha do usuário de autenticação do servidor de saída deve ser informado."
        End If

        If String.IsNullOrEmpty(txtRemetente.Text) Then Return "O remetente deve ser informado."

        Return Nothing
    End Function

    Private Function ExisteAlgumCampoDeEmailComValores() As Boolean
        Return Not String.IsNullOrEmpty(txtServidorDeSaidaDeEmail.Text) AndAlso _
               Not txtPorta.Value Is Nothing AndAlso _
               Not String.IsNullOrEmpty(txtRemetente.Text) AndAlso _
               chkRequerAutenticacao.Checked
    End Function

    Private Sub btnSalva_Click()
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Dim Configuracao As IConfiguracaoDoSistema = ObtenhaObjetoConfiguracaoDoSistema()

        Try
            Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
                Servico.Salve(Configuracao)
            End Using
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Configurações gerais modificadas com sucesso."), False)
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function ObtenhaObjetoConfiguracaoDoSistema() As IConfiguracaoDoSistema
        Dim Configuracao As IConfiguracaoDoSistema
        Dim ConfiguracaoDeEmail As IConfiguracaoDeEmailDoSistema = Nothing

        Configuracao = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDoSistema)()
        Configuracao.NotificarErrosAutomaticamente = chkNotificarErrosNaAplicacaoAutomaticamente.Checked

        If chkNotificarErrosNaAplicacaoAutomaticamente.Checked Then
            Configuracao.DestinatarioDaNotificaoDeErros = txtDestinatarioDeNotificacaoDeErro.Text
        End If

        If ExisteAlgumCampoDeEmailComValores() Then
            ConfiguracaoDeEmail = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeEmailDoSistema)()

            ConfiguracaoDeEmail.EmailRemetente = txtRemetente.Text
            ConfiguracaoDeEmail.HabilitarSSL = chkSSL.Checked
            ConfiguracaoDeEmail.Porta = CInt(txtPorta.Value)
            ConfiguracaoDeEmail.RequerAutenticacao = chkRequerAutenticacao.Checked

            If chkRequerAutenticacao.Checked Then
                ConfiguracaoDeEmail.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida = AjudanteDeCriptografia.Criptografe(txtSenhaUsuarioAutenticacaoServidorDeSaida.Text)
                ConfiguracaoDeEmail.UsuarioDeAutenticacaoDoServidorDeSaida = txtUsuarioAutenticacaoServidorDeSaida.Text
            End If

            ConfiguracaoDeEmail.ServidorDeSaidaDeEmail = txtServidorDeSaidaDeEmail.Text
            ConfiguracaoDeEmail.TipoDoServidor = TipoDeServidorDeEmail.Obtenha(CChar(cboTipoDeServidor.SelectedValue))
        End If

        Configuracao.ConfiguracaoDeEmailDoSistema = ConfiguracaoDeEmail

        Dim ConfiguracaoDeAgendaDoSistema As IConfiguracaoDeAgendaDoSistema

        ConfiguracaoDeAgendaDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgendaDoSistema)()

        With ConfiguracaoDeAgendaDoSistema
            .ApresentarLinhasNoCabecalhoDaAgenda = chkApresentarLinhasCabecalhoAgenda.Checked
            .ApresentarLinhasNoRodapeDaAgenda = chkApresentarLinhasRodapeAgenda.Checked
            .TextoDoCompromissoEntreLinhas = chkApresentarLinhasTextoCompromisso.Checked
            .TextoDeLembretesEntreLinhas = chkApresentarLinhasTextoLembrete.Checked
            .TextoDeTarefasEntreLinhas = chkApresentarLinhasTextoTarefas.Checked
            .TextoCabecalhoDaAgenda = txtCabecalhoAgenda.Text
            .TextoCompromissos = txtCompromissos.Text
            .TextoLembretes = txtTextoLembretes.Text
            .TextoTarefas = txtTextoTarefas.Text
        End With

        Configuracao.ConfiguracaoDeAgendaDoSistema = ConfiguracaoDeAgendaDoSistema

        Return Configuracao
    End Function

    Private Sub btnTestarConfiguracaoEmail_Click(sender As Object, e As System.EventArgs) Handles btnTestarConfiguracaoEmail.Click
        Dim Configuracao As IConfiguracaoDoSistema

        Using ServicoDeConfiguracao As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = ServicoDeConfiguracao.ObtenhaConfiguracaoDoSistema()
        End Using

        If Not Configuracao Is Nothing Then
            Dim ConfiguracaoDeEmail As IConfiguracaoDeEmailDoSistema

            ConfiguracaoDeEmail = Configuracao.ConfiguracaoDeEmailDoSistema
            If Not Configuracao Is Nothing Then
                Dim destinarios = New List(Of String)

                destinarios.Add(Configuracao.DestinatarioDaNotificaoDeErros)

                GerenciadorDeEmail.EnviaEmail("Teste de envio de e-mail.", _
                                              ConfiguracaoDeEmail.EmailRemetente, _
                                              destinarios, _
                                              Nothing, _
                                              "Teste de envio de e-mail.", _
                                              Nothing)
            End If
        End If
    End Sub


End Class