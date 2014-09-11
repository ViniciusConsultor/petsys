Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Core.Interfaces.Servicos

Partial Public Class cdMunicipio
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO_CD_MUNICIPIO As String = "CHAVE_ESTADO_CD_MUNICIPIO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlMunicipios1.MunicipioFoiSelecionado, AddressOf MunicipioFoiSelecionado

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
        ctrlMunicipios1.LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoMunicipio, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoMunicipio, Control), False)
        ctrlMunicipios1.HabiliteComponente(True)
        ViewState(CHAVE_ESTADO_CD_MUNICIPIO) = Estado.Inicial
        PreecheUFs()
        ctrlMunicipios1.EnableLoadOnDemand = True
        ctrlMunicipios1.ShowDropDownOnTextboxClick = True
        ctrlMunicipios1.AutoPostBack = True
        ctrlMunicipios1.ExibeTituloParaSelecionarUmItem = True
    End Sub

    Private Sub ExibaTelaNovo()
        ctrlMunicipios1.LimparControle()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoMunicipio, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_MUNICIPIO) = Estado.Novo
        PreecheUFs()
        ctrlMunicipios1.EnableLoadOnDemand = False
        ctrlMunicipios1.ShowDropDownOnTextboxClick = False
        ctrlMunicipios1.AutoPostBack = False
        ctrlMunicipios1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaModificar()
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoMunicipio, Control), True)
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_MUNICIPIO) = Estado.Modifica
        ctrlMunicipios1.EnableLoadOnDemand = False
        ctrlMunicipios1.ShowDropDownOnTextboxClick = False
        ctrlMunicipios1.ExibeTituloParaSelecionarUmItem = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_MUNICIPIO) = Estado.Remove
        ctrlMunicipios1.HabiliteComponente(False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoMunicipio, Control), False)
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlMunicipios1.ExibeTituloParaSelecionarUmItem = True
    End Sub

    Private Sub PreecheUFs()
        cboUF.Items.Clear()

        For Each Item As UF In UF.ObtenhaTodos
            cboUF.Items.Add(New RadComboBoxItem(Item.Nome, CStr(Item.ID)))
        Next
    End Sub

    Private Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
                Servico.Excluir(ctrlMunicipios1.MunicipioSelecionado.ID.Value)
            End Using

            ExibaTelaInicial()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Município excluído com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IMunicipio
        Dim Municipio As IMunicipio

        Municipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMunicipio)()

        If CByte(ViewState(CHAVE_ESTADO_CD_MUNICIPIO)) <> Estado.Novo Then
            Municipio.ID = ctrlMunicipios1.MunicipioSelecionado.ID
        End If

        Municipio.Nome = ctrlMunicipios1.NomeDoMunicipio
        Municipio.UF = UF.Obtenha(CShort(cboUF.SelectedValue))

        If Not String.IsNullOrEmpty(txtCep.Text) Then
            Municipio.CEP = New CEP(CLng(txtCep.Text))
        End If

        Return Municipio
    End Function

    Private Sub ExibaMunicipio(ByVal Municipio As IMunicipio)
        ctrlMunicipios1.NomeDoMunicipio = Municipio.Nome

        If Not Municipio.CEP Is Nothing Then
            txtCep.Text = Municipio.CEP.Numero.ToString
        Else
            txtCep.Text = String.Empty
        End If

        cboUF.SelectedValue = Municipio.UF.ID.ToString
    End Sub

    Private Sub btnModificar_Click()
        Me.ExibaTelaModificar()
    End Sub

    Private Sub btnSalva_Click()
        Dim Municipio As IMunicipio = Nothing
        Dim Mensagem As String = Nothing

        Municipio = MontaObjeto()

        Try
            Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()

                If CByte(ViewState(CHAVE_ESTADO_CD_MUNICIPIO)) = Estado.Novo Then
                    Servico.Inserir(Municipio)
                    Mensagem = "Município cadastrado com sucesso."
                Else
                    Servico.Modificar(Municipio)
                    Mensagem = "Município modificado com sucesso."
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
        Return "FUN.NCL.001"
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

    Private Sub MunicipioFoiSelecionado(ByVal Municipio As IMunicipio)
        Me.ExibaTelaConsultar()
        Me.ExibaMunicipio(Municipio)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

End Class