<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDespachoAgenda.ascx.vb"
    Inherits="Diary.Client.ctrlDespachoAgenda" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlDadosDoCompromisso" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label1" runat="server" Text="Data e horário de início"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadDateTimePicker ID="txtDataHorarioInicio" runat="server">
                </telerik:RadDateTimePicker>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label2" runat="server" Text="Data e horário de término"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadDateTimePicker ID="txtDataHorarioFim" runat="server">
                </telerik:RadDateTimePicker>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label3" runat="server" Text="Assunto"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadTextBox ID="txtAssunto" runat="server" Width="350px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label4" runat="server" Text="Local"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadTextBox ID="txtLocal" runat="server" Width="350px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label5" runat="server" Text="Descrição"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadTextBox ID="txtDescricao" runat="server" Rows="10" TextMode="MultiLine"
                    Width="350px">
                </telerik:RadTextBox>
            </td>
        </tr>
         <tr>
            <td colspan="2">
             <asp:Button ID="btnAdicionarDespacho" runat="server" CssClass="RadUploadSubmit" 
                                                Text="Despachar" />
            </td>
        </tr>
    </table>
</asp:Panel>
