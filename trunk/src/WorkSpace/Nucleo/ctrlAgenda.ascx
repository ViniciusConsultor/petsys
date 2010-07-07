<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAgenda.ascx.vb"
    Inherits="WorkSpace.ctrlAgenda" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadScheduler runat="server" ID="RadScheduler1" Width="100%" DayStartTime="08:00:00"
    DayEndTime="18:00:00" TimeZoneOffset="03:00:00" OnAppointmentInsert="RadScheduler1_AppointmentInsert"
    OnAppointmentUpdate="RadScheduler1_AppointmentUpdate" OnAppointmentDelete="RadScheduler1_AppointmentDelete"
    DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End"
    DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentId">
    <AdvancedForm Modal="true" />
    <TimelineView UserSelectable="false" />
    <TimeSlotContextMenuSettings EnableDefault="true" />
    <AppointmentContextMenuSettings EnableDefault="true" />
</telerik:RadScheduler>
