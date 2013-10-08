<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlLayoutRevistaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlLayoutRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboLayoutPatente" runat="server" EmptyMessage="Selecione uma layout"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" >
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="10%">
                    Nome Campo
                </td>
                <td width="40%">
                    Descrição Resumida
                </td>
                <td width="16%">
                    Tamanho do campo
                </td>
                <td width="10%">
                    Identifica Registro
                </td>
                <td width="10%">
                    Identifica Processo
                </td>
                <td width="10%">
                    Identifica Colidência
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
                <td width="40%">
                    <%#DataBinder.Eval(Container, "Attributes['Descricao']")%>
                </td>
                  <td width="16%">
                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                </td>
                <td width="10%">
                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                </td>
                <td width="10%">
                    <%#DataBinder.Eval(Container, "Attributes['Descricao']")%>
                </td>
                 <td width="10%">
                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>