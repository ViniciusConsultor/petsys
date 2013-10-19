<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlDespachoDePatentes.ascx.cs" Inherits="MP.Client.MP.ctrlDespachoDePatentes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboDespachoDePatentes" runat="server" EmptyMessage="Selecione um despacho"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboDespachoDePatentes_ItemsRequested"
    OnSelectedIndexChanged="cboDespachoDePatentes_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="15%">
                    Despacho
                </td>
                <td width="81%">
                    Situação do processo
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="15%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="61%">
                    <%#DataBinder.Eval(Container, "Attributes['SituacaoProcessoDePatentes']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.MP.004.0001" OnClick="btnNovo_OnClick" />
