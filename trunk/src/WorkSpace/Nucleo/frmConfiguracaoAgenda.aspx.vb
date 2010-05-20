Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados
Imports Core.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio

Partial Public Class frmConfiguracaoAgenda
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_FRM_CONFIGURACAO_AGENDA"
    Private CHAVE_ID_CONFIGURACAO As String = "CHAVE_ID_CONFIGURACAO_AGENDA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlConfiguracao, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosDaConfiguracao, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlConfiguracao, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaConfiguracao, Control), False)
        Session(CHAVE_ESTADO) = Estado.Inicial
        Session(CHAVE_ID_CONFIGURACAO) = Nothing
        cboNome.ShowDropDownOnTextboxClick = True
        cboNome.AutoPostBack = True
        CarregaDiasDaSemana()
    End Sub

    Private Sub CarregaDiasDaSemana()
        cboPrimeiroDiaDaSemana.Items.Clear()

        For Each DiaDaSemana As DiaDaSemana In DiaDaSemana.ObtenhaTodos
            cboPrimeiroDiaDaSemana.Items.Add(New RadComboBoxItem(DiaDaSemana.Descricao, DiaDaSemana.IDDoDia.ToString))
        Next
    End Sub

    Private Sub ExibaTelaNovo()
        UtilidadesWeb.LimparComponente(CType(pnlConfiguracao, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaConfiguracao, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Novo
        cboNome.ShowDropDownOnTextboxClick = False
        cboNome.AutoPostBack = False
    End Sub

    Private Sub ExibaTelaModificar()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaConfiguracao, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Modifica
        cboNome.ShowDropDownOnTextboxClick = False
        cboNome.AutoPostBack = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        Session(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlConfiguracao, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDaConfiguracao, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
    End Sub

    Private Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracaoDeAgenda)()
                Servico.Excluir(CLng(Session(CHAVE_ID_CONFIGURACAO)))
            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Configuração de agenda excluída com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IConfiguracaoDeAgenda
        Dim Configuracao As IConfiguracaoDeAgenda

        Configuracao = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgenda)()

        If CByte(Session(CHAVE_ESTADO)) <> Estado.Novo Then
            Configuracao.ID = CLng(Session(CHAVE_ID_CONFIGURACAO))
        End If

        Configuracao.Nome = cboNome.Text
        Configuracao.HoraDeInicio = txtHorarioDeInicio.SelectedDate.Value
        Configuracao.HoraDeTermino = txtHorarioDeTermino.SelectedDate.Value
        Configuracao.IntervaloEntreHorarios = txtIntervaloEntreHorarios.SelectedDate.Value
        Configuracao.PrimeiroDiaDaSemana = DiaDaSemana.Obtenha(CShort(cboPrimeiroDiaDaSemana.SelectedValue))

        Return Configuracao
    End Function

    Private Sub ExibaConfiguracao(ByVal Configuracao As IConfiguracaoDeAgenda)
        Session(CHAVE_ID_CONFIGURACAO) = Configuracao.ID
        cboNome.Text = Configuracao.Nome
        txtHorarioDeInicio.SelectedDate = Configuracao.HoraDeInicio
        txtHorarioDeTermino.SelectedDate = Configuracao.HoraDeTermino
        txtIntervaloEntreHorarios.SelectedDate = Configuracao.IntervaloEntreHorarios
        cboPrimeiroDiaDaSemana.SelectedValue = Configuracao.PrimeiroDiaDaSemana.IDDoDia.ToString
    End Sub

    Private Sub btnModificar_Click()
        Me.ExibaTelaModificar()
    End Sub

    Private Sub btnSalva_Click()
        Dim Configuracao As IConfiguracaoDeAgenda = Nothing

        Configuracao = MontaObjeto()

        Try
            Using Servico As IServicoDeConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracaoDeAgenda)()

                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(Configuracao)
                Else
                    Servico.Modificar(Configuracao)
                End If

            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Configuração de agenda cadastrada com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try

    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluir()
    End Sub

    Private Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.009"
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnModificar"
                Call btnModificar_Click()
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

    Private Sub cboNome_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboNome.ItemsRequested
        Dim Configuracoes As IList(Of IConfiguracaoDeAgenda)

        Using Servico As IServicoDeConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracaoDeAgenda)()
            Configuracoes = Servico.ObtenhaConfiguracoesPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Configuracoes Is Nothing Then
            For Each Configuracao As IConfiguracaoDeAgenda In Configuracoes
                Dim Item As New RadComboBoxItem(Configuracao.Nome, Configuracao.ID.ToString)

                cboNome.Items.Add(Item)
            Next
        End If

    End Sub

    Private Sub cboNome_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboNome.SelectedIndexChanged
        Dim Configuracao As IConfiguracaoDeAgenda
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeConfiguracaoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracaoDeAgenda)()
            Configuracao = Servico.ObtenhaConfiguracao(CLng(Valor))
        End Using

        Me.ExibaTelaConsultar()
        Me.ExibaConfiguracao(Configuracao)
    End Sub

End Class