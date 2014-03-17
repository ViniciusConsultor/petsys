<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlFormaRecebimento.ascx.cs" Inherits="FN.Client.FN.ctrlFormaRecebimento" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboFormaDeRecebimento" runat="server" EmptyMessage="Selecione uma forma de recebimento"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="50%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnSelectedIndexChanged="cboFormaDeRecebimento_SelectedIndexChanged">
</telerik:RadComboBox>
