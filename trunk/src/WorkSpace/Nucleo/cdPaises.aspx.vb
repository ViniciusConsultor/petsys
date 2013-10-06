Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI

Public Class cdPaises
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_CD_PAIS As String = "CHAVE_ESTADO_CD_PAIS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPais1.PaisFoiSelecionado, AddressOf PaisFoiSelecionado

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
        ctrlPais1.LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoPais, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoPais, Control), False)
        ctrlPais1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_PAIS) = Estado.Inicial
        ctrlPais1.EnableLoadOnDemand = True
        ctrlPais1.ShowDropDownOnTextboxClick = True
        ctrlPais1.AutoPostBack = True
        ctrlPais1.ExibeTituloParaSelecionarUmItem = True
    End Sub

    Private Sub ExibaTelaNovo()
        ctrlPais1.LimparControle()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoPais, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_PAIS) = Estado.Novo
        ctrlPais1.EnableLoadOnDemand = False
        ctrlPais1.ShowDropDownOnTextboxClick = False
        ctrlPais1.AutoPostBack = False
        ctrlPais1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaModificar()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoPais, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_PAIS) = Estado.Modifica
        ctrlPais1.EnableLoadOnDemand = False
        ctrlPais1.ShowDropDownOnTextboxClick = False
        ctrlPais1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_PAIS) = Estado.Remove
        ctrlPais1.HabiliteComponente(False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoPais, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlPais1.ExibeTituloParaSelecionarUmItem = True
    End Sub

    Private Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()
                Servico.Excluir(ctrlPais1.PaisSelecionado.ID.Value)
            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("País excluído com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IPais
        Dim Pais As IPais

        Pais = FabricaGenerica.GetInstancia.CrieObjeto(Of IPais)()

        If CByte(ViewState(CHAVE_ESTADO_CD_PAIS)) <> Estado.Novo Then
            Pais.ID = ctrlPais1.PaisSelecionado.ID
        End If

        Pais.Nome = ctrlPais1.Nome
        Pais.Sigla = txtSigla.Text

        Return Pais
    End Function

    Private Sub ExibaPais(ByVal Pais As IPais)
        ctrlPais1.Nome = Pais.Nome
        txtSigla.Text = Pais.Sigla
    End Sub

    Private Sub btnModificar_Click()
        Me.ExibaTelaModificar()
    End Sub

    Private Sub btnSalva_Click()
        Dim Pais As IPais = Nothing
        Dim Mensagem As String = Nothing

        Pais = MontaObjeto()

        Try
            Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()

                If CByte(ViewState(CHAVE_ESTADO_CD_PAIS)) = Estado.Novo Then
                    Servico.Inserir(Pais)
                    Mensagem = "País cadastrado com sucesso."
                Else
                    Servico.Modificar(Pais)
                    Mensagem = "País modificado com sucesso."
                End If

            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)

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
        Return "FUN.NCL.018"
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

    Private Sub PaisFoiSelecionado(ByVal Pais As IPais)
        ExibaTelaConsultar()
        ExibaPais(Pais)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class