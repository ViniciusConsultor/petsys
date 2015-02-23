<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmBoletosGerados.aspx.cs" Inherits="FN.Client.FN.frmBoletosGerados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc4" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc5" %>
<%@ Register Src="~/ctrlCedente.ascx" TagName="ctrlCedente" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Recarregar" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnRecarregar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Limpar" ImageUrl="~/imagens/limpar.gif"
                CommandName="btnLimpar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Gerar Relatório" ImageUrl="~/imagens/imprimir.png"
                CommandName="btnRelatorio" CausesValidation="False" />
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
                            <tr runat="server" id="pnlOpcaoDeFiltro">
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Operação do filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc4:ctrlOperacaoFiltro ID="ctrlOperacaoFiltro1" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlCliente">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc5:ctrlCliente ID="ctrlCliente1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorCliente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorCliente_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlCedente">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Cedente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc3:ctrlCedente ID="ctrlCedente1" runat="server" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar"
                                        OnClick="btnPesquisarPorCedente_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDataDeGeracao">
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Selecione o período:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeGeracaoIni" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label7" runat="server" Text="  à  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataDeGeracaoFim" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarPorDataDeCadastro" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDataDeGeracao_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDataDeVencimento">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Selecione o período:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeVencimentoIni" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label8" runat="server" Text="  à  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataDeVencimentoFim" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar"
                                        OnClick="btnPesquisarPorDataDeVencimento_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlNossoNumero">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Nosso Número:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNossoNumero" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPoNossoNumero" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorNossoNumero_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlVencidos">
                                <td class="th3">
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnVencidos" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar"
                                        OnClick="btnVencidos_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlAVencer">
                                <td class="th3">
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnAVencer" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar"
                                        OnClick="btnAVencer_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkBoletosGerados" runat="server" Title="Boletos Gerados" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdBoletosGerados" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdBoletosGerados_OnPageIndexChanged"
                                    OnItemCommand="grdBoletosGerados_OnItemCommand" OnItemDataBound="grdBoletosGerados_OnItemDataBound">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" FilterImageToolTip="Modificar"
                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column11">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column12"
                                                ConfirmDialogType="RadWindow" ConfirmText="Deseja mesmo excluir o boleto gerado?"
                                                ConfirmTitle="Apagar boleto gerado">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cedente" HeaderText="Cedente" UniqueName="column2">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente" UniqueName="column3">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumeroBoleto" HeaderText="Num. Boleto" UniqueName="column4">
                                                <ItemStyle Width="6%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NossoNumero" HeaderText="Nosso Número" UniqueName="column5">
                                                <ItemStyle Width="7%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor R$" UniqueName="column6">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataGeracao" HeaderText="Data Geração" UniqueName="column7">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataVencimento" HeaderText="Data Vencimento"
                                                UniqueName="column8">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StatusBoleto" HeaderText="Status" UniqueName="column9">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EhBoletoAvulso" HeaderText="Boleto Avulso" UniqueName="column10">
                                                <ItemStyle Width="5%"></ItemStyle>
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
