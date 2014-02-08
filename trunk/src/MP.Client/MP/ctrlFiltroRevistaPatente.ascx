<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlFiltroRevistaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlFiltroRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboFiltroPatente" runat="server" EmptyMessage="Selecione uma operação para o filtro"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" Height="100px">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="6%">
                    Código
                </td>
                <td width="90%">
                    Descrição
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="10%">
                    <%#DataBinder.Eval(Container, "Attributes['Codigo']")%>
                </td>
                <td width="90%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>

