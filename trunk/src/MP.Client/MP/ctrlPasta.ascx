<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlPasta.ascx.cs" Inherits="MP.Client.MP.ctrlPasta" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboPasta" runat="server" EmptyMessage="Selecione uma pasta"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboPasta_OnItemsRequested"
    OnSelectedIndexChanged="cboPasta_OnSelectedIndexChanged" MaxLength="100">
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo" 
    CausesValidation="False" CommandArgument="OPE.MP.011.0001" OnClick="btnNovo_OnClick" />