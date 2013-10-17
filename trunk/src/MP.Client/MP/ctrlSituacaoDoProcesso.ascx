<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlSituacaoDoProcesso.ascx.cs"
    Inherits="MP.Client.MP.ctrlSituacaoDoProcesso" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboSituacaoDoProcesso" runat="server" EmptyMessage="Selecione um tipo de situação"
    Width="90%" Skin="Vista" CausesValidation="False">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="16%">
                    Código
                </td>
                <td width="80%">
                    Descrição da situação
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="16%">
                    <%#DataBinder.Eval(Container, "Attributes['Codigo']")%>
                </td>
                <td width="80%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
