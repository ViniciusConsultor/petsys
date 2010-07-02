Imports Telerik.Web.UI

Class AppointmentInfo
    Private _id As String
    Private _subject As String
    Private _start As DateTime
    Private _end As DateTime
    Private _recurrenceRule As String
    Private _recurrenceParentId As String
    Private _userID As System.Nullable(Of Integer)

    Public ReadOnly Property ID() As String
        Get
            Return _id
        End Get
    End Property

    Public Property Subject() As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property Start() As DateTime
        Get
            Return _start
        End Get
        Set(ByVal value As DateTime)
            _start = value
        End Set
    End Property

    Public Property [End]() As DateTime
        Get
            Return _end
        End Get
        Set(ByVal value As DateTime)
            _end = value
        End Set
    End Property

    Public Property RecurrenceRule() As String
        Get
            Return _recurrenceRule
        End Get
        Set(ByVal value As String)
            _recurrenceRule = value
        End Set
    End Property

    Public Property RecurrenceParentID() As String
        Get
            Return _recurrenceParentId
        End Get
        Set(ByVal value As String)
            _recurrenceParentId = value
        End Set
    End Property

    Public Property UserID() As System.Nullable(Of Integer)
        Get
            Return _userID
        End Get
        Set(ByVal value As System.Nullable(Of Integer))
            _userID = value
        End Set
    End Property

    Private Sub New()
        Me._id = Guid.NewGuid().ToString()
    End Sub

    Public Sub New(ByVal subject As String, ByVal start As DateTime, ByVal [end] As DateTime, ByVal recurrenceRule As String, ByVal recurrenceParentID As String, ByVal userID As System.Nullable(Of Integer))
        Me.New()
        Me._subject = subject
        Me._start = start
        Me._end = [end]
        Me._recurrenceRule = recurrenceRule
        Me._recurrenceParentId = recurrenceParentID
        Me._userID = userID
    End Sub

    Public Sub New(ByVal source As Appointment)
        Me.New()
        CopyInfo(source)
    End Sub

    Public Sub CopyInfo(ByVal source As Appointment)
        Subject = source.Subject
        Start = source.Start
        [End] = source.[End]
        RecurrenceRule = source.RecurrenceRule
        If source.RecurrenceParentID IsNot Nothing Then
            RecurrenceParentID = source.RecurrenceParentID.ToString()
        End If

        Dim user As Resource = source.Resources.GetResourceByType("User")
        If user IsNot Nothing Then
            UserID = DirectCast(user.Key, System.Nullable(Of Integer))
        Else
            UserID = Nothing
        End If
    End Sub
End Class

Partial Public Class ctrlAgenda
    Inherits System.Web.UI.UserControl


    Private Const AppointmentsKey As String = "Telerik.Web.Examples.Scheduler.BindToList.VB.Apts"

    Private ReadOnly Property Appointments() As List(Of AppointmentInfo)
        Get
            Dim sessApts As List(Of AppointmentInfo) = TryCast(Session(AppointmentsKey), List(Of AppointmentInfo))
            If sessApts Is Nothing Then
                sessApts = New List(Of AppointmentInfo)()
                Session(AppointmentsKey) = sessApts
            End If

            Return sessApts
        End Get
    End Property

    Protected Sub DefaultVB_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Session.Remove(AppointmentsKey)

            ' InitializeResources()
            InitializeAppointments()
        End If

        RadScheduler1.DataSource = Appointments
    End Sub

    Protected Sub RadScheduler1_AppointmentInsert(ByVal sender As Object, ByVal e As SchedulerCancelEventArgs)
        Appointments.Add(New AppointmentInfo(e.Appointment))
    End Sub

    Protected Sub RadScheduler1_AppointmentUpdate(ByVal sender As Object, ByVal e As AppointmentUpdateEventArgs)
        Dim ai As AppointmentInfo = FindById(e.ModifiedAppointment.ID)
        ai.CopyInfo(e.ModifiedAppointment)
    End Sub

    Protected Sub RadScheduler1_AppointmentDelete(ByVal sender As Object, ByVal e As SchedulerCancelEventArgs)
        Appointments.Remove(FindById(e.Appointment.ID))
    End Sub

    'Private Sub InitializeResources()
    '    Dim resType As New ResourceType("User")
    '    resType.ForeignKeyField = "UserID"

    '    RadScheduler1.ResourceTypes.Add(resType)
    '    RadScheduler1.Resources.Add(New Resource("User", 1, "Alex"))
    '    RadScheduler1.Resources.Add(New Resource("User", 2, "Bob"))
    '    RadScheduler1.Resources.Add(New Resource("User", 3, "Charlie"))
    'End Sub

    Private Sub InitializeAppointments()
        Dim start As DateTime = DateTime.UtcNow.[Date]
        start = start.AddHours(6)
        Appointments.Add(New AppointmentInfo("Take the car to the service", start, start.AddHours(1), String.Empty, Nothing, 1))
        Appointments.Add(New AppointmentInfo("Meeting with Alex", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 2))

        start = start.AddDays(-1)
        Dim dayStart As DateTime = RadScheduler1.UtcDayStart(start)
        Appointments.Add(New AppointmentInfo("Bob's Birthday", dayStart, dayStart.AddDays(1), String.Empty, Nothing, 1))
        Appointments.Add(New AppointmentInfo("Call Charlie about the Project", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 2))

        start = start.AddDays(2)
        Appointments.Add(New AppointmentInfo("Get the car from the service", start.AddHours(2), start.AddHours(3), String.Empty, Nothing, 1))
    End Sub

    Private Function FindById(ByVal ID As Object) As AppointmentInfo
        For Each ai As AppointmentInfo In Appointments
            If ai.ID.Equals(ID) Then
                Return ai
            End If
        Next

        Return Nothing
    End Function
End Class