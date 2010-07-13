Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

'Class AppointmentInfo
'    Private _id As String
'    Private _subject As String
'    Private _start As DateTime
'    Private _end As DateTime
'    Private _recurrenceRule As String
'    Private _recurrenceParentId As String
'    Private _userID As System.Nullable(Of Integer)

'    Public ReadOnly Property ID() As String
'        Get
'            Return _id
'        End Get
'    End Property

'    Public Property Subject() As String
'        Get
'            Return _subject
'        End Get
'        Set(ByVal value As String)
'            _subject = value
'        End Set
'    End Property

'    Public Property Start() As DateTime
'        Get
'            Return _start
'        End Get
'        Set(ByVal value As DateTime)
'            _start = value
'        End Set
'    End Property

'    Public Property [End]() As DateTime
'        Get
'            Return _end
'        End Get
'        Set(ByVal value As DateTime)
'            _end = value
'        End Set
'    End Property

'    Public Property RecurrenceRule() As String
'        Get
'            Return _recurrenceRule
'        End Get
'        Set(ByVal value As String)
'            _recurrenceRule = value
'        End Set
'    End Property

'    Public Property RecurrenceParentID() As String
'        Get
'            Return _recurrenceParentId
'        End Get
'        Set(ByVal value As String)
'            _recurrenceParentId = value
'        End Set
'    End Property

'    Public Property UserID() As System.Nullable(Of Integer)
'        Get
'            Return _userID
'        End Get
'        Set(ByVal value As System.Nullable(Of Integer))
'            _userID = value
'        End Set
'    End Property

'    Private Sub New()
'        Me._id = Guid.NewGuid().ToString()
'    End Sub

'    Public Sub New(ByVal subject As String, ByVal start As DateTime, ByVal [end] As DateTime, ByVal recurrenceRule As String, ByVal recurrenceParentID As String, ByVal userID As System.Nullable(Of Integer))
'        Me.New()
'        Me._subject = subject
'        Me._start = start
'        Me._end = [end]
'        Me._recurrenceRule = recurrenceRule
'        Me._recurrenceParentId = recurrenceParentID
'        Me._userID = userID
'    End Sub

'    Public Sub New(ByVal source As Appointment)
'        Me.New()
'        CopyInfo(source)
'    End Sub

'    Public Sub CopyInfo(ByVal source As Appointment)
'        Subject = source.Subject
'        Start = source.Start
'        [End] = source.[End]
'        RecurrenceRule = source.RecurrenceRule
'        If source.RecurrenceParentID IsNot Nothing Then
'            RecurrenceParentID = source.RecurrenceParentID.ToString()
'        End If

'        Dim user As Resource = source.Resources.GetResourceByType("User")
'        If user IsNot Nothing Then
'            UserID = DirectCast(user.Key, System.Nullable(Of Integer))
'        Else
'            UserID = Nothing
'        End If
'    End Sub
'End Class

Partial Public Class ctrlAgenda
    Inherits System.Web.UI.UserControl


    Private Const CHAVE_COMPROMISSOS As String = "CHAVE_COMPROMISSOS"
    Private Const CHAVE_ID_PROPRIETARIO As String = "CHAVE_ID_PROPRIETARIO"

    'Private ReadOnly Property Compromissos() As IList(Of ICompromisso)
    '    Get
    '        Dim sessApts As List(Of AppointmentInfo) = TryCast(Session(AppointmentsKey), List(Of AppointmentInfo))
    '        If sessApts Is Nothing Then
    '            sessApts = New List(Of AppointmentInfo)()
    '            Session(AppointmentsKey) = sessApts
    '        End If

    '        Return sessApts
    '    End Get
    'End Property

    'Protected Sub RadScheduler1_AppointmentInsert(ByVal sender As Object, ByVal e As SchedulerCancelEventArgs)
    '    Appointments.Add(New AppointmentInfo(e.Appointment))
    'End Sub

    'Protected Sub RadScheduler1_AppointmentUpdate(ByVal sender As Object, ByVal e As AppointmentUpdateEventArgs)
    '    Dim ai As AppointmentInfo = FindById(e.ModifiedAppointment.ID)
    '    ai.CopyInfo(e.ModifiedAppointment)
    'End Sub

    'Protected Sub RadScheduler1_AppointmentDelete(ByVal sender As Object, ByVal e As SchedulerCancelEventArgs)
    '    Appointments.Remove(FindById(e.Appointment.ID))
    'End Sub

    'Private Sub InitializeAppointments()
    '    Dim start As DateTime = DateTime.UtcNow.[Date]
    '    start = start.AddHours(6)
    '    Appointments.Add(New AppointmentInfo("Take the car to the service", start, start.AddHours(1), String.Empty, Nothing, 1))
    '    Appointments.Add(New AppointmentInfo("Meeting with Alex", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 2))

    '    start = start.AddDays(-1)
    '    Dim dayStart As DateTime = RadScheduler1.UtcDayStart(start)
    '    Appointments.Add(New AppointmentInfo("Bob's Birthday", dayStart, dayStart.AddDays(1), String.Empty, Nothing, 1))
    '    Appointments.Add(New AppointmentInfo("Call Charlie about the Project", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 2))

    '    start = start.AddDays(2)
    '    Appointments.Add(New AppointmentInfo("Get the car from the service", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 1))
    'End Sub

    'Private Function FindById(ByVal ID As Object) As AppointmentInfo
    '    For Each ai As AppointmentInfo In Appointments
    '        If ai.ID.Equals(ID) Then
    '            Return ai
    '        End If
    '    Next

    '    Return Nothing
    'End Function

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

            UtilidadesWeb.LimparComponente(CType(grdTarefas, Control))
            grdTarefas.DataSource = New List(Of ITarefa)
            grdTarefas.DataBind()
            Exit Sub
        End If
    End Sub

    Public Sub CarregaAgenda()
        Session.Remove(CHAVE_COMPROMISSOS)

        Dim Compromissos As IList(Of ICompromisso)

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Compromissos = Servico.ObtenhaCompromissos(IDProprietario)
        End Using

        Session(CHAVE_COMPROMISSOS) = Compromissos
        schCompromissos.DataSource = Compromissos
        schCompromissos.DataBind()
    End Sub
End Class