<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmRelatorioGeralPatentes.aspx.cs" Inherits="MP.Client.MP.frmRelatorioGeralPatentes" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2013.3.1015.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc1" %>
<%@ Register Src="~/ctrlEmpresa.ascx" TagName="ctrlEmpresa" TagPrefix="uc2" %>
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
                                    <asp:RadioButtonList ID="rblTiposDePatente" runat="server" RepeatDirection="Horizontal">
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
                                    <asp:RadioButtonList ID="rblOrigemDeProcessos" runat="server" RepeatDirection="Horizontal">
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
                                    <asp:RadioButtonList ID="rdlTipoDeOrigem" runat="server" RepeatDirection="Horizontal">
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
                                    <asp:RadioButtonList ID="rdlStatusDoProcesso" runat="server" RepeatDirection="Horizontal">
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
                                    <asp:RadioButton ID="rdbCliente" runat="server" />
                                </td>
                                <td class="td">
                                    <uc1:ctrlCliente runat="server" ID="crtlCliente1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:RadioButton ID="rdbTitular" runat="server" />
                                </td>
                                <td class="td">
                                    <uc3:ctrlTitular runat="server" ID="ctrlTitular1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:RadioButton ID="rdbEmpresa" runat="server" />
                                </td>
                                <td class="td">
                                    <uc2:ctrlEmpresa runat="server" ID="ctrlEmpresa1" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
