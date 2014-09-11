<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlTipoLacamentoFinanceiroRecebimento.ascx.cs" Inherits="FN.Client.FN.ctrlTipoLacamentoFinanceiroRecebimento" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboTipoLacamento" runat="server" EmptyMessage="Selecione um tipo"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="50%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnSelectedIndexChanged="cboTipoLacamento_SelectedIndexChanged">
</telerik:RadComboBox>