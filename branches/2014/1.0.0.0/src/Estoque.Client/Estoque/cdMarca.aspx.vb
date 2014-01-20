Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Estoque.Interfaces.Negocio
Imports Compartilhados.Fabricas
Imports Estoque.Interfaces.Servicos
Imports Compartilhados

Partial Public Class cdMarca
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_CD_MARCA"
    Private CHAVE_ID As String = "CHAVE_ID_MARCA"

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
        UtilidadesWeb.LimparComponente(CType(pnlMarca, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlMarca, Control), True)
        Session(CHAVE_ESTADO) = Estado.Inicial
        cboMarca.EmptyMessage = "Selecione uma marca de produto"
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
        UtilidadesWeb.LimparComponente(CType(pnlMarca, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlMarca, Control), True)
        cboMarca.EmptyMessage = ""
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlMarca, Control), True)
        cboMarca.EmptyMessage = ""
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
        UtilidadesWeb.HabilitaComponentes(CType(pnlMarca, Control), False)
        cboMarca.EmptyMessage = ""
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        cboMarca.EmptyMessage = ""
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function ValidaDados() As String
        If String.IsNullOrEmpty(cboMarca.Text) Then
            Return "O nome da marca do produto deve ser informado."
        End If

        Return Nothing
    End Function

    Private Sub btnSalva_Click()
        Dim MarcaDeProduto As IMarcaDeProduto = Nothing
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        MarcaDeProduto = MontaObjeto()

        Try
            Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.InserirMarcaDeProduto(MarcaDeProduto)
                    Mensagem = "Marca de produto cadastrado com sucesso."
                Else
                    Servico.AtualizarMarcaDeProduto(MarcaDeProduto)
                    Mensagem = "Marca de produto alterado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IMarcaDeProduto
        Dim Marca As IMarcaDeProduto

        Marca = FabricaGenerica.GetInstancia.CrieObjeto(Of IMarcaDeProduto)()

        If CByte(Session(CHAVE_ESTADO)) <> Estado.Novo Then
            Marca.ID = CLng(Session(CHAVE_ID))
        End If

        Marca.Nome = cboMarca.Text

        Return Marca
    End Function

    Private Sub ExibaMarcaDeProduto(ByVal Marca As IMarcaDeProduto)
        cboMarca.Text = Marca.Nome
        Session(CHAVE_ID) = Marca.ID
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
            Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
                Servico.RemoverMarcaDeProduto(CLng(Session(CHAVE_ID)))
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Marca de produto removida com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.ETQ.003"
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

    Private Sub cboMarca_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboMarca.ItemsRequested
        Dim Marcas As IList(Of IMarcaDeProduto)

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Marcas = Servico.ObtenhaMarcasDeProdutosPorNome(e.Text, 50)
        End Using

        If Not Marcas Is Nothing Then
            For Each Marca As IMarcaDeProduto In Marcas
                cboMarca.Items.Add(New RadComboBoxItem(Marca.Nome, Marca.ID.ToString))
            Next
        End If
    End Sub

    Private Sub cboMarca_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMarca.SelectedIndexChanged
        Dim Marca As IMarcaDeProduto
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Marca = Servico.ObtenhaMarcaDeProduto(CLng(Valor))
        End Using

        Me.ExibaMarcaDeProduto(Marca)
        Me.ExibaTelaConsultar()
    End Sub

End Class