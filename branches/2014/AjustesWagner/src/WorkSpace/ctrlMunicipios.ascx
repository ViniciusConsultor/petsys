<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMunicipios.ascx.vb"
    Inherits="WorkSpace.ctrlMunicipios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlMunicipio" runat="server">
    <telerik:RadComboBox ID="cboMunicipios" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
        LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
        AllowCustomText="True" HighlightTemplatedItems="True"  Width="90%" 
        Skin="Vista" CausesValidation="False" EmptyMessage="Selecione um município" MaxLength="50">
        <HeaderTemplate>
            <table style="width: 96%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 70%;">
                        Município
                    </td>
                    <td style="width: 30%;">
                        Estado
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
                        <%#DataBinder.Eval(Container, "Attributes['UF']")%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </telerik:RadComboBox>
</asp:Panel>