<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdLancamentoFinanceiroRecebimento.aspx.cs" Inherits="FN.Client.FN.cdLancamentoFinanceiroRecebimento" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="ctrlCliente" Src="~/ctrlCliente.ascx" %>
<%@ Register Src="ctrlTipoLacamentoFinanceiroRecebimento.ascx" TagName="ctrlTipoLacamentoFinanceiroRecebimento" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Width="100%" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Titular" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDaConta" runat="server">
                        <table class="tabela">
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Descrição"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricao" runat="server" MaxLength="255" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlCliente ID="ctrlCliente" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Data do lançamento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDoLancamento" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                              <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Data do vencimento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDoVencimento" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Valor"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtValor" runat="server" Type="Number">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Tipo"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc3:ctrlTipoLacamentoFinanceiroRecebimento ID="ctrlTipoLacamentoFinanceiroRecebimento" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Observação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtObservacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Width="450px" Rows="5">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
