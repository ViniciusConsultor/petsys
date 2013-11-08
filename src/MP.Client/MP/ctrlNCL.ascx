<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlNCL.ascx.cs" Inherits="MP.Client.MP.ctrlNCL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboNCL" runat="server" EmptyMessage="Selecione uma NCL"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="6%">
                    Código
                </td>
                <td width="60%">
                    Descrição
                </td>
                <td width="30%">
                    Natureza
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="10%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="60%">
                    <%#DataBinder.Eval(Container, "Attributes['Descricao']")%>
                </td>
                  <td width="30%">
                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
