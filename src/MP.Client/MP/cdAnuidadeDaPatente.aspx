<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdAnuidadeDaPatente.aspx.cs" Inherits="MP.Client.MP.cdAnuidadeDaPatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Anuidades de Patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="PanelDadosDoProcurador" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDescricaoDaAnuidade" runat="server" Text="Descrição da Anuidade" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricaoDaAnuidade" MaxLength="45" runat="server">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblInicioPrazoPagamento" runat="server" Text="Início Prazo de Pagto" />
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtInicioPrazoPagamento" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPagamentoSemMulta" runat="server" Text="Limite Pagto S/ Multa" />
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtPagamentoSemMulta" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPagamentoComMulta" runat="server" Text="Limite Pagto C/ Multa" />
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtPagamentoComMulta" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataPagamento" runat="server" Text="Data Pagamento" />
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="RadDatePicker3" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblValorPagamento" runat="server" Text="Valor Pagamento" />
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtValorPagamento" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
