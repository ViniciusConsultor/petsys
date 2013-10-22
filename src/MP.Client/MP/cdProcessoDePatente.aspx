<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdProcessoDePatente.aspx.cs" Inherits="MP.Client.MP.cdProcessoDePatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlPatente.ascx" TagName="ctrlPatente" TagPrefix="uc1" %>
<%@ Register Src="ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de patente"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlProcessoDeMarca" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Patente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlPatente ID="ctrlPatente1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Data de entrada"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeEntrada" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcesso" runat="server" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Processo é de terceiro?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblProcessoEhDeTerceiro" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Procurador"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlProcurador ID="ctrlProcurador" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Processo é estrangeiro?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblProcessoEhEstrangeiro" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Situção"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblSituacao" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
