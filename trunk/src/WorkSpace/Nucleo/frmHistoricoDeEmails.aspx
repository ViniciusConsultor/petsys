<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmHistoricoDeEmails.aspx.vb" Inherits="WorkSpace.frmHistoricoDeEmails" %>

<%@ Import Namespace="Compartilhados.Interfaces.Core.Negocio" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Recarregar" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnRecarregar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Limpar" ImageUrl="~/imagens/limpar.gif"
                CommandName="btnLimpar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Filtro" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Opção de filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboTipoDeFiltro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTipoDeFiltro_OnSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Operação do filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlOperacaoFiltro ID="ctrlOperacaoFiltro1" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlAssunto">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Assunto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtAssunto" runat="server" Skin="Vista" MaxLength="255">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorAssunto" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorAssunto_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDestinario">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Destinatário"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDestinario" runat="server" Skin="Vista" MaxLength="255">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorDestinario" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDestinatario_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlData">
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Data de envio"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeEnvio" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarPorDataDeEnvio" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDataDeEnvio_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlContexto">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Contexto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtContexto" runat="server" Skin="Vista" MaxLength="255">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorContexto" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorContexto_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlMensagem">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Mensagem"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtMensagem" runat="server" Skin="Vista" MaxLength="255">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorMensagem" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorMensagem_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkHistorico" runat="server" Title="Histórico de e-mails enviados"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdHistoricoDeEmails" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdHistoricoDeEmails_OnPageIndexChanged"
                                    OnItemCommand="grdHistoricoDeEmails_OnItemCommand">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Detalhar" FilterImageToolTip="Detalhar"
                                                HeaderTooltip="Detalhar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="ReenviarEmail" FilterImageToolTip="Reenviar o e-mail"
                                                HeaderTooltip="Reenviar o e-mail" ImageUrl="~/imagens/email.gif" UniqueName="column9">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Data" HeaderText="Data" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Assunto" HeaderText="Assunto" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                           <%-- <telerik:GridBoundColumn DataField="Mensagem" HeaderText="Mensagem" UniqueName="column11">
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridTemplateColumn HeaderText="Destinatários">
                                                <ItemTemplate>
                                                    <%#MontaListaDeDestinatarios(CType(Container.DataItem, IHistoricoDeEmail))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Destinatários CC">
                                                <ItemTemplate>
                                                    <%#MontaListaDeDestinatariosCC(CType(Container.DataItem, IHistoricoDeEmail))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Destinatários CCo">
                                                <ItemTemplate>
                                                    <%#MontaListaDeDestinatariosCCo(CType(Container.DataItem, IHistoricoDeEmail))%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Contexto" HeaderText="Contexto" UniqueName="column10">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
