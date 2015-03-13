<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmDetalharPesquisaDeMarca.aspx.cs" Inherits="PMP.Client.PMP.frmDetalharPesquisaDeMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de marca"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Número da RPI"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblNumeroRPI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Data de publicação da RPI"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDataPublicacaoRPI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Número processo de marca"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblNumeroProcessoDeMarca" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Data do depósito"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDataDoDeposito" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Data de concessão"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDataDeConcessao" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Data de vigência"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDataDeVigencia" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label7" runat="server" Text="Código de despacho"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblCodigoDespacho" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label8" runat="server" Text="Despacho"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDespacho" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label9" runat="server" Text="Titular"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblTitular" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label10" runat="server" Text="País do titular"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblPaisDoTitular" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label11" runat="server" Text="UF do titular"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblUFTitular" runat="server"></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td class="th3">
                                <asp:Label ID="Label12" runat="server" Text="Marca"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblMarca" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
