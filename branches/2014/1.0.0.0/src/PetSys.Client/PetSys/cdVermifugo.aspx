<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="cdVermifugo.aspx.vb" Inherits="PetSys.Client.cdVermifugo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="pnlDadosDoVermifugo" runat="server" Title="Dados do vermífugo" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Data"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtData" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNome" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Observação"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtObservacao" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Próxima dose"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtProximaDose" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Veterinário responsável"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblVeterinarioResponsavel" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
