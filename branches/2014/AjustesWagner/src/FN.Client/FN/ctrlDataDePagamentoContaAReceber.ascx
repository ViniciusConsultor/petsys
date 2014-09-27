<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlDataDePagamentoContaAReceber.ascx.cs"
    Inherits="FN.Client.FN.ctrlDataDePagamentoContaAReceber" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlDataDePagamentoContaAReceber" runat="server">
<form id="form1" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label8" runat="server" Text="Informe a data de recebimento:"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadDatePicker ID="txtDataDeRecebimento" runat="server">
                </telerik:RadDatePicker>
            </td>
             <td class="td">
                <telerik:RadButton ID="btnReceber" runat="server" Text="Receber" Skin="Vista"
                    OnClick="btnReceber_ButtonClick">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
    </form>
</asp:Panel>
