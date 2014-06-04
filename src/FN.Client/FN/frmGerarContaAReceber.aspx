<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmGerarContaAReceber.aspx.cs" Inherits="FN.Client.FN.frmGerarContaAReceber" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="ctrlCliente" Src="~/ctrlCliente.ascx" %>
<%@ Register Src="ctrlSituacao.ascx" TagName="ctrlSituacao" TagPrefix="uc2" %>
<%@ Register Src="ctrlTipoLacamentoFinanceiroRecebimento.ascx" TagName="ctrlTipoLacamentoFinanceiroRecebimento" TagPrefix="uc3" %>
<%@ Register Src="ctrlFormaRecebimento.ascx" TagName="ctrlFormaRecebimento" TagPrefix="uc4" %>
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
                    <asp:Panel ID="pnlDadosDosItens" runat="server">
                        <table class="tabela">
                             
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Forma do recebimento"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc4:ctrlFormaRecebimento ID="ctrlFormaRecebimento" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>