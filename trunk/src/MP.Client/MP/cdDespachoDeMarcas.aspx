<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdDespachoDeMarcas.aspx.cs" Inherits="MP.Client.MP.cdDespachoDeMarcas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas" TagPrefix="uc1" %>
<%@ Register Src="ctrlSituacaoDoProcesso.ascx" TagName="ctrlSituacaoDoProcesso" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.004.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.004.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.004.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro do despacho de marcas"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlDespachoDeMarcas ID="ctrlDespachoDeMarcas" runat="server" />
                    <asp:Panel ID="PanelCdDespachoDeMarcas" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Código do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtCodigo" runat="server" Width="87px">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Descrição:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Situação do processo após a publicação:"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlSituacaoDoProcesso ID="ctrlSituacaoDoProcesso" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Indica a concessão do registro:"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblConcessaoDeRegistro" runat="server" AutoPostBack="false"
                                        RepeatDirection="Horizontal">
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
