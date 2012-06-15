<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmEntradaDeProduto.aspx.vb" Inherits="Estoque.Client.frmEntradaDeProduto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register Src="ctrlProduto.ascx" TagName="ctrlProduto" TagPrefix="uc1" %>
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
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da movimentação" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDaMovimentacao" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Data da movimentação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDateInput ID="txtDataDaMovimentacao" runat="server">
                                    </telerik:RadDateInput>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Número documento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDocumento" runat="server">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Fornecedor"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboFornecedor" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="400px" Skin="Vista"
                                        CausesValidation="False" MaxLength="80">
                                    </telerik:RadComboBox>
                                    <asp:ImageButton ID="btnNovoFornecedor" runat="server" ImageUrl="~/imagens/new.gif"
                                        CausesValidation="False" CommandArgument="OPE.NCL.013.0001" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Histórico"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtHistorico" runat="server" Rows="5" 
                                        TextMode="MultiLine" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock1" runat="server" Title="Produtos da movimentação" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlProdutosDaMovimentacao" runat="server">
                        <uc1:ctrlProduto ID="ctrlProduto1" runat="server" />
                    </asp:Panel>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Quantidade"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtQuantidadeDeProdutoMovimentado" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Preço"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtPrecoProdutoMovimentado" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td" colspan="2">
                                <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar produto" CssClass="RadUploadSubmit" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server">
                        <telerik:RadGrid ID="grdItensLancados" runat="server" AutoGenerateColumns="False"
                            GridLines="None" Skin="Vista">
                            <MasterTableView GridLines="Both">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column7"
                                        ImageUrl="~/imagens/delete.gif">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="Produto.ID" UniqueName="column" Visible="False"
                                        HeaderText="ID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Quantidade" UniqueName="column1" Visible="True"
                                        HeaderText="Quantidade">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Preco" UniqueName="column2" Visible="True" HeaderText="Preço unitário">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Produto.Nome" UniqueName="column3" HeaderText="Produto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PrecoTotal" UniqueName="column5" HeaderText="Total">
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
