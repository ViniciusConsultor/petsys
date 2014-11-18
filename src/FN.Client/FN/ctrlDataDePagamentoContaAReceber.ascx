<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlDataDePagamentoContaAReceber.ascx.cs"
    Inherits="FN.Client.FN.ctrlDataDePagamentoContaAReceber" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label8" runat="server" Text="Data do recebimento"></asp:Label>
        </td>
        <td class="td">
            <telerik:RadDatePicker ID="txtDataDeRecebimento" runat="server">
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="td" colspan="2">
             &nbsp;    
        </td>
    </tr>
    <tr>
        <td class="td" colspan="2">
            <telerik:RadButton ID="btnReceber" runat="server" Text="Receber conta" Skin="Vista" OnClick="btnReceber_ButtonClick">
            </telerik:RadButton>
             &nbsp;            
            <telerik:RadButton ID="btnFechar" runat="server" Text="Fechar" Skin="Vista" OnClick="btnFechar_ButtonClick">
            </telerik:RadButton>
        </td>
    </tr>
</table>
