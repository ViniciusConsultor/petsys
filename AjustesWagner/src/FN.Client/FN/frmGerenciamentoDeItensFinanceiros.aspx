<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmGerenciamentoDeItensFinanceiros.aspx.cs" Inherits="FN.Client.FN.frmGerenciamentoDeItensFinanceiros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc4" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.FN.007.0001" />
            <telerik:RadToolBarButton runat="server" Text="Gerar conta a receber coletivamente"
                ImageUrl="~/imagens/dinheiro.gif" CommandName="btnGerarContaAReceberColetivo"
                CausesValidation="False" />
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
                            <tr runat="server" id="pnlPeriodoDeVencimento">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Período"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtPeriodo1" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label4" runat="server" Text="  à  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtPeriodo2" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarPorPeriodoDeVencimento" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorPeriodoDeVencimento_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDescricao">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Descrição"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricao" runat="server" Width="450px" MaxLength="255">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorDescricao" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDescricao_OnClick_" />
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
            <telerik:RadDock ID="rdkItensFinanceirosDeRecebimento" runat="server" Title="Lançamentos financeiros de recebimento"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdItensFinanceiros" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="50" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdItensFinanceiros_OnPageIndexChanged"
                                    OnItemCommand="grdItensFinanceiros_OnItemCommand" OnItemDataBound="grdItensFinanceiros_OnItemDataBound">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView Width="100%">
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="Cliente.Pessoa.Nome" HeaderText="Cliente" />
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="Cliente.Pessoa.Nome" />
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="ToggleRowSelection"
                                                        AutoPostBack="True" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                        AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" FilterImageToolTip="Modificar"
                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="GerarContaAReceber"
                                                FilterImageToolTip="Gerar conta a receber" HeaderTooltip="Gerar conta a receber"
                                                ImageUrl="~/imagens/dinheiro.gif" UniqueName="column108">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="column353">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente.Pessoa.ID" HeaderText="ID do cliente"
                                                UniqueName="column991" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente.Pessoa.Nome" HeaderText="Cliente" UniqueName="column53"
                                                Display="false">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor" UniqueName="column5"
                                                DataFormatString="{0:C}">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDoLancamento" HeaderText="Data do lançamento"
                                                UniqueName="column3" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDoVencimento" HeaderText="Data do vencimento"
                                                UniqueName="column23" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Situacao.Descricao" HeaderText="Situação" UniqueName="column63">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TipoLacamento.Descricao" HeaderText="Tipo do lançamento"
                                                UniqueName="column73">
                                                <ItemStyle Width="20%"></ItemStyle>
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
