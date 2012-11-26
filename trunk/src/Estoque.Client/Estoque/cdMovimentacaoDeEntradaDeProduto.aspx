<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdMovimentacaoDeEntradaDeProduto.aspx.vb" Inherits="Estoque.Client.cdMovimentacaoDeEntradaDeProduto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.ETQ.005.0001" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Pesquisa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label76" runat="server" Text="Data da movimentação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataInicial" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label77" runat="server" Text=" a  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataFinal" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock> 
            <telerik:RadDock ID="RadDock2" runat="server" Title="Movimentações" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlMovimentacoes" runat="server">
                        <telerik:RadGrid ID="grdMovimentacoes" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Skin="Vista">
                            <MasterTableView GridLines="Both">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Detalhar" UniqueName="column5"
                                        ImageUrl="~/imagens/details.gif">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column6"
                                        ImageUrl="~/imagens/delete.gif">
                                        </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="ID" UniqueName="column" Visible="False"
                                        HeaderText="ID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Data" UniqueName="column1" Visible="True"
                                        HeaderText="Data">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NumeroDocumento" UniqueName="column2" Visible="True" HeaderText="Número documento">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fornecedor.Pessoa.Nome" UniqueName="column3" HeaderText="Fornecedor">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalDaMovimentacao" UniqueName="column4" HeaderText="Total da movimentação">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
