<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmContasAReceber.aspx.cs" Inherits="FN.Client.FN.frmContasAReceber" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc4" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc5" %>
<%@ Register Src="ctrlSituacao.ascx" TagName="ctrlSituacao" TagPrefix="uc1" %>
<%@ Register Src="ctrlFormaRecebimento.ascx" TagName="ctrlFormaRecebimento" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.FN.001.0001" />
            <telerik:RadToolBarButton runat="server" Text="Gerar boleto coletivamente" ImageUrl="~/imagens/boleto.png"
                CommandName="btnGerarBoletoColetivo" CausesValidation="False" />
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
                            <tr runat="server" id="pnlSituacao">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Situação"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlSituacao ID="ctrlSituacao" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorSituacao" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorSituacao_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlFormaDeRecebimento">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Forma de recebimento"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlFormaRecebimento ID="ctrlFormaRecebimento" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorFormaDeRecebimento" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorFormaDeRecebimento_OnClick_" />
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
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkProcessosDeMarcas" runat="server" Title="Processos de Marcas"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdItensDeContasAReceber" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdItensDeContasAReceber_OnPageIndexChanged"
                                    OnItemCommand="grdItensDeContasAReceber_OnItemCommand">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView Width="100%">
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="Cliente.Pessoa.Nome" HeaderText="Cliente" />
                                                    <%-- <telerik:GridGroupByField FieldName="Valor" HeaderText="TOTAL"  Aggregate="Sum" />--%>
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="Cliente.Pessoa.Nome" SortOrder="Descending" />
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
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" FilterImageToolTip="Modificar"
                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="GerarBoleto" FilterImageToolTip="Gerar boleto"
                                                HeaderTooltip="Gerar boleto" ImageUrl="~/imagens/boletopequeno.png" UniqueName="column108">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Cancelar" FilterImageToolTip="Cancelar"
                                                HeaderTooltip="Cancelar" ImageUrl="~/imagens/delete.gif" UniqueName="column8"
                                                ConfirmDialogType="RadWindow" ConfirmText="Deseja mesmo cancelar a conta a receber?"
                                                ConfirmTitle="Cancelar">
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
                                            <telerik:GridBoundColumn DataField="DataDoRecebimento" HeaderText="Data do recebimento"
                                                UniqueName="column83" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FormaDeRecebimento.Descricao" HeaderText="Forma de recebimento"
                                                UniqueName="column563">
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
