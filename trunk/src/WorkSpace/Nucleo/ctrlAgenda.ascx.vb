Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Partial Public Class ctrlAgenda
    Inherits System.Web.UI.UserControl

    Private Const CHAVE_COMPROMISSOS As String = "CHAVE_COMPROMISSOS"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO"
    Private Const CHAVE_TAREFAS As String = "CHAVE_TAREFAS"

    Public Property HoraInicio() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAINICIO"))
        End Get
        Set(ByVal value As Date)
            schCompromissos.WorkDayStartTime = value.TimeOfDay
            ViewState("CHAVE_HORAINICIO") = value
        End Set
    End Property

    Public Property HoraFim() As Date
        Get
            Return CDate(ViewState("CHAVE_HORAFIM"))
        End Get
        Set(ByVal value As Date)
            schCompromissos.WorkDayEndTime = value.TimeOfDay
            ViewState("CHAVE_HORAFIM") = value
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

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovaTarefa"
                Call btnNovaTarefa_Click()
        End Select
    End Sub

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

    Private Sub RadToolBar1_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles RadToolBar1.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovoCompromisso"
                Call btnNovoCompromisso_Click()
        End Select
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregaAgenda()
        End If
    End Sub

    Public Sub CarregaAgenda()
        Session.Remove(CHAVE_COMPROMISSOS)
        Session.Remove(CHAVE_TAREFAS)

        Dim Tarefas As IList(Of ITarefa)
        Dim Compromissos As IList(Of ICompromisso)

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromissos = Servico.ObtenhaCompromissos(IDProprietario)
            Tarefas = Servico.ObtenhaTarefas(IDProprietario)
        End Using

        Session(CHAVE_COMPROMISSOS) = Compromissos
        schCompromissos.DataSource = Compromissos
        schCompromissos.DataBind()

        Session(CHAVE_TAREFAS) = Tarefas

        grdTarefas.DataSource = Tarefas
        grdTarefas.DataBind()
    End Sub

    Private Sub schCompromissos_AppointmentClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerEventArgs) Handles schCompromissos.AppointmentClick
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdCompromisso.aspx")
        URL = String.Concat(URL, "?IdProprietario=", IDProprietario.ToString)
        URL = String.Concat(URL, "?IdCompromisso=", IDProprietario.ToString)

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de compromissos"), False)
    End Sub

    Private Sub schCompromissos_AppointmentCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentCommandEventArgs) Handles schCompromissos.AppointmentCommand

    End Sub

    Private Sub schCompromissos_AppointmentContextMenuItemClicked(ByVal sender As Object, ByVal e As Telerik.Web.UI.AppointmentContextMenuItemClickedEventArgs) Handles schCompromissos.AppointmentContextMenuItemClicked

    End Sub

    Private Sub schCompromissos_AppointmentDelete(ByVal sender As Object, ByVal e As Telerik.Web.UI.SchedulerCancelEventArgs) Handles schCompromissos.AppointmentDelete
        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Servico.RemovaCompromisso(CLng(e.Appointment.ID))
        End Using

        CarregaAgenda()
    End Sub
End Class