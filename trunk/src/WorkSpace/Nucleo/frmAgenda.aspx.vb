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

            lblInconsistencia.Text = "Não existe agenda configurada para esta pessoa."
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Não existe agenda configurada para esta pessoa."), False)
            Exit Sub
        End If

        lblInconsistencia.Visible = False
        pnlCompromissos.Visible = True
        pnlTarefas.Visible = True

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

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CarregaAgenda()
    End Sub

    Private Const CHAVE_COMPROMISSOS As String = "CHAVE_COMPROMISSOS"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO"
    Private Const CHAVE_TAREFAS As String = "CHAVE_TAREFAS"
    Private URL As String

    Public Property HoraInicio() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAINICIO"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_HORAINICIO") = value
        End Set
    End Property

    Public Property HoraFim() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAFIM"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_HORAFIM") = value
        End Set
    End Property

    Public Property IntervaloEntreCompromissos() As Date
        Get
            Return CDate(ViewState("CHAVE_INTERVALOENTRECOMPROMISSOS"))
        End Get
        Set(ByVal value As Date)
            ViewState("CHAVE_INTERVALOENTRECOMPROMISSOS") = value
        End Set
    End Property

    Public Property IDProprietario() As Long
        Get
            Return CLng(ViewState(CHAVE_ID_PROPRIETARIO))
        End Get
        Set(ByVal value As Long)
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

    Public Sub CarregaAgenda()
        UtilidadesWeb.LimparComponente(CType(pnlCompromissos, Control))
        UtilidadesWeb.LimparComponente(CType(pnlTarefas, Control))

        Session.Remove(CHAVE_COMPROMISSOS)
        Session.Remove(CHAVE_TAREFAS)

        Dim Tarefas As IList(Of ITarefa)
        Dim Compromissos As IList(Of ICompromisso)

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromissos = Servico.ObtenhaCompromissos(IDProprietario)
            Tarefas = Servico.ObtenhaTarefas(IDProprietario)
        End Using

        schCompromissos.DayStartTime = HoraInicio.TimeOfDay
        schCompromissos.DayEndTime = HoraFim.TimeOfDay
        schCompromissos.MinutesPerRow = IntervaloEntreCompromissos.Minute

        Session(CHAVE_COMPROMISSOS) = Compromissos
        schCompromissos.DataSource = Compromissos
        schCompromissos.DataBind()

        ExibaTarefas(Tarefas)
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
        Session(CHAVE_TAREFAS) = Tarefas
        grdTarefas.DataSource = Tarefas
        grdTarefas.DataBind()
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
            Tarefas = CType(Session((CHAVE_TAREFAS)), IList(Of ITarefa))
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

    Private Sub grdTarefas_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdTarefas.PageIndexChanged
        UtilidadesWeb.PaginacaoDataGrid(grdTarefas, Session(CHAVE_TAREFAS), e)
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
        CType(ToolBarCompromisso.FindButtonByCommandName("btnNovaTarefa"), RadToolBarButton).Visible = Principal.EstaAutorizado("OPE.NCL.012.0004")

        'Coluna com botão remover para tarefas
        grdTarefas.Columns(0).Visible = Principal.EstaAutorizado("OPE.NCL.012.0005")

        'Coluna com botão modificar para tarefas
        grdTarefas.Columns(1).Visible = Principal.EstaAutorizado("OPE.NCL.012.0006")

        'Permitido visualizar agenda de outras pessoas
        pnlProprietario.Visible = Principal.EstaAutorizado("OPE.NCL.012.0007")
    End Sub

    Private Sub ToolBarTarefa_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles ToolBarTarefa.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovaTarefa"
                btnNovaTarefa_Click()
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

    Private Sub btnImprimirCompromisso_Click()
        Dim URL As String

        URL = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual, "Nucleo/frmImpressaoCompromisso.aspx", "?IdProprietario=", IDProprietario.ToString)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir compromisso"), False)
    End Sub

End Class