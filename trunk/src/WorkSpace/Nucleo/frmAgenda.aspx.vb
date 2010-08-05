Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Partial Public Class frmAgenda
    Inherits SuperPagina
    Implements ICallbackEventHandler

    Private Sub ExibaAgendaDaPessoa(ByVal Pessoa As IPessoa)
        Dim Agenda As IAgenda

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Agenda = Servico.ObtenhaAgenda(Pessoa.ID.Value)
        End Using

        lblInconsistencia.Visible = True

        If Agenda Is Nothing Then
            pnlCompromissos.Visible = False
            pnlTarefas.Visible = False
            pnlLembretes.Visible = False

            lblInconsistencia.Text = "Não existe agenda configurada para esta pessoa."
            Exit Sub
        End If

        lblInconsistencia.Visible = False
        pnlCompromissos.Visible = True
        pnlTarefas.Visible = True
        pnlLembretes.Visible = True

        Me.IDProprietario = Pessoa.ID.Value
        ConfiguraAgenda(Agenda)
        CarregaAgenda()
    End Sub

    Private Sub ConfiguraAgenda(ByVal Agenda As IAgenda)

        Me.HoraInicio = Agenda.HorarioDeInicio
        Me.HoraFim = Agenda.HorarioDeTermino
        Me.IntervaloEntreCompromissos = Agenda.IntervaloEntreOsCompromissos
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.ToolBarPrincipal
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.012"
    End Function

    Private Sub ExibaTelaInicial()
        Dim UsuarioLogado As Usuario
        Dim Pessoa As IPessoaFisica

        UsuarioLogado = FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario
        Pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UsuarioLogado.ID)

        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = False
        ctrlPessoa1.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa1.SetaTipoDePessoaPadrao(TipoDePessoa.Fisica)
        ctrlPessoa1.PessoaSelecionada = Pessoa
        ExibaAgendaDaPessoa(Pessoa)
    End Sub

    Private Const CHAVE_COMPROMISSOS As String = "CHAVE_COMPROMISSOS"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO"
    Private Const CHAVE_TAREFAS As String = "CHAVE_TAREFAS"
    Private Const CHAVE_LEMBRETES As String = "CHAVE_LEMBRETES"
    Private URL As String

    Private Property HoraInicio() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAINICIO"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_HORAINICIO") = value
        End Set
    End Property

    Private Property HoraFim() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAFIM"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_HORAFIM") = value
        End Set
    End Property

    Private Property IntervaloEntreCompromissos() As Date
        Get
            Return CDate(ViewState("CHAVE_INTERVALOENTRECOMPROMISSOS"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_INTERVALOENTRECOMPROMISSOS") = value
        End Set
    End Property

    Private Property IDProprietario() As Nullable(Of Long)
        Get
            Return CLng(ViewState(CHAVE_ID_PROPRIETARIO))
        End Get
        Set(ByVal value As Nullable(Of Long))
            ViewState(CHAVE_ID_PROPRIETARIO) = value
        End Set
    End Property

    Private Sub btnNovoCompromisso_Click()
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdCompromisso.aspx")
        URL = String.Concat(URL, "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de compromissos"), False)
    End Sub

    Private Sub btnNovaTarefa_Click()
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdTarefa.aspx")
        URL = String.Concat(URL, "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de tarefas"), False)
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cbReference As String
        Dim callbackScript As String

        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ExibaAgendaDaPessoa

        cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context", True)
        callbackScript = String.Concat("function CallServer(arg,context) { ", cbReference, ";}")
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

        If Not IsPostBack Then
            ExibaTelaInicial()
        End If
    End Sub

    Private Sub CarregaAgenda()
        If Not Me.IDProprietario.HasValue Then Exit Sub

        UtilidadesWeb.LimparComponente(CType(pnlCompromissos, Control))
        UtilidadesWeb.LimparComponente(CType(pnlTarefas, Control))
        UtilidadesWeb.LimparComponente(CType(pnlLembretes, Control))

        ViewState.Remove(CHAVE_COMPROMISSOS)
        ViewState.Remove(CHAVE_TAREFAS)
        ViewState.Remove(CHAVE_LEMBRETES)

        Dim Tarefas As IList(Of ITarefa)
        Dim Compromissos As IList(Of ICompromisso)
        Dim Lembretes As IList(Of ILembrete)

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromissos = Servico.ObtenhaCompromissos(IDProprietario.Value)
            Tarefas = Servico.ObtenhaTarefas(IDProprietario.Value)
            Lembretes = Servico.ObtenhaLembretes(IDProprietario.Value)
        End Using

        schCompromissos.DayStartTime = HoraInicio.TimeOfDay
        schCompromissos.DayEndTime = HoraFim.TimeOfDay
        schCompromissos.MinutesPerRow = IntervaloEntreCompromissos.Minute

        ViewState(CHAVE_COMPROMISSOS) = Compromissos
        schCompromissos.DataSource = Compromissos
        schCompromissos.DataBind()

        ExibaTarefas(Tarefas)
        ExibaLembretes(Lembretes)
    End Sub

    Private Sub schCompromissos_AppointmentDelete(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerCancelEventArgs) Handles schCompromissos.AppointmentDelete
        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Servico.RemovaCompromisso(CLng(e.Appointment.ID))
        End Using

        CarregaAgenda()
    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Dim InformacoesClient As String = String.Concat(URL, ",", "Cadastro de compromissos,", Guid.NewGuid.ToString)
        Return InformacoesClient
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim ID As String = eventArgument

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdCompromisso.aspx")
        URL = String.Concat(URL, "?IdProprietario=", IDProprietario.ToString)
        URL = String.Concat(URL, "&IdCompromisso=", ID)
    End Sub

    Private Sub ExibaTarefas(ByVal Tarefas As IList(Of ITarefa))
        ViewState(CHAVE_TAREFAS) = Tarefas
        grdTarefas.DataSource = Tarefas
        grdTarefas.DataBind()
    End Sub

    Private Sub ExibaLembretes(ByVal Lembretes As IList(Of ILembrete))
        ViewState(CHAVE_LEMBRETES) = Lembretes
        grdLembretes.DataSource = Lembretes
        grdLembretes.DataBind()
    End Sub

    Private Sub grdTarefas_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdTarefas.ItemCommand
        Dim ID As Long
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            ID = CLng(e.Item.Cells(4).Text)
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Tarefas As IList(Of ITarefa)
            Tarefas = CType(ViewState((CHAVE_TAREFAS)), IList(Of ITarefa))
            Tarefas.RemoveAt(IndiceSelecionado)
            ExibaTarefas(Tarefas)

            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                Servico.RemovaTarefa(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/cdTarefa.aspx", "?IdTarefa=", ID, "&IdProprietario=", IDProprietario.ToString)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastrar tarefa"), False)
        End If
    End Sub

    Private Sub grdTarefas_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdTarefas.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdTarefas.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub grdTarefas_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdTarefas.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdTarefas, ViewState(CHAVE_TAREFAS), e)
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        'Permitido inserir compromissos
        CType(ToolBarCompromisso.FindButtonByCommandName("btnNovoCompromisso"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0001")

        'Permitido excluir compromissos
        schCompromissos.AllowDelete = Principal.EstaAutorizado("OPE.NCL.012.0002")

        'Permitido Imprimir compromisso
        CType(ToolBarCompromisso.FindButtonByCommandName("btnImprimirCompromisso"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0008")

        'Permitido inserir tarefas
        CType(ToolBarTarefa.FindButtonByCommandName("btnNovaTarefa"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0004")

        'Coluna com botão remover para tarefas
        grdTarefas.Columns(0).Visible = Principal.EstaAutorizado("OPE.NCL.012.0005")

        'Coluna com botão modificar para tarefas
        grdTarefas.Columns(1).Visible = Principal.EstaAutorizado("OPE.NCL.012.0006")

        'Permitido visualizar agenda de outras pessoas
        pnlProprietario.Visible = Principal.EstaAutorizado("OPE.NCL.012.0007")

        'Permitido inserir lembretes
        CType(ToolBarLembretes.FindButtonByCommandName("btnNovoLembrete"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0010")

        'Permitido imprimir lembretes
        CType(ToolBarLembretes.FindButtonByCommandName("btnImprimirLembretes"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0013")
    End Sub

    Private Sub ToolBarTarefa_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles ToolBarTarefa.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovaTarefa"
                btnNovaTarefa_Click()
            Case "btnImprimirTarefas"
                btnImprimirTarefas_Click()
        End Select
    End Sub

    Private Sub ToolBarCompromisso_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles ToolBarCompromisso.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovoCompromisso"
                btnNovoCompromisso_Click()
            Case "btnImprimirCompromisso"
                btnImprimirCompromisso_Click()
        End Select
    End Sub

    Private Sub btnImprimirTarefas_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/frmImpressaoTarefa.aspx", "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir tarefas"), False)
    End Sub

    Private Sub btnImprimirCompromisso_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/frmImpressaoCompromisso.aspx", "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir compromissos"), False)
    End Sub

    Private Sub btnImprimirLembretes_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/frmImpressaoLembrete.aspx", "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir lembretes"), False)
    End Sub

    Private Sub ToolBarLembretes_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles ToolBarLembretes.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovoLembrete"
                btnNovoLembrete_Click()
            Case "btnImprimirLembretes"
                btnImprimirLembretes_Click()
        End Select
    End Sub

    Private Sub btnNovoLembrete_Click()
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdLembrete.aspx")
        URL = String.Concat(URL, "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de lembretes"), False)
    End Sub

    Private Sub grdLembretes_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdLembretes.ItemCommand
        Dim ID As Long
        Dim IndiceSelecionado As Integer

        If Not e.CommandName = "Page" AndAlso Not e.CommandName = "ChangePageSize" Then
            ID = CLng(e.Item.Cells(4).Text)
            IndiceSelecionado = e.Item().ItemIndex
        End If

        If e.CommandName = "Excluir" Then
            Dim Lembretes As IList(Of ILembrete)

            Lembretes = CType(ViewState((CHAVE_LEMBRETES)), IList(Of ILembrete))
            Lembretes.RemoveAt(IndiceSelecionado)
            ExibaLembretes(Lembretes)

            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                Servico.RemovaLembrete(ID)
            End Using
        ElseIf e.CommandName = "Modificar" Then
            Dim URL As String

            URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/cdLembrete.aspx", "?IdLembrete=", ID, "&IdProprietario=", IDProprietario.ToString)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastrar lembrete"), False)
        End If

    End Sub

    Private Sub grdLembretes_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdLembretes.ItemCreated
        If (TypeOf e.Item Is GridDataItem) Then
            Dim gridItem As GridDataItem = CType(e.Item, GridDataItem)

            For Each column As GridColumn In grdLembretes.MasterTableView.RenderColumns
                If (TypeOf column Is GridButtonColumn) Then
                    gridItem(column.UniqueName).ToolTip = column.HeaderTooltip
                End If
            Next
        End If
    End Sub

    Private Sub grdLembretes_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdLembretes.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdTarefas, ViewState(CHAVE_LEMBRETES), e)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CarregaAgenda()
    End Sub

End Class