Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.Telefone

Partial Public Class frmDespachoDeSolicitacao
    Inherits System.Web.UI.Page

    Private Const CHAVE_DESPACHOS_DA_SOLICITACAO As String = "CHAVE_DESPACHOS_DA_SOLICITACAO"
    Private Const CHAVE_SOLICITACAO As String = "CHAVE_SOLICITACAO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlDespachoAgenda1.SolicitacaoFoiDespachada, AddressOf SolicitacaoFoiDespachada
        AddHandler ctrlDespachoTarefa1.SolicitacaoFoiDespachada, AddressOf SolicitacaoFoiDespachada
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ProprietarioFoiSelecionado

        If Not IsPostBack Then
            Dim Id As Nullable(Of Long)
            Dim Tipo As TipoDeSolicitacao = Nothing

            If Not String.IsNullOrEmpty(Request.QueryString("Id")) Then
                Id = CLng(Request.QueryString("Id"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("Tipo")) Then
                Tipo = TipoDeSolicitacao.Obtenha(CByte(Request.QueryString("Tipo")))
            End If

            ExibaTelaInicial()

            If Not Id Is Nothing AndAlso Not Tipo Is Nothing Then
                Dim Solicitacao As ISolicitacao

                If Tipo.Equals(TipoDeSolicitacao.Audiencia) Then
                    Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeAudienciaLazyLoad)(CLng(Id))
                Else
                    Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeConviteLazyLoad)(CLng(Id))
                End If

                ctrlDespachoAgenda1.Solicitacao = Solicitacao
                ctrlDespachoTarefa1.Solicitacao = Solicitacao
                ViewState(CHAVE_SOLICITACAO) = Solicitacao
                CarregueTodosOsDespachosDaSolicitacao(Id.Value)
                AlimentaDados()
            End If
        End If
    End Sub

    Private Sub CarregueTodosOsDespachosDaSolicitacao(ByVal IDDaSolicitacao As Long)
        Dim DespachosDaSolicitacao As IList(Of IDespacho)

        Using Servico As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
            DespachosDaSolicitacao = Servico.ObtenhaDespachosDaSolicitacao(CLng(IDDaSolicitacao))
            ExibaDespachos(DespachosDaSolicitacao)
        End Using
    End Sub

    Private Sub ProprietarioFoiSelecionado(ByVal Pessoa As IPessoa)
        Dim Agenda As IAgenda

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Agenda = Servico.ObtenhaAgenda(Pessoa)
        End Using

        lblInconsistencia.Visible = True

        If Agenda Is Nothing Then
            lblInconsistencia.Text = "O alvo selecionado para o despacho ainda não possui agenda cadastrada."
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("O alvo selecionado para o despacho ainda não possui agenda cadastrada."), False)
            Exit Sub
        End If

        lblInconsistencia.Visible = False
        ctrlDespachoAgenda1.IDAlvo = ctrlPessoa1.PessoaSelecionada.ID.Value
        ctrlDespachoTarefa1.IDAlvo = ctrlPessoa1.PessoaSelecionada.ID.Value
    End Sub

    Private Sub SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)
        Dim Despachos As IList(Of IDespacho)

        Despachos = CType(ViewState(CHAVE_DESPACHOS_DA_SOLICITACAO), IList(Of IDespacho))
        Despachos.Add(Despacho)
        ExibaDespachos(Despachos)
    End Sub

    Private Sub ExibaDespachos(ByVal Despachos As IList(Of IDespacho))
        grdDespachos.DataSource = Despachos
        grdDespachos.DataBind()
        ViewState(CHAVE_DESPACHOS_DA_SOLICITACAO) = Despachos
    End Sub

    Private Sub CarregaDados()
        cboDespacho.Items.Clear()
        cboTipoDespachoFiltro.Items.Clear()

        For Each Item As TipoDeDespacho In TipoDeDespacho.ObtenhaTodos
            cboDespacho.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
            cboTipoDespachoFiltro.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next

        cboTipoDeFiltro.Items.Clear()
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Nenhum", "0"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Entre datas", "1"))
        cboTipoDeFiltro.Items.Add(New RadComboBoxItem("Tipo de despacho", "2"))
    End Sub

    Private Sub ExibaTelaInicial()
        LimpaDados()
        CarregaDados()
        cboDespacho.SelectedValue = TipoDeDespacho.Agendar.ID.ToString
        SetaTipoDeDespachoNosControles(TipoDeDespacho.Agendar)
        pnlComponenteDespachoAgenda.Visible = True
        pnlComponenteDespachoTarefa.Visible = False
        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = False
        ctrlPessoa1.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa1.SetaTipoDePessoaPadrao(TipoDePessoa.Fisica)
        pnlEntreDadas.Visible = False
        pnlTipoDeDespacho.Visible = False
    End Sub

    Private Sub LimpaDados()
        UtilidadesWeb.LimparComponente(CType(pnlComponenteDespachoAgenda, Control))
        UtilidadesWeb.LimparComponente(CType(pnlComponenteDespachoTarefa, Control))
    End Sub

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnSalvar"
        End Select
    End Sub

    Private Sub cboDespacho_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboDespacho.SelectedIndexChanged
        Dim Tipo As TipoDeDespacho

        Tipo = TipoDeDespacho.Obtenha(CByte(cboDespacho.SelectedValue))

        Select Case cboDespacho.SelectedValue
            Case TipoDeDespacho.Agendar.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Lembrente.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Mensagem.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Presente.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Remarcar.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
            Case TipoDeDespacho.Representante.ID.ToString
                pnlComponenteDespachoAgenda.Visible = True
                pnlComponenteDespachoTarefa.Visible = False
            Case TipoDeDespacho.Telegrama.ID.ToString
                pnlComponenteDespachoAgenda.Visible = False
                pnlComponenteDespachoTarefa.Visible = True
        End Select

        SetaTipoDeDespachoNosControles(Tipo)
        AlimentaDados()
    End Sub

    Private Sub SetaTipoDeDespachoNosControles(ByVal TipoDeDespacho As TipoDeDespacho)
        ctrlDespachoAgenda1.TipoDespacho = TipoDeDespacho
        ctrlDespachoTarefa1.TipoDespacho = TipoDeDespacho
    End Sub

    Private Sub btnPesquisarEntreDadas_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarEntreDadas.Click
        Dim Despachos As IList(Of IDespacho)

        If Not txtDataInicial.SelectedDate.HasValue Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("A data de início deve ser informada."), False)
            Exit Sub
        End If

        Using Servico As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
            Despachos = Servico.ObtenhaDespachosDaSolicitacao(CType(ViewState(CHAVE_SOLICITACAO), ISolicitacao).ID.Value, txtDataInicial.SelectedDate.Value, txtDataFinal.SelectedDate)
        End Using

        ExibaDespachos(Despachos)
    End Sub

    Private Sub btnPesquisarPorTipoDeDespacho_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarPorTipoDeDespacho.Click
        Dim Despachos As IList(Of IDespacho)
        Dim TipoDeDespachoSelecionado As TipoDeDespacho

        TipoDeDespachoSelecionado = TipoDeDespacho.Obtenha(CByte(cboTipoDespachoFiltro.SelectedValue))

        Using Servico As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
            Despachos = Servico.ObtenhaDespachosDaSolicitacao(CType(ViewState(CHAVE_SOLICITACAO), ISolicitacao).ID.Value, TipoDeDespachoSelecionado)
        End Using

        ExibaDespachos(Despachos)
    End Sub

    Private Sub AlimentaDados()
        Dim Solicitacao As ISolicitacao

        Solicitacao = CType(ViewState(CHAVE_SOLICITACAO), ISolicitacao)

        If Solicitacao Is Nothing Then Exit Sub

        If Solicitacao.Tipo.Equals(TipoDeSolicitacao.Audiencia) Then
            ctrlDespachoAgenda1.Assunto = CType(Solicitacao, ISolicitacaoDeAudiencia).Assunto
            ctrlDespachoTarefa1.Assunto = CType(Solicitacao, ISolicitacaoDeAudiencia).Assunto
        ElseIf Solicitacao.Tipo.Equals(TipoDeSolicitacao.Convite) Then
            ctrlDespachoAgenda1.Assunto = "Convite"
            ctrlDespachoTarefa1.Assunto = "Convite"
            ctrlDespachoAgenda1.Inicio = CType(Solicitacao, ISolicitacaoDeConvite).DataEHorario
            ctrlDespachoTarefa1.Inicio = CType(Solicitacao, ISolicitacaoDeConvite).DataEHorario
        End If

        ctrlDespachoAgenda1.Local = Solicitacao.Local

        Dim DescricaoDaSoliticao As New StringBuilder
        'Nome do contato da solicitação
        DescricaoDaSoliticao.AppendLine(Solicitacao.Contato.Pessoa.Nome)

        'Cargo do contato
        If Not String.IsNullOrEmpty(Solicitacao.Contato.Cargo) Then
            DescricaoDaSoliticao.AppendLine(Solicitacao.Contato.Cargo)
        End If

        'Telefones do contato
        Dim TelefonesSTR As New StringBuilder

        Dim TelefonesResidencial As IList(Of ITelefone)
        Dim TelefonesComercial As IList(Of ITelefone)
        Dim TelefonesCelular As IList(Of ITelefone)

        TelefonesResidencial = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Residencial)
        TelefonesComercial = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Comercial)
        TelefonesCelular = Solicitacao.Contato.Pessoa.ObtenhaTelelefones(TipoDeTelefone.Celular)

        For Each Telefone As ITelefone In TelefonesResidencial
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        For Each Telefone As ITelefone In TelefonesComercial
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        For Each Telefone As ITelefone In TelefonesCelular
            TelefonesSTR.Append(String.Concat(Telefone.ToString, " "))
        Next

        'Se tiver telefones
        If Not TelefonesSTR.Length = 0 Then
            DescricaoDaSoliticao.AppendLine(TelefonesSTR.ToString)
        End If

        'Descrição da solicitação
        If Not String.IsNullOrEmpty(Solicitacao.Descricao) Then
            DescricaoDaSoliticao.AppendLine(Solicitacao.Descricao)
        End If

        'Observação da solicitação de convite
        If Solicitacao.Tipo.Equals(TipoDeSolicitacao.Convite) Then
            If String.IsNullOrEmpty(CType(Solicitacao, ISolicitacaoDeConvite).Observacao) Then
                DescricaoDaSoliticao.AppendLine(CType(Solicitacao, ISolicitacaoDeConvite).Observacao)
            End If
        End If

        ctrlDespachoAgenda1.Descricao = DescricaoDaSoliticao.ToString
        ctrlDespachoTarefa1.Descricao = DescricaoDaSoliticao.ToString
    End Sub

    Private Sub cboTipoDeFiltro_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTipoDeFiltro.SelectedIndexChanged
        Select Case cboTipoDeFiltro.SelectedValue
            Case "0"
                pnlEntreDadas.Visible = False
                pnlTipoDeDespacho.Visible = False
                CarregueTodosOsDespachosDaSolicitacao(CType(ViewState(CHAVE_SOLICITACAO), ISolicitacao).ID.Value)
            Case "1"
                pnlEntreDadas.Visible = True
                pnlTipoDeDespacho.Visible = False
            Case "2"
                pnlEntreDadas.Visible = False
                pnlTipoDeDespacho.Visible = True
        End Select
    End Sub

    Private Sub toolDespachos_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles toolDespachos.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnImprimirDespachos"
                ImprimirDespachos()
        End Select
    End Sub

    Private Sub ImprimirDespachos()
        Dim NomeDoArquivo As String
        Dim Gerador As GeradorDeDespachosEmPDF
        Dim Despachos As IList(Of IDespacho)
        Dim URL As String

        Despachos = CType(ViewState(CHAVE_DESPACHOS_DA_SOLICITACAO), IList(Of IDespacho))

        If Despachos Is Nothing AndAlso Despachos.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Não existe nenhum despacho para imprimir."), False)
        End If

        Gerador = New GeradorDeDespachosEmPDF(Despachos)
        NomeDoArquivo = Gerador.GerePDFSolicitacoesEmAberto
        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir"), False)
    End Sub
End Class