Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports T13.Interfaces.Negocio
Imports T13.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos

Partial Public Class frmLancamentoDeServicosPrestados
    Inherits SuperPagina

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private CHAVE_ESTADO As String = "CHAVE_ESTADO_FRM_LANCAMENTO_DE_SERVICOS"
    Private CHAVE_SERVICO_PRESTADO_SELECIONADO As String = "CHAVE_SERVICO_PRESTADO_SELECIONADO"
    Private CHAVE_LANCAMENTO_SERVICOS_PRESTADOS As String = "CHAVE_LANCAMENTO_SERVICOS_PRESTADOS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnImprimir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnReaproveitar"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Inicial
        Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = Nothing
        Session(CHAVE_SERVICO_PRESTADO_SELECIONADO) = Nothing
        UtilidadesWeb.LimparComponente(CType(pnlCliente, Control))
        UtilidadesWeb.LimparComponente(CType(pnlDadosBasicos, Control))
        UtilidadesWeb.LimparComponente(CType(pnlLancamentos, Control))
        UtilidadesWeb.LimparComponente(CType(pnlLancamentosDoCliente, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlCliente, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlLancamentos, Control), False)
        rdkLancamentosDoCliente.Visible = False
        Me.ExibaItensDeLancamento(New List(Of IItemDeLancamento), 0, 0)
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovo()
        Dim LancamentoDeServicosPrestados As ILacamentoDeServicosPrestados

        LancamentoDeServicosPrestados = FabricaGenerica.GetInstancia.CrieObjeto(Of ILacamentoDeServicosPrestados)()
        Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = LancamentoDeServicosPrestados
        txtDataDoLancamento.SelectedDate = LancamentoDeServicosPrestados.DataDeLancamento
        Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            txtNumero.Text = Servico.ObtenhaProximoNumeroDisponivel.ToString
        End Using
    End Sub

    Private Sub ExibaTelaNovo()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnImprimir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnReaproveitar"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Novo
        UtilidadesWeb.LimparComponente(CType(pnlDadosBasicos, Control))
        UtilidadesWeb.LimparComponente(CType(pnlLancamentos, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlLancamentos, Control), True)
        rdkLancamentosDoCliente.Visible = False
    End Sub

    Private Sub ExibaTelaModificar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnImprimir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnReaproveitar"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), True)
        UtilidadesWeb.HabilitaComponentes(CType(pnlLancamentos, Control), True)
        rdkLancamentosDoCliente.Visible = False
    End Sub

    Private Sub ExibaTelaExcluir()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnImprimir"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnReaproveitar"), RadToolBarButton).Visible = False
        Session(CHAVE_ESTADO) = Estado.Remove
        rdkLancamentosDoCliente.Visible = False
    End Sub

    Private Sub ExibaTelaConsultar()
        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        CType(rtbToolBar.FindButtonByCommandName("btnImprimir"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnReaproveitar"), RadToolBarButton).Visible = True
        rdkLancamentosDoCliente.Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosBasicos, Control), False)
        UtilidadesWeb.HabilitaComponentes(CType(pnlLancamentos, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicial()
    End Sub

    Private Sub btnSalva_Click()
        Dim LancamentoDeServicos As ILacamentoDeServicosPrestados = Nothing
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ValidaLancamento()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        LancamentoDeServicos = MontaObjeto()

        Try
            Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
                If CByte(Session(CHAVE_ESTADO)) = Estado.Novo Then
                    Servico.Inserir(LancamentoDeServicos)
                    Mensagem = "Lançamento de servicos prestados cadastrado com sucesso."
                Else
                    Servico.Modificar(LancamentoDeServicos)
                    Mensagem = "Lançamento de servicos prestados modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaConsultar()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function MontaObjeto() As ILacamentoDeServicosPrestados
        Dim LancamentoDeServicos As ILacamentoDeServicosPrestados = Nothing

        LancamentoDeServicos = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)
        LancamentoDeServicos.Aliquota = txtAliquota.Text

        Using ServicoDeCliente As IServicoDeCliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCliente)()
            LancamentoDeServicos.Cliente = ServicoDeCliente.Obtenha(CLng(cboCliente.SelectedValue))
        End Using

        LancamentoDeServicos.DataDeLancamento = txtDataDoLancamento.SelectedDate.Value
        LancamentoDeServicos.NaturezaDaOperacao = txtNaturezaDaOperacao.Text
        LancamentoDeServicos.Observacoes = txtObservacoes.Text
        LancamentoDeServicos.Numero = CInt(txtNumero.Text)

        Return LancamentoDeServicos
    End Function

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
        Dim Id As Long

        Id = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados).ID.Value

        Try
            Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
                Servico.Excluir(Id)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Lançamento excluído com sucesso."), False)
            ExibaTelaInicial()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.T13.002"
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
            Case "btnImprimir"
                Call btnImprir_Click()
            Case "btnReaproveitar"
                Call btnReaproveitar_Click()
        End Select
    End Sub

    Private Sub btnImprir_Click()
        Dim NomeDoArquivo As String
        Dim Gerador As CriadorDeRelatorio
        Dim URL As String
        Dim Lancamento As ILacamentoDeServicosPrestados
        Dim Lancamentos As New List(Of ILacamentoDeServicosPrestados)

        Lancamento = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)
        Lancamentos.Add(Lancamento)

        Gerador = New CriadorDeRelatorio(Lancamentos)
        NomeDoArquivo = Gerador.GereNotaFiscal()
        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoArquivo
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraArquivoParaDownload(URL, "Imprimir"), False)
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub cboCliente_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboCliente.ItemsRequested
        Dim Clientes As IList(Of ICliente)

        Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCliente)()
            Clientes = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Clientes Is Nothing Then
            For Each Cliente As ICliente In Clientes
                cboCliente.Items.Add(New RadComboBoxItem(Cliente.Pessoa.Nome, Cliente.Pessoa.ID.Value.ToString))
            Next
        End If
    End Sub

    Private Sub cboCliente_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCliente.SelectedIndexChanged
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        cboLancamentos.Items.Clear()

        If String.IsNullOrEmpty(Valor) Then
            CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
            CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
            cboCliente.Enabled = True
            Exit Sub
        End If

        CType(rtbToolBar.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
        CType(rtbToolBar.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        cboCliente.Enabled = False

        Dim LancamentosDoClienteSelecionado As IList(Of ILacamentoDeServicosPrestados)

        Using ServicoDeLancamento As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            LancamentosDoClienteSelecionado = ServicoDeLancamento.ObtenhaLancamentosTardio(CLng(Valor))
        End Using

        If LancamentosDoClienteSelecionado Is Nothing OrElse LancamentosDoClienteSelecionado.Count = 0 Then Exit Sub

        rdkLancamentosDoCliente.Visible = True

        For Each Lancamento As ILacamentoDeServicosPrestados In LancamentosDoClienteSelecionado
            cboLancamentos.Items.Add(New RadComboBoxItem(Lancamento.DataDeLancamento.ToString("dd/MM/yyyy"), Lancamento.ID.Value.ToString))
        Next

    End Sub

    Private Sub cboServicosPrestados_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboServicosPrestados.ItemsRequested
        Dim ServicosPrestados As IList(Of IServicoPrestado)

        Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
            ServicosPrestados = Servico.ObtenhaServicoPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not ServicosPrestados Is Nothing Then
            For Each ServicoPrestado As IServicoPrestado In ServicosPrestados
                cboServicosPrestados.Items.Add(New RadComboBoxItem(ServicoPrestado.Nome, ServicoPrestado.ID.ToString))
            Next
        End If
    End Sub

    Private Sub cboServicosPrestados_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboServicosPrestados.SelectedIndexChanged
        Dim ServicoPrestado As IServicoPrestado
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then Return

        Using Servico As IServicoDeServicoPrestado = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeServicoPrestado)()
            ServicoPrestado = Servico.ObtenhaServico(CLng(Valor))
        End Using

        ExibaServicoPrestadoSelecionado(ServicoPrestado)
    End Sub

    Private Sub ExibaServicoPrestadoSelecionado(ByVal ServicoPrestado As IServicoPrestado)
        txtValorDoServico.Value = ServicoPrestado.Valor
        Session(CHAVE_SERVICO_PRESTADO_SELECIONADO) = ServicoPrestado
    End Sub

    Private Sub btnAdicionarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarItem.Click
        Dim ItemAlancar As IItemDeLancamento
        Dim LancamentoDeServicos As ILacamentoDeServicosPrestados
        Dim Inconsistencias As String

        Inconsistencias = ValidaInsercaoDeItensDoLancamento()

        If Not String.IsNullOrEmpty(Inconsistencias) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencias), False)
            Exit Sub
        End If

        LancamentoDeServicos = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)
        LancamentoDeServicos.Aliquota = txtAliquota.Text
        ItemAlancar = CriaItemDeLancamento()
        LancamentoDeServicos.AdicionaItemDeLancamento(ItemAlancar)
        ExibaItensDeLancamento(LancamentoDeServicos.ObtenhaItensDeLancamento, LancamentoDeServicos.ObtenhaTotalDosItensLancados, LancamentoDeServicos.ObtenhaValorDoISSQN)
        Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = LancamentoDeServicos
        LimpaCamposDeLancamento()
    End Sub

    Private Function CriaItemDeLancamento() As IItemDeLancamento
        Dim ItemDeLancamento As IItemDeLancamento

        ItemDeLancamento = FabricaGenerica.GetInstancia.CrieObjeto(Of IItemDeLancamento)()
        ItemDeLancamento.Servico = CType(Session(CHAVE_SERVICO_PRESTADO_SELECIONADO), IServicoPrestado)
        If Not String.IsNullOrEmpty(txtQuantidade.Text) Then ItemDeLancamento.Quantidade = CShort(txtQuantidade.Text)
        ItemDeLancamento.Unidade = txtUnidade.Text
        ItemDeLancamento.Valor = txtValorDoServico.Value.Value
        ItemDeLancamento.Observacao = txtObservacaoItem.Text

        Return ItemDeLancamento
    End Function

    Private Sub LimpaCamposDeLancamento()
        txtQuantidade.Text = ""
        txtUnidade.Text = ""
        txtValorDoServico.Text = ""
        cboServicosPrestados.Text = ""
        txtObservacaoItem.Text = ""
        Session(CHAVE_SERVICO_PRESTADO_SELECIONADO) = Nothing
    End Sub

    Private Sub cboLancamentos_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboLancamentos.SelectedIndexChanged
        Dim Lancamento As ILacamentoDeServicosPrestados
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue

        Using ServicoDeLancamento As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            Lancamento = ServicoDeLancamento.ObtenhaLancamento(CLng(Valor))
        End Using

        Me.ExibaTelaConsultar()
        ExibaLancamento(Lancamento)
    End Sub

    Private Sub ExibaLancamento(ByVal Lancamento As ILacamentoDeServicosPrestados)
        txtNumero.Text = Lancamento.Numero.ToString
        txtAliquota.Text = Lancamento.Aliquota
        txtDataDoLancamento.SelectedDate = Lancamento.DataDeLancamento
        txtNaturezaDaOperacao.Text = Lancamento.NaturezaDaOperacao
        txtObservacoes.Text = Lancamento.Observacoes
        ExibaItensDeLancamento(Lancamento.ObtenhaItensDeLancamento, Lancamento.ObtenhaTotalDosItensLancados, Lancamento.ObtenhaValorDoISSQN)
        Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = Lancamento
    End Sub

    Private Sub ExibaItensDeLancamento(ByVal Itens As IList(Of IItemDeLancamento), ByVal ValorTotal As Double, ByVal ValorISSQN As Double)
        grdItensLancados.DataSource = Itens
        grdItensLancados.DataBind()
        lblValorTotal.Text = ValorTotal.ToString
        lblISSQN.Text = ValorISSQN.ToString
    End Sub

    Private Sub grdItensLancados_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdItensLancados.ItemCommand
        If e.CommandName = "Excluir" Then

            Dim IndiceSelecionado As Integer = e.Item().ItemIndex
            Dim Lancamento As ILacamentoDeServicosPrestados

            Lancamento = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)
            Lancamento.ObtenhaItensDeLancamento().RemoveAt(IndiceSelecionado)
            ExibaItensDeLancamento(Lancamento.ObtenhaItensDeLancamento, Lancamento.ObtenhaTotalDosItensLancados, Lancamento.ObtenhaValorDoISSQN)
            Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = Lancamento
        End If
    End Sub

    Private Function ValidaInsercaoDeItensDoLancamento() As String
        If String.IsNullOrEmpty(txtAliquota.Text) Then Return "Alíquota deve ser informada."
        If Session(CHAVE_SERVICO_PRESTADO_SELECIONADO) Is Nothing Then Return "O serviço prestado deve ser informado."
        If String.IsNullOrEmpty(txtValorDoServico.Text) Then Return "O valor do serviço deve ser informado."
        Return Nothing
    End Function

    Private Function ValidaLancamento() As String
        If String.IsNullOrEmpty(txtNumero.Text) Then Return "Número do lançamento deve ser informado."
        If txtDataDoLancamento.SelectedDate Is Nothing Then Return "Data do lançamento deve ser informada."
        If String.IsNullOrEmpty(txtAliquota.Text) Then Return "Alíquota deve ser informada."

        Dim Lancamento As ILacamentoDeServicosPrestados

        Lancamento = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)

        If Lancamento.ObtenhaItensDeLancamento.Count = 0 Then Return "Pelo menos um serviço deve ser lançado."

        Return Nothing
    End Function

    Private Sub btnReaproveitar_Click()
        Dim LancamentoAtual As ILacamentoDeServicosPrestados
        Dim LancamentoClone As ILacamentoDeServicosPrestados

        LancamentoAtual = CType(Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS), ILacamentoDeServicosPrestados)
        LancamentoClone = CType(LancamentoAtual.Clone, ILacamentoDeServicosPrestados)
        ExibaTelaNovo()
        ExibaLancamento(LancamentoClone)
        Session(CHAVE_LANCAMENTO_SERVICOS_PRESTADOS) = LancamentoClone
        txtDataDoLancamento.SelectedDate = LancamentoClone.DataDeLancamento
        Using Servico As IServicoDeLancamentoDeServicos = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeLancamentoDeServicos)()
            txtNumero.Text = Servico.ObtenhaProximoNumeroDisponivel.ToString
        End Using
    End Sub

End Class