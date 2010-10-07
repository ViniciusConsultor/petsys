<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMunicipios.ascx.vb"
    Inherits="WorkSpace.ctrlMunicipios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlMunicipio" runat="server">
    <telerik:RadComboBox ID="cboMunicipios" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
        LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
        AllowCustomText="True" HighlightTemplatedItems="True" Width="500px" 
        Skin="Vista" CausesValidation="False" EmptyMessage="Selecione um município" MaxLength="50">
        <HeaderTemplate>
            <table style="width: 450px" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 300px;">
                        Município
                    </td>
                    <td style="width: 150px;">
                        Estado
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table style="width: 450px" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 300px;">
                        <%# DataBinder.Eval(Container, "Text")%>
                    </td>
                    <td style="width: 150px;">
                        <%#DataBinder.Eval(Container, "Attributes['UF']")%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </telerik:RadComboBox>
</asp:Panel>