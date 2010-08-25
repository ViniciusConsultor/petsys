<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmPainelDeControle.aspx.vb" Inherits="WorkSpace.frmPainelDeControle" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Painel de controle" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1">
                        <Tabs>
                            <telerik:RadTab runat="server" Selected="True" Text="Ferramentas administrativas">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="E-mail">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="rpvFerramentas" runat="server">
                            <table class="tabela">
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label24" runat="server" Text="Notificar erros na aplicação automaticamente?"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:CheckBox ID="chkNotificarErrosNaAplicacaoAutomaticamente" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label4" runat="server" Text="E-mail do remetente da notificação de erros"></asp:Label>
                                    </td>
                                    <td class="td">
                                         <telerik:RadTextBox ID="txtRemetenteNotificacaoDeErros" runat="server" MaxLength="255" Skin="Vista"
                                            Width="300px" SelectionOnFocus="CaretToBeginning">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvEmail" runat="server">
                            <table class="tabela">
                              <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label2" runat="server" Text="Tipo"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadComboBox ID="cboTipoDeServidor" Runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label3" runat="server" 
                                            Text="Servidor de saída de e-mails"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtServidorDeSaidaDeEmail" runat="server" MaxLength="255" Skin="Vista"
                                            Width="300px" SelectionOnFocus="CaretToBeginning">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label1" runat="server" Text="Porta"></asp:Label>
                                    </td>
                                    <td class="td" style="height: 32px">
                                        <telerik:RadNumericTextBox ID="txtPorta" Runat="server" 
                                            DataType="System.Int16" Skin="Vista">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label25" runat="server" Text="Habitar SSL?"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:CheckBox ID="chkSSL" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label26" runat="server" Text="Requer autenticação?"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:CheckBox ID="chkRequerAutenticacao" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label27" runat="server" Text="Usuário"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtUsuarioAutenticacaoServidorDeSaida" runat="server" MaxLength="255" 
                                            SelectionOnFocus="CaretToBeginning" Skin="Vista" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label28" runat="server" Text="Senha"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtSenhaUsuarioAutenticacaoServidorDeSaida" runat="server" MaxLength="255" 
                                            SelectionOnFocus="CaretToBeginning" Skin="Vista" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label29" runat="server" Text="Remetente"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtRemetente" runat="server" MaxLength="255" 
                                            SelectionOnFocus="CaretToBeginning" Skin="Vista" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
