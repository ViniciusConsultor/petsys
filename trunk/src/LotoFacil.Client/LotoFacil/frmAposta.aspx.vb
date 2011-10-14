Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports LotoFacil.Interfaces.Negocio
Imports LotoFacil.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados

Partial Public Class frmAposta
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_FRM_APOSTA"
    Private CHAVE_JOGOS_DA_APOSTA As String = "CHAVE_JOGOS_DA_APOSTA"
    Private CHAVE_ID_DA_APOSTA As String = "CHAVE_ID_DA_APOSTA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnConferir"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlAposta, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaAposta, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlAposta, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAposta, Control), False)
        Session(CHAVE_ESTADO) = Estado.Inicial
        Session(CHAVE_JOGOS_DA_APOSTA) = Nothing
    End Sub

    Private Sub ExibaTelaNovo()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaAposta, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnConferir"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Novo
        Session(CHAVE_JOGOS_DA_APOSTA) = Nothing
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnConferir"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlAposta, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnConferir"), RadToolBarButton).Visible = True
    End Sub

    Private Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeAposta = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAposta)()

            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Município excluído com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IAposta
        Dim Aposta As IAposta
        Dim Concurso As IConcurso
        Dim JogosDaAposta As IList(Of IJogo)

        Concurso = FabricaDeConcurso.CrieObjeto(CInt(txtNumeroDoConcurso.Value), txtDataDoConcurso.SelectedDate.Value)
        JogosDaAposta = CType(Session(CHAVE_JOGOS_DA_APOSTA), IList(Of IJogo))

        Aposta = FabricaDeAposta.CrieObjeto(Concurso, JogosDaAposta)
        Aposta.Nome = cboApostas.Text

        Return Aposta
    End Function

    Private Sub ExibaAposta(ByVal Aposta As IAposta)
        cboApostas.Text = Aposta.Nome
        txtDataDoConcurso.SelectedDate = Aposta.Concurso.Data
        txtNumeroDoConcurso.Value = Aposta.Concurso.Numero
        Session(CHAVE_ID_DA_APOSTA) = Aposta.ID
        Session(CHAVE_JOGOS_DA_APOSTA) = Aposta.Jogos
    End Sub

    Private Sub btnSalva_Click()
        Dim Aposta As IAposta

        Try
            ValidaDadosObrigatorios()
        Catch ex As ValidacaoException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
            Exit Sub
        End Try

        Dim Mensagem As String

        Aposta = MontaObjeto()

        Try
            Using Servico As IServicoDeAposta = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAposta)()
                Mensagem = Servico.GraveAposta(Aposta)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub ValidaDadosObrigatorios()
        If String.IsNullOrEmpty(cboApostas.Text) Then
            Throw New ValidacaoException("O nome da aposta deve ser informado.")
        End If

        If String.IsNullOrEmpty(txtNumeroDoConcurso.Text) Then
            Throw New ValidacaoException("O número do concurso deve ser informado.")
        End If

        If Not txtDataDoConcurso.SelectedDate.HasValue Then
            Throw New ValidacaoException("A data do concurso deve ser informada.")
        End If

        If Session(CHAVE_JOGOS_DA_APOSTA) Is Nothing Then
            Throw New ValidacaoException("Não existem jogos criados para esta aposta. Acesse a função Gerar dezenas para que os jogos sejam gerados.")
        End If
    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluir()
    End Sub

    Private Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.LTF.001"
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnExcluir"
                Call btnExclui_Click()
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
            Case "btnSim"
                Call btnSim_Click()
            Case "btnNao"
                Call btnNao_Click()
        End Select
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub btnGerarAposta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGerarAposta.Click
        Dim JogosDaAposta As IList(Of IJogo)
        Dim TempoDeGeracao As String = ""
        Dim Aposta As IAposta = Nothing

        Using Servico As IServicoDeAposta = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAposta)()
            JogosDaAposta = Servico.GereJogos(ObtenhaDezenasEscolhidas)
            TempoDeGeracao = Servico.ObtenhaTempoGastoParaGerarOsJogos
        End Using

        Session(CHAVE_JOGOS_DA_APOSTA) = JogosDaAposta

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(TempoDeGeracao), False)
    End Sub

    Private Function ObtenhaDezenasEscolhidas() As IList(Of IDezena)
        Dim DezenasEscolhidas As IList(Of IDezena)

        DezenasEscolhidas = New List(Of IDezena)

        For Each Item As ListItem In cklDezenas.Items
            If Item.Selected Then
                DezenasEscolhidas.Add(FabricaDeDezena.CrieObjeto(CByte(Item.Value)))
            End If
        Next

        Return DezenasEscolhidas
    End Function

    Private Sub cboApostas_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboApostas.ItemsRequested
        Dim Apostas As IList(Of IAposta)

        Using Servico As IServicoDeAposta = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAposta)()
            Apostas = Servico.ObtenhaApostas(e.Text, 50)
        End Using

        If Not Apostas Is Nothing Then
            For Each Aposta As IAposta In Apostas
                cboApostas.Items.Add(New RadComboBoxItem(Aposta.Nome, Aposta.ID.ToString))
            Next
        End If
    End Sub

    Private Sub cboApostas_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboApostas.SelectedIndexChanged
        Dim Aposta As IAposta
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeAposta = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAposta)()
            Aposta = Servico.ObtenhaAposta(CLng(Valor))
        End Using

        Me.ExibaAposta(Aposta)
        Me.ExibaTelaConsultar()
    End Sub
End Class