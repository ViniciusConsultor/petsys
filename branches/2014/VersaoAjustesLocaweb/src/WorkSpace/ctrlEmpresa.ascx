<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEmpresa.ascx.vb"
    Inherits="WorkSpace.ctrlEmpresa" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlEmpresa" runat="server">
    <telerik:RadComboBox ID="cboEmpresa" runat="server" EmptyMessage="Selecione uma empresa"
        EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
        ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
        Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
        <HeaderTemplate>
            <table width="96%">
                <tr>
                    <td width="50%">
                        Razão social
                    </td>
                    <td width="50%">
                        Nome fantasia
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table width="100%">
                <tr>
                    <td width="50%">
                        <%# DataBinder.Eval(Container, "Text")%>
                    </td>
                    <td width="50%">
                        <%#DataBinder.Eval(Container, "Attributes['NomeFantasia']")%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </telerik:RadComboBox>
</asp:Panel>
