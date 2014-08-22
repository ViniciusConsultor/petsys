<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlFiltroRevistaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlFiltroRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlUF.ascx" TagName="ctrlUF" TagPrefix="uc4" %>
<table>
    <tr>
        <td class="th3">
            <asp:Label ID="lblCampo" runat="server" Text="Campo:"></asp:Label>
        </td>
        <td class="td">                                                
            <telerik:RadComboBox ID="cboFiltroPatente" runat="server" EmptyMessage="Selecione uma operação para o filtro"
                Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" Height="100px" OnSelectedIndexChanged="cboFiltroPatente_SelectedIndexChanged">
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
        </td>
    </tr>
    <tr>
        <td class="th3">
            <asp:Label ID="lblValor" runat="server" Text="Valor:"></asp:Label>
        </td>
        <td class="td">
            <uc4:ctrlUF ID="ctrlUF" runat="server" Visible="false" />
            <asp:TextBox ID="txtValor" runat="server" Width="80%" />
        </td>
    </tr>
</table>

