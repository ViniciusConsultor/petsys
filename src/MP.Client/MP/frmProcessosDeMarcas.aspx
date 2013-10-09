<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmProcessosDeMarcas.aspx.cs" Inherits="MP.Client.MP.frmProcessosDeMarcas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo Processo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.DRY.002.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/imprimir.png" Text="Imprimir"
                CommandName="btnImprimir" CausesValidation="False" CommandArgument="OPE.DRY.002.0006" />
            <telerik:RadToolBarButton runat="server" Text="Recarregar" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnRecarregar" CausesValidation="False" />
             <telerik:RadToolBarButton runat="server" Text="Ler revista" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnLerRevista" CausesValidation="False" />
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
                                    <telerik:RadComboBox ID="cboTipoDeFiltro" runat="server" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlEntreDadas">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Data de cadastro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataInicial" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label1" runat="server" Text=" a  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataFinal" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlCodigoDaSolicitacao">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Código da solicitação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtCodigoDaSolicitacao" runat="server" DataType="System.Int64"
                                        AllowOutOfRangeAutoCorrect="False">
                                    </telerik:RadNumericTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorCodigo" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" />
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
                                <telerik:RadGrid ID="grdProcessosDeMarcas" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="50" GridLines="None" Skin="Vista" AllowFilteringByColumn="true">
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
                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" UniqueName="column30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Assunto" HeaderText="Pauta" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Local" HeaderText="Local" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDaSolicitacao" HeaderText="Data e hora do cadastro"
                                                UniqueName="column1" Visible="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contato.Pessoa.Nome" HeaderText="Contato" UniqueName="column2"
                                                Visible="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Despachar" FilterImageToolTip="Despachar"
                                                HeaderTooltip="Despachar" ImageUrl="~/imagens/accordian.gif" UniqueName="column7">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Finalizar" FilterImageToolTip="Finalizar"
                                                HeaderTooltip="Finalizar" ImageUrl="~/imagens/yes.gif" UniqueName="column9">
                                            </telerik:GridButtonColumn>
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
