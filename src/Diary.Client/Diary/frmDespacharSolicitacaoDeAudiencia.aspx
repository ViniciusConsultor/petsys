<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="frmDespacharSolicitacaoDeAudiencia.aspx.vb" Inherits="Diary.Client.frmDespacharSolicitacaoDeAudiencia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Contato" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDespacho" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo de despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboDespacho" runat="server" Skin="Vista">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da solicitação de audiência" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDaSolicitacao" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Assunto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtAssunto" runat="server" MaxLength="255" 
                                        Width="400px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Descrição"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricao" runat="server" MaxLength="500" Rows="10"
                                        TextMode="MultiLine" Width="400px">
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
