<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGrupoDeAtividade.ascx.vb" Inherits="WorkSpace.ctrlGrupoDeAtividade" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlGrupo" runat="server">
    <telerik:RadComboBox ID="cboGrupos" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
        AllowCustomText="True" HighlightTemplatedItems="True" Width="400px" Skin="Vista"
        CausesValidation="False" EmptyMessage="Selecione um grupo de atividade" MaxLength="255">
    </telerik:RadComboBox>
   
</asp:Panel>