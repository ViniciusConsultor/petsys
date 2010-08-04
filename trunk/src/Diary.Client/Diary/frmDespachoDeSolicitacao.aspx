<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmDespachoDeSolicitacao.aspx.vb" Inherits="Diary.Client.frmDespachoDeSolicitacao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlDespachoAgenda.ascx" TagName="ctrlDespachoAgenda" TagPrefix="uc1" %>
<%@ Register Src="ctrlDespachoTarefa.ascx" TagName="ctrlDespachoTarefa" TagPrefix="uc2" %>
<%@ Register Src="../ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc3" %>
<%@ Register Src="ctrlDespachoLembrete.ascx" TagName="ctrlDespachoLembrete" TagPrefix="uc4" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Alvo do despacho" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc3:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                    <asp:Label ID="lblInconsistencia" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock1" runat="server" Title="Despacho" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDespacho" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo de despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboDespacho" runat="server" Skin="Vista" AutoPostBack="True">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlComponenteDespachoAgenda" runat="server">
                            <uc1:ctrlDespachoAgenda ID="ctrlDespachoAgenda1" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlComponenteDespachoTarefa" runat="server">
                            <uc2:ctrlDespachoTarefa ID="ctrlDespachoTarefa1" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlComponenteDespachoLembrete" runat="server">
                            <uc4:ctrlDespachoLembrete ID="ctrlDespachoLembrete1" runat="server" />
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Despachos da solicitação" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadToolBar ID="toolDespachos" runat="server" Skin="Vista" Style="width: 100%;">
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/imprimir.png" Text="Imprimir despachos"
                                CommandName="btnImprimirDespachos" />
                        </Items>
                    </telerik:RadToolBar>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Opção de filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboTipoDeFiltro" runat="server" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlEntreDadas">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Data do despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataInicial" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label2" runat="server" Text=" a  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataFinal" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarEntreDadas" runat="server" ToolTip="Pesquisar" ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlTipoDeDespacho">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Tipo de despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboTipoDespachoFiltro" runat="server" Skin="Vista">
                                    </telerik:RadComboBox>
                                    <asp:ImageButton ID="btnPesquisarPorTipoDeDespacho" runat="server" ToolTip="Pesquisar"
                                        ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <telerik:RadGrid ID="grdDespachos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="10" GridLines="None" Skin="Vista">
                        <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                        <MasterTableView GridLines="Both">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DataDoDespacho" HeaderText="Data do despacho"
                                    UniqueName="column30">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tipo.Descricao" HeaderText="Tipo" UniqueName="column3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Solicitante.Nome" HeaderText="Pessoa solicitante"
                                    UniqueName="column6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Alvo.Nome" HeaderText="Pessoa alvo" UniqueName="column1"
                                    Visible="True">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
