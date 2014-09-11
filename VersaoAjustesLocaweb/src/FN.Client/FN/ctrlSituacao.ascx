<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlSituacao.ascx.cs" Inherits="FN.Client.FN.ctrlSituacao" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboSituacao" runat="server" EmptyMessage="Selecione uma situação"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="50%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnSelectedIndexChanged="cboSituacao_SelectedIndexChanged">
</telerik:RadComboBox>
