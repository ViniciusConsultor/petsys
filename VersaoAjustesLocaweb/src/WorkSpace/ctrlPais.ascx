<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPais.ascx.vb" Inherits="WorkSpace.ctrlPais" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlPais" runat="server">
    <telerik:RadComboBox ID="cboPais" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
        LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
        AllowCustomText="True" HighlightTemplatedItems="True" Width="90%" Skin="Vista"
        CausesValidation="False" EmptyMessage="Selecione um país" MaxLength="100">
        <HeaderTemplate>
            <table style="width: 96%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 70%;">
                        País
                    </td>
                    <td style="width: 30%;">
                        Sigla
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 70%;">
                        <%# DataBinder.Eval(Container, "Text")%>
                    </td>
                    <td style="width: 30%;">
                        <%#DataBinder.Eval(Container, "Attributes['Sigla']")%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </telerik:RadComboBox>
</asp:Panel>
