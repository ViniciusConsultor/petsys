Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports T13.Interfaces.Negocio
Imports T13.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados

Partial Public Class cdServicos
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_SERVICO"
    Private CHAVE_ID_SERVICO_PRESTADO As String = "CHAVE_ID_SERVICO_PRESTADO"

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
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDosServicosPrestados, Control))
        UtilidadesWeb.LimparComponente(CType(pnlServicosPrestados, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlServicosPrestados, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosServicosPrestados, Control), False)
        Session(CHAVE_ESTADO) = Estado.Inicial
        cboServico.EmptyMessage = "Selecione um serviço"
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Novo
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDosServicosPrestados, Control))
        UtilidadesWeb.LimparComponente(CType(pnlServicosPrestados, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosServicosPrestados, Control), True)
        cboServico.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDosServicosPrestados, Control), True)
        cboServico.EmptyMessage = ""
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlServicosPrestados, Control), False)
        cboServico.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        cboServico.EmptyMessage = ""
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnSalva_Click()
        Dim ServicoPrestado As IServicoPrestado = Nothing
        Dim Mensagem As String

        ServicoPrestado = MontaObjeto()

        Try
            Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(ServicoPrestado)
                    Mensagem = "Serviço prestado cadastrado com sucesso."
                Else
                    Servico.Modificar(ServicoPrestado)
                    Mensagem = "Serviço prestado modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IServicoPrestado
        Dim ServicoPrestado As IServicoPrestado

        ServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoPrestado)()

        If CByte(Session(CHAVE_ESTADO)) <> Estado.Novo Then
            ServicoPrestado.ID = CLng(Session(CHAVE_ID_SERVICO_PRESTADO))
        End If

        ServicoPrestado.Nome = cboServico.Text
        ServicoPrestado.CaracterizaDesconto = chkCaracterizaDesconto.Checked
        ServicoPrestado.Valor = txtValorDoServico.Value

        Return ServicoPrestado
    End Function

    Private Sub ExibaServicoPrestado(ByVal ServicoPrestado As IServicoPrestado)
        cboServico.Text = ServicoPrestado.Nome
        Session(CHAVE_ID_SERVICO_PRESTADO) = ServicoPrestado.ID
        txtValorDoServico.Value = ServicoPrestado.Valor
        chkCaracterizaDesconto.Checked = ServicoPrestado.CaracterizaDesconto
    End Sub

    Private Sub btnModificar_Click()
        ExibaTelaModificar()
    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluir()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicial()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
                Servico.Excluir(CLng(Session(CHAVE_ID_SERVICO_PRESTADO)))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Serviço prestado excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.T13.001"
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

    Private Sub cboServico_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboServico.ItemsRequested
        Dim ServicosPrestados As IList(Of IServicoPrestado)

        Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
            ServicosPrestados = Servico.ObtenhaServicoPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not ServicosPrestados Is Nothing Then
            For Each ServicoPrestado As IServicoPrestado In ServicosPrestados
                cboServico.Items.Add(New RadComboBoxItem(ServicoPrestado.Nome, ServicoPrestado.ID.ToString))
            Next
        End If
    End Sub

    Private Sub cboServico_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboServico.SelectedIndexChanged
        Dim ServicoPrestado As IServicoPrestado
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
            ServicoPrestado = Servico.ObtenhaServico(CLng(Valor))
        End Using

        Me.ExibaServicoPrestado(ServicoPrestado)
        Me.ExibaTelaConsultar()
    End Sub

End Class