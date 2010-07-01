<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAgenda.ascx.vb"
    Inherits="WorkSpace.ctrlAgenda" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radscheduler id="shlAgenda" runat="server" culture="Portuguese (Brazil)"
    editformtimeformat="HH:mm" firstdayofweek="Monday" skin="Vista">
                        <AdvancedForm Modal="True" TimeFormat="HH:mm" />
                    </telerik:radscheduler>
