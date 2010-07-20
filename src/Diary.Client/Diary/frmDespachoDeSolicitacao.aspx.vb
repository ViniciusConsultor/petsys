Imports Telerik.Web.UI
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio.LazyLoad

Partial Public Class frmDespachoDeSolicitacao
    Inherits System.Web.UI.Page

    Private Const CHAVE_DESPACHOS_DA_SOLICITACAO As String = "CHAVE_DESPACHOS_DA_SOLICITACAO"

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
                Dim DespachosDaSolicitacao As IList(Of IDespacho)
                Dim Solicitacao As ISolicitacao

                If Tipo.Equals(TipoDeSolicitacao.Audiencia) Then
                    Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeAudienciaLazyLoad)(CLng(Id))
                Else
                    Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeConviteLazyLoad)(CLng(Id))
                End If

                ctrlDespachoAgenda1.Solicitacao = Solicitacao
                ctrlDespachoTarefa1.Solicitacao = Solicitacao

                Using Servico As IServicoDeDespacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeDespacho)()
                    DespachosDaSolicitacao = Servico.ObtenhaDespachosDaSolicitacao(CLng(Id))
                    ExibaDespachos(DespachosDaSolicitacao)
                End Using
            End If
        End If
    End Sub

    Private Sub ProprietarioFoiSelecionado(ByVal Pessoa As IPessoa)
        Dim Agenda As IAgenda

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Agenda = Servico.ObtenhaAgenda(Pessoa)
        End Using

        If Agenda Is Nothing Then
            UtilidadesWeb.MostraMensagemDeInformacao("O alvo selecionado para o despacho ainda não possui agenda cadastrada.")
            Exit Sub
        End If

        ctrlDespachoAgenda1.IDAlvo = ctrlPessoa1.PessoaSelecionada.ID.Value
        ctrlDespachoTarefa1.IDAlvo = ctrlPessoa1.PessoaSelecionada.ID.Value
    End Sub

    Private Sub SolicitacaoFoiDespachada(ByVal Despacho As IDespacho)
        Dim Despachos As IList(Of IDespacho)

        Despachos = CType(Session(CHAVE_DESPACHOS_DA_SOLICITACAO), IList(Of IDespacho))
        Despachos.Add(Despacho)
        ExibaDespachos(Despachos)
    End Sub

    Private Sub ExibaDespachos(ByVal Despachos As IList(Of IDespacho))
        grdDespachos.DataSource = Despachos
        grdDespachos.DataBind()
        Session(CHAVE_DESPACHOS_DA_SOLICITACAO) = Despachos
    End Sub

    Private Sub CarregaDados()
        cboDespacho.Items.Clear()

        For Each Item As TipoDeDespacho In TipoDeDespacho.ObtenhaTodos
            cboDespacho.Items.Add(New RadComboBoxItem(Item.Descricao, Item.ID.ToString))
        Next
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
    End Sub

    Private Sub SetaTipoDeDespachoNosControles(ByVal TipoDeDespacho As TipoDeDespacho)
        ctrlDespachoAgenda1.TipoDespacho = TipoDeDespacho
        ctrlDespachoTarefa1.TipoDespacho = TipoDeDespacho
    End Sub

End Class