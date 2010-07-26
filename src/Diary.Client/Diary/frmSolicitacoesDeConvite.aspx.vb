Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class frmSolicitacoesDeConvite
    Inherits SuperPagina

    Private Const CHAVE_SOLICITACOES As String = "CHAVE_SOLICITACOES_CONVITE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.DRY.003"
    End Function

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True

        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite)

        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        UtilidadesWeb.LimparComponente(CType(rdkLancamentos, Control))

        CarregaOpcoesDeFiltro()
        pnlCodigoDaSolicitacao.Visible = False
        pnlEntreDadas.Visible = True
        pnlContato.Visible = False
        chkConsiderarSolicitacoesFinalizadas.Checked = False

        Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeConvite(Not chkConsiderarSolicitacoesFinalizadas.Checked)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub CarregaOpcoesDeFiltro()
        rblOpcaoFiltro.Items.Clear()

        rblOpcaoFiltro.Items.Add(New ListItem("Por código", "1"))
        rblOpcaoFiltro.Items.Add(New ListItem("Entre datas", "2"))
        rblOpcaoFiltro.Items.Add(New ListItem("Por contato", "3"))

        'Seta o valor entre datas como inicial
        rblOpcaoFiltro.SelectedValue = "2"
    End Sub

    Private Sub ExibaSolicitacoes(ByVal Solicitacoes As IList(Of ISolicitacaoDeConvite))
        Session(CHAVE_SOLICITACOES) = Solicitacoes
        Me.grdItensLancados.DataSource = Solicitacoes
        Me.grdItensLancados.DataBind()
    End Sub

    Protected Sub btnNovo_Click()
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Nova solicitação de convite"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeConvite.aspx")
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnImprimir"
                btnImprir_Click()
        End Select
    End Sub

    Private Sub grdItensLancados_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdItensLancados.ItemCommand
        Dim ID As Long
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            ID = CLng(e.Item.Cells(4).Text)
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Solicitacoes As IList(Of ISolicitacaoDeConvite)
            Solicitacoes = CType(Session(CHAVE_SOLICITACOES), IList(Of ISolicitacaoDeConvite))
            Solicitacoes.RemoveAt(IndiceSelecionado)
            ExibaSolicitacoes(Solicitacoes)

            Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
                Servico.Remover(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/cdSolicitacaoDeConvite.aspx", "?Id=", ID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastrar solicitação de convite"), False)

        ElseIf e.CommandName = "Finalizar" Then
            Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
                Servico.Finalizar(ID)
            End Using
        ElseIf e.CommandName = "Despachar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Diary/frmDespachoDeSolicitacao.aspx", "?Id=", ID, "&Tipo=", TipoDeSolicitacao.Audiencia.ID.ToString)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Despachar solicitação de convite"), False)
        End If
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        'Coluna com botão modificar
        grdItensLancados.Columns(0).Visible = Principal.EstaAutorizado("OPE.DRY.003.0002")

        'Coluna com botão excluir
        grdItensLancados.Columns(1).Visible = Principal.EstaAutorizado("OPE.DRY.003.0003")

        'Coluna com botão despachar
        grdItensLancados.Columns(9).Visible = Principal.EstaAutorizado("OPE.DRY.003.0004")

        'Coluna com botão finalizar
        grdItensLancados.Columns(10).Visible = Principal.EstaAutorizado("OPE.DRY.003.0005")

    End Sub

    Private Sub grdItensLancados_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdItensLancados.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdItensLancados, Session(CHAVE_SOLICITACOES), e)
    End Sub

    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        If txtDataInicial.IsEmpty Then
            UtilidadesWeb.MostraMensagemDeInconsitencia("O data inicial da solicitação de convite deve ser informada.")
            Exit Sub
        End If

        If txtDataFinal.IsEmpty Then
            UtilidadesWeb.MostraMensagemDeInconsitencia("O data final da solicitação de convite deve ser informada.")
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite)

        Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()

            Solicitacoes = Servico.ObtenhaSolicitacoesDeConvite(Not chkConsiderarSolicitacoesFinalizadas.Checked, _
                                                                txtDataInicial.SelectedDate.Value, txtDataFinal.SelectedDate.Value)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub rblOpcaoFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblOpcaoFiltro.SelectedIndexChanged
        Dim ValorSelecionado As String

        ValorSelecionado = rblOpcaoFiltro.SelectedValue

        If ValorSelecionado = "1" Then
            pnlCodigoDaSolicitacao.Visible = True
            pnlEntreDadas.Visible = False
            pnlContato.Visible = False
        ElseIf ValorSelecionado = "2" Then
            pnlCodigoDaSolicitacao.Visible = False
            pnlEntreDadas.Visible = True
            pnlContato.Visible = False
        ElseIf ValorSelecionado = "3" Then
            pnlCodigoDaSolicitacao.Visible = False
            pnlEntreDadas.Visible = False
            pnlContato.Visible = True
        End If
    End Sub

    Protected Sub btnPesquisarPorCodigo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarPorCodigo.Click
        If String.IsNullOrEmpty(txtCodigoDaSolicitacao.Text) Then
            UtilidadesWeb.MostraMensagemDeInconsitencia("O código da solicitação de convite deve ser informado.")
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite) = New List(Of ISolicitacaoDeConvite)

        Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
            Dim Solicitacao As ISolicitacaoDeConvite

            Solicitacao = Servico.ObtenhaSolicitacaoPorCodigo(CLng(txtCodigoDaSolicitacao.Value))
            If Not Solicitacao Is Nothing Then Solicitacoes.Add(Solicitacao)
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

    Private Sub btnImprir_Click()
        Dim NomeDoArquivo As String
        Dim Gerador As GeradorDeSolicitacoesDeConviteEmPDF
        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite)
        Dim URL As String

        Solicitacoes = CType(Session(CHAVE_SOLICITACOES), IList(Of ISolicitacaoDeConvite))

        Gerador = New GeradorDeSolicitacoesDeConviteEmPDF(Solicitacoes)
        NomeDoArquivo = Gerador.GerePDFSolicitacoesEmAberto
        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir"), False)
    End Sub

    Private Sub cboContato_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboContato.ItemsRequested
        Dim Contatos As IList(Of IContato)

        Using Servico As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contatos = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Contatos Is Nothing Then
            For Each Contato As IContato In Contatos
                Dim Item As New RadComboBoxItem(Contato.Pessoa.Nome, Contato.Pessoa.ID.ToString)

                Dim TelefoneResidencial As ITelefone
                Dim TelefoneCelular As ITelefone

                TelefoneResidencial = Contato.Pessoa.ObtenhaTelelefone(TipoDeTelefone.Residencial)
                TelefoneCelular = Contato.Pessoa.ObtenhaTelelefone(TipoDeTelefone.Celular)

                If Not TelefoneResidencial Is Nothing Then
                    Item.Attributes.Add("Telefone", TelefoneResidencial.ToString)
                Else
                    Item.Attributes.Add("Telefone", "")
                End If

                If Not TelefoneCelular Is Nothing Then
                    Item.Attributes.Add("Celular", TelefoneCelular.ToString)
                Else
                    Item.Attributes.Add("Celular", "")
                End If

                If Not String.IsNullOrEmpty(Contato.Cargo) Then
                    Item.Attributes.Add("Cargo", Contato.Cargo)
                Else
                    Item.Attributes.Add("Cargo", "")
                End If

                cboContato.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub btnPesquisarPorContato_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarPorContato.Click
        If String.IsNullOrEmpty(cboContato.SelectedValue) Then
            UtilidadesWeb.MostraMensagemDeInconsitencia("O contato da solicitação de convite deve ser informado.")
            Exit Sub
        End If

        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite) = New List(Of ISolicitacaoDeConvite)

        Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
            Solicitacoes = Servico.ObtenhaSolicitacoesDeConvite(Not chkConsiderarSolicitacoesFinalizadas.Checked, CLng(cboContato.SelectedValue))
        End Using

        ExibaSolicitacoes(Solicitacoes)
    End Sub

End Class