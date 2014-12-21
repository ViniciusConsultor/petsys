<%@ Page Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmConfiguracaoGeralFinaceiro.aspx.cs" Inherits="FN.Client.FN.frmConfiguracaoGeralFinaceiro" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" CommandArgument="OPE.FN.006.0001" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Configuração Geral"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="tabConfigFinanceiro" runat="server" SelectedIndex="0" Skin="Vista" MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Boleto" Selected="True"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="rpvDadosBoleto" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlConfigGeralFinanceiro" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblInstrucoesDoBoleto" runat="server" Text="Instruções do boleto" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtInstrucoesDoBoleto" runat="server" TextMode="MultiLine" Rows="5" Width="100%" MaxLength="4000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblHabilitarBotaoImprimir" runat="server" Text="Habilitar botão de impressão" />
                                        </td>
                                        <td class="td">
                                        <asp:CheckBox ID="chkHabilitarBotaoImprimir" runat="server" AutoPostBack="true" />
                                    </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>                    
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
