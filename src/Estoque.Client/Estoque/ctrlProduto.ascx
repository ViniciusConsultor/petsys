<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlProduto.ascx.vb"
    Inherits="Estoque.Client.ctrlProduto" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label1" runat="server" Text="Código de barras "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <telerik:RadTextBox ID="txtCodigo" runat="server" AutoPostBack="True" Width="200px">
            </telerik:RadTextBox>
            &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Nome "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <telerik:RadComboBox ID="cboProduto" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                AllowCustomText="True" HighlightTemplatedItems="True" Width="350px" Skin="Vista"
                CausesValidation="False" MaxLength="80">
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
