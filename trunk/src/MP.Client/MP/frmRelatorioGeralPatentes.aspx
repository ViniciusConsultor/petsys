<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmRelatorioGeralPatentes.aspx.cs" Inherits="MP.Client.MP.frmRelatorioGeralPatentes" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc1" %>
<%@ Register Src="~/MP/ctrlInventor.ascx" TagName="ctrlInventor" TagPrefix="uc2" %>
<%@ Register Src="~/MP/ctrlTitular.ascx" TagName="ctrlTitular" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Gerar" ImageUrl="~/imagens/imprimir.png"
                CommandName="btnImprimir" CausesValidation="False" CommandArgument="OPE.MP.020.0001"  />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Pesquisa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlRegistroConcedido" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTiposDePatente" runat="server" Text="Tipos de Patentes:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblTiposDePatente" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                                        <Items>
                                            <asp:ListItem Selected="true" runat="server" Value="Patentes" />
                                            <asp:ListItem runat="server" Value="Patentes (Desenho Industrial)" />
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblOrigemDosProcessos" runat="server" Text="Origem de Processo:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblOrigemDeProcessos" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                                        <Items>
                                            <asp:ListItem Selected="true" runat="server" Value="Todos" />
                                            <asp:ListItem runat="server" Value="Nacionais" />
                                            <asp:ListItem runat="server" Value="Internacionais" />
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTipoDeOrigem" runat="server" Text="Tipo de Origem:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rdlTipoDeOrigem" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                                        <Items>
                                            <asp:ListItem Selected="true" runat="server" Value="Próprios e Terceiros" />
                                            <asp:ListItem runat="server" Value="Próprios" />
                                            <asp:ListItem runat="server" Value="Terceiros" />
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblStatusDoProcesso" runat="server" Text="Status do Processo:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rdlStatusDoProcesso" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                                        <Items>
                                            <asp:ListItem Selected="true" runat="server" Value="Ativos e Inativos" />
                                            <asp:ListItem runat="server" Value="Ativos" />
                                            <asp:ListItem runat="server" Value="Inativos" />
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Ordenação:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rdlOrdenacao" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                                        <Items>
                                            <asp:ListItem Selected="true" runat="server" Value="Cliente" />
                                            <asp:ListItem runat="server" Value="Patente" />
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCliete" runat="server" Text="Cliente:" />
                                </td>
                                <td class="td">
                                    <uc1:ctrlCliente runat="server" ID="ctrlCliente1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTitular" runat="server" Text="Titular:" />
                                </td>
                                <td class="td">
                                    <uc3:ctrlTitular runat="server" ID="ctrlTitular1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblInventor" runat="server" Text="Inventor:" />
                                </td>
                                <td class="td">
                                    <uc2:ctrlInventor runat="server" ID="ctrlInventor1" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
