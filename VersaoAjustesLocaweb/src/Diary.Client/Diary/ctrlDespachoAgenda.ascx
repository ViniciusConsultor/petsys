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
                <telerik:RadEditor ID="txtAssunto" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                    Language="pt-BR" MaxHtmlLength="4000" Skin="Vista" AutoResizeHeight="True" Width="450px"
                    ContentAreaCssFile="~/RadEditor/StyleSheetRadEditor.css">
                    <content>
                    </content>
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label4" runat="server" Text="Local"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadEditor ID="txtLocal" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                    Language="pt-BR" MaxHtmlLength="4000" Skin="Vista" AutoResizeHeight="True" Width="450px"
                    ContentAreaCssFile="~/RadEditor/StyleSheetRadEditor.css">
                    <content>
                    </content>
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label5" runat="server" Text="Descrição"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadEditor ID="txtDescricao" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                    Language="pt-BR" MaxHtmlLength="4000" Skin="Vista" AutoResizeHeight="True" Width="450px"
                    ContentAreaCssFile="~/RadEditor/StyleSheetRadEditor.css">
                    <content>
                    </content>
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAdicionarDespacho" runat="server" CssClass="RadUploadSubmit" Text="Despachar" />
            </td>
        </tr>
    </table>
</asp:Panel>
