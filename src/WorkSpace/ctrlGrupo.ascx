<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGrupo.ascx.vb"
    Inherits="WorkSpace.ctrlGrupo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlGrupo" runat="server">
    <telerik:RadComboBox ID="cboGrupos" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
        AllowCustomText="True" HighlightTemplatedItems="True"  Width="90%" Skin="Vista"
        CausesValidation="False" EmptyMessage="Selecione um grupo" MaxLength="30">
    </telerik:RadComboBox>
   
</asp:Panel>
