Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Servicos
Imports Compartilhados.Fabricas

Partial Public Class cdProduto
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO"
    Private CHAVE_OBJETO As String = "CHAVE_OBJETO_PRODUTO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlProduto1.ProdutoFoiSelecionado, AddressOf ExibaProduto
        
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
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDoProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlImpostosValores, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlObservacoes, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlProduto, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlObservacoes, Control), False)
        ViewState(CHAVE_ESTADO) = Estado.Inicial
        ctrlProduto1.HabilitaEmptyMessage(True)
        ctrlProduto1.HabilitaEnableLoadOnDemandEAutoPostBack(True)
        Session(CHAVE_OBJETO) = Nothing
        CarregaDadosIniciais()
        pnlQuantidadeEmEstoque.Visible = True
        txtQtdEstoque.Enabled = False
    End Sub

    Private Sub CarregaDadosIniciais()
        cboMedida.Items.Clear()

        For Each Item As UnidadeDeMedida In UnidadeDeMedida.ObtenhaTodos
            cboMedida.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
        Session(CHAVE_OBJETO) = FabricaGenerica.GetInstancia.CrieObjeto(Of IProduto)()
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO) = Estado.Novo
        UtilidadesWeb.LimparComponente(CType(pnlCaracteristicasDoProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlImpostosValores, Control))
        UtilidadesWeb.LimparComponente(CType(pnlProduto, Control))
        UtilidadesWeb.LimparComponente(CType(pnlObservacoes, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlObservacoes, Control), True)
        ctrlProduto1.HabilitaEmptyMessage(False)
        ctrlProduto1.HabilitaEnableLoadOnDemandEAutoPostBack(False)
        CarregaDadosIniciais()
        pnlQuantidadeEmEstoque.Visible = False
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlImpostosValores, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlCaracteristicasDoProduto, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlObservacoes, Control), True)
        ctrlProduto1.HabilitaEmptyMessage(False)
        ctrlProduto1.HabilitaEnableLoadOnDemandEAutoPostBack(False)
        pnlQuantidadeEmEstoque.Visible = True
        txtQtdEstoque.Enabled = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO) = Estado.Remove
        UtilidadesWeb.HabilitaComponentes(CType(pnlProduto, Control), False)
        ctrlProduto1.HabilitaEmptyMessage(False)
        pnlQuantidadeEmEstoque.Visible = True
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ctrlProduto1.HabilitaEmptyMessage(False)
        pnlQuantidadeEmEstoque.Visible = True
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function ValidaDados() As String
        Dim Inconsistencias As IList(Of String) = New List(Of String)

        If String.IsNullOrEmpty(ctrlProduto1.NomeProdutoSelecionado()) Then Return "O nome do produto deve ser informado."
        If String.IsNullOrEmpty(cboGrupoDeProduto.SelectedValue) Then Return "O grupo de produto deve ser informado."

        Return Nothing
    End Function

    Private Sub btnSalva_Click()
        Dim Produto As IProduto = Nothing
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If


        Produto = MontaObjeto()

        Try
            Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
                If CByte(ViewState(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.InserirProduto(Produto)
                    Mensagem = "Produto cadastrado com sucesso."
                Else
                    Servico.AtualizarProduto(Produto)
                    Mensagem = "Produto modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As IProduto
        Dim Produto As IProduto

        Produto = CType(Session(CHAVE_OBJETO), IProduto)
        Produto.CodigoDeBarras = ctrlProduto1.CodigoDeBarrasProdutoSelecionado()
        Produto.Nome = ctrlProduto1.NomeProdutoSelecionado()
   
        Produto.Observacoes = txtObservacoes.Text
        Produto.PorcentagemDeLucro = txtPorcentagemDeLucro.Value
        Produto.QuantidadeMinimaEmEstoque = txtQtdEstoqueMinimo.Value

        If Not String.IsNullOrEmpty(cboMedida.SelectedValue) Then
            Produto.UnidadeDeMedida = UnidadeDeMedida.ObtenhaTipoDeUnidade(CChar(cboMedida.SelectedValue))
        End If

        Produto.ValorDeCusto = txtValorDeCusto.Value
        Produto.ValorDeVendaMinimo = txtValorDeVendaMinimo.Value

        Return Produto
    End Function

    Private Sub ExibaProduto(ByVal Produto As IProduto)
        Session(CHAVE_OBJETO) = Produto

        ctrlProduto1.ProdutoSelecionado = Produto
        cboGrupoDeProduto.SelectedValue = Produto.GrupoDeProduto.ID.Value.ToString
        cboGrupoDeProduto.Text = Produto.GrupoDeProduto.Nome

        If Not Produto.Marca Is Nothing Then
            cboMarca.SelectedValue = Produto.Marca.ID.Value.ToString
            cboMarca.Text = Produto.Marca.Nome
        End If

        txtQtdEstoqueMinimo.Value = Produto.QuantidadeMinimaEmEstoque
        txtObservacoes.Text = Produto.Observacoes
        txtPorcentagemDeLucro.Value = Produto.PorcentagemDeLucro

        If Not Produto.UnidadeDeMedida Is Nothing Then
            cboMedida.SelectedValue = Produto.UnidadeDeMedida.ID.ToString
        End If

        txtValorDeCusto.Value = Produto.ValorDeCusto
        txtValorDeVendaMinimo.Value = Produto.ValorDeVendaMinimo
        txtValorDeVenda.Value = Produto.ValorDeVenda
        txtQtdEstoque.Value = Produto.QuantidadeEmEstoque
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
        Dim Produto As IProduto

        Produto = CType(Session(CHAVE_OBJETO), IProduto)
        Try
            Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
                Servico.Remover(Produto.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Produto excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.ETQ.002"
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

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Dim Principal As Compartilhados.Principal

        'Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        ''Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        'If btnNovaMarca.Visible Then
        '    Principal.EstaAutorizado(btnNovaMarca.CommandArgument)
        'End If
    End Sub

    Private Sub btnNovaMarca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovaMarca.Click
        Dim URL As String

        URL = ObtenhaURLNovaMarca()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de marcas de produtos", 650, 450), False)
    End Sub

    Private Function ObtenhaURLNovoGrupoDeProduto() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        Return String.Concat(URL, "Estoque/cdGrupoDeProduto.aspx")
    End Function

    Private Function ObtenhaURLNovaMarca() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        Return String.Concat(URL, "Estoque/cdMarca.aspx")
    End Function

   Private Sub cboGrupoDeProduto_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboGrupoDeProduto.ItemsRequested
        Dim Grupos As IList(Of IGrupoDeProduto)

        Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
            Grupos = Servico.ObtenhaGruposDeProdutosPorNome(e.Text, 50)

            If Not Grupos Is Nothing Then
                For Each Grupo As IGrupoDeProduto In Grupos
                    cboGrupoDeProduto.Items.Add(New RadComboBoxItem(Grupo.Nome, Grupo.ID.ToString))
                Next
            End If
        End Using
    End Sub

    Private Sub cboMarca_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboMarca.ItemsRequested
        Dim Marcas As IList(Of IMarcaDeProduto)

        Using Servico As IServicoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeProduto)()
            Marcas = Servico.ObtenhaMarcasDeProdutosPorNome(e.Text, 50)

            If Not Marcas Is Nothing Then
                For Each Marca As IMarcaDeProduto In Marcas
                    cboMarca.Items.Add(New RadComboBoxItem(Marca.Nome, Marca.ID.ToString))
                Next
            End If
        End Using
    End Sub

    Private Sub cboGrupoDeProduto_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboGrupoDeProduto.SelectedIndexChanged
        Dim Grupo As IGrupoDeProduto
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeGrupoDeProduto = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGrupoDeProduto)()
            Grupo = Servico.ObtenhaGrupoDeProdutos(CLng(Valor))
        End Using

        If Not Grupo Is Nothing Then
            Dim Produto As IProduto

            Produto = CType(Session(CHAVE_OBJETO), IProduto)
            Produto.GrupoDeProduto = Grupo
            Session(CHAVE_OBJETO) = Produto
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

        If Not Marca Is Nothing Then
            Dim Produto As IProduto

            Produto = CType(Session(CHAVE_OBJETO), IProduto)
            Produto.Marca = Marca
            Session(CHAVE_OBJETO) = Produto
        End If
    End Sub

    Private Sub txtPorcentagemDeLucro_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPorcentagemDeLucro.TextChanged
        Dim Produto As IProduto

        Produto = CType(Session(CHAVE_OBJETO), IProduto)

        If Produto Is Nothing Then Exit Sub

        Produto.PorcentagemDeLucro = txtPorcentagemDeLucro.Value
        txtValorDeVenda.Value = Produto.ValorDeVenda
    End Sub

    Private Sub txtValorDeCusto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValorDeCusto.TextChanged
        Dim Produto As IProduto

        Produto = CType(Session(CHAVE_OBJETO), IProduto)

        If Produto Is Nothing Then Exit Sub

        Produto.ValorDeCusto = txtValorDeCusto.Value
        txtValorDeVenda.Value = Produto.ValorDeVenda
    End Sub

    Protected Sub btnNovoGrupoDoProduto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovoGrupoDoProduto.Click
        Dim URL As String

        URL = ObtenhaURLNovoGrupoDeProduto()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de grupo de produto", 650, 450), False)
    End Sub

End Class