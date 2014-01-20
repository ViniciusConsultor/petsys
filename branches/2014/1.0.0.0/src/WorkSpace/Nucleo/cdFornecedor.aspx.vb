Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Servicos
Imports Telerik.Web.UI

Partial Public Class cdFornecedor
    Inherits SuperPagina

    Private CHAVE_ESTADO_CD_FORNECEDOR As String = "CHAVE_ESTADO_CD_FORNECEDOR"
    Private CHAVE_OBJ_FORNECEDOR As String = "CHAVE_OBJ_FORNECEDOR"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ObtenhaFornecedor
        AddHandler ctrlPessoa2.PessoaFoiSelecionada, AddressOf ContatoFoiSelecionado

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.013"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoFornecedor, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoFornecedor, Control), False)
        UtilidadesWeb.LimparComponente(CType(pnlDadosDosContatos, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDosContatos, Control), False)

        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True

        ctrlPessoa2.Inicializa()
        ctrlPessoa2.BotaoDetalharEhVisivel = False
        ctrlPessoa2.BotaoNovoEhVisivel = True
        ctrlPessoa2.SetaTipoDePessoaPadrao(TipoDePessoa.Fisica)
        ctrlPessoa2.OpcaoTipoDaPessoaEhVisivel = False
        ViewState(CHAVE_ESTADO_CD_FORNECEDOR) = Estado.Inicial
        ViewState(CHAVE_OBJ_FORNECEDOR) = Nothing
        MostraContatos(New List(Of IPessoaFisica))
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
        ViewState(CHAVE_ESTADO_CD_FORNECEDOR) = Estado.Novo
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoFornecedor, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDosContatos, Control), True)
        txtDataDoCadastro.SelectedDate = Now
        txtDataDoCadastro.Enabled = False
        ViewState(CHAVE_OBJ_FORNECEDOR) = FabricaGenerica.GetInstancia.CrieObjeto(Of IFornecedor)((New Object() {ctrlPessoa1.PessoaSelecionada}))
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_FORNECEDOR) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoFornecedor, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDosContatos, Control), True)
        txtDataDoCadastro.Enabled = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_FORNECEDOR) = Estado.Remove
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoFornecedor, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDosContatos, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Function MontaObjetoFornecedor() As IFornecedor
        Dim Fornecedor As IFornecedor

        Fornecedor = CType(ViewState(CHAVE_OBJ_FORNECEDOR), IFornecedor)
        Fornecedor.DataDoCadastro = txtDataDoCadastro.SelectedDate.Value
        Fornecedor.InformacoesAdicionais = ""
        Return Fornecedor
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String

        Dim Fornecedor As IFornecedor = MontaObjetoFornecedor()

        Try
            Using Servico As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
                If CByte(ViewState(CHAVE_ESTADO_CD_FORNECEDOR)) = Estado.Novo Then
                    Servico.Inserir(Fornecedor)
                    Mensagem = "Fornecedor cadastrado com sucesso."
                Else
                    Servico.Modificar(Fornecedor)
                    Mensagem = "Fornecedor modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
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
            Using Servico As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
                Servico.Remover(ctrlPessoa1.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Fornecedor excluido com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

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

    Private Sub ObtenhaFornecedor(ByVal Pessoa As IPessoa)
        Dim Fornecedor As IFornecedor

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeFornecedor = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeFornecedor)()
            Fornecedor = Servico.Obtenha(Pessoa)
        End Using

        If Fornecedor Is Nothing Then
            CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
            Exit Sub
        End If

        ViewState(CHAVE_OBJ_FORNECEDOR) = Fornecedor
        MostreFornecedor(Fornecedor)
        ExibaTelaConsultar()
    End Sub

    Private Sub ContatoFoiSelecionado(ByVal Pessoa As IPessoa)
        Dim Fornecedor As IFornecedor = CType(ViewState(CHAVE_OBJ_FORNECEDOR), IFornecedor)

        Try
            ctrlPessoa2.BotaoDetalharEhVisivel = True
            Fornecedor.AdicionaContato(CType(Pessoa, IPessoaFisica))
            ViewState(CHAVE_OBJ_FORNECEDOR) = Fornecedor
            MostraContatos(Fornecedor.Contatos)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub MostraContatos(ByVal Contatos As IList(Of IPessoaFisica))
        Me.grdContato.DataSource = Contatos
        Me.grdContato.DataBind()
    End Sub

    Private Sub MostreFornecedor(ByVal Fornecedor As IFornecedor)
        txtDataDoCadastro.SelectedDate = Fornecedor.DataDoCadastro
        MostraContatos(Fornecedor.Contatos)
    End Sub

End Class