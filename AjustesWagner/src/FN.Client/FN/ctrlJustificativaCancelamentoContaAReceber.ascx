<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlJustificativaCancelamentoContaAReceber.ascx.cs" Inherits="FN.Client.FN.ctrlJustificativaCancelamentoContaAReceber" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label8" runat="server" Text="Justificativa:"></asp:Label>
        </td>
        <td class="td">
             <telerik:RadTextBox ID="txtJustificativa" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                                Rows="10" Width="350px">
                                                            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="td" colspan="2">
            <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar conta" Skin="Vista" OnClick="btnCancelar_ButtonClick">
            </telerik:RadButton>
            &nbsp;
            &nbsp;
            <telerik:RadButton ID="btnFechar" runat="server" Text="Fechar" Skin="Vista" OnClick="btnFechar_ButtonClick">
            </telerik:RadButton>
        </td>
    </tr>
</table>
