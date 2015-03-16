<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmPesquisaDeMarca.aspx.cs" Inherits="PMP.Client.PMP.frmPesquisaDeMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label12" runat="server" Text="Revista"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblOpcaoDeRevista" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblOpcaoDeRevista_OnSelectedIndexChanged">
                                    </asp:RadioButtonList>
                                    <telerik:RadNumericTextBox ID="txtNumeroRevista" runat="server" Type="Number" EmptyMessage="Número da revista" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlTitular">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Titular"></asp:Label>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtTitular" runat="server" Width="100%">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label4" runat="server" Text="UF"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtUF" runat="server" Width="30px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label9" runat="server" Text="Pais"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtPais" runat="server" Width="30px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <asp:ImageButton ID="btnPesquisarPorTitular" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorTitular_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlMarca">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Marca"></asp:Label>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label8" runat="server" Text="Nome"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtMarca" runat="server">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label11" runat="server" Text="NCL"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtNCL" runat="server">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <asp:ImageButton ID="btnPesquisarPorMarca" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorMarca_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlProcesso">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcesso" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorProcesso" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorProcesso_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlProcurador">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Procurador"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcurador" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorProcurador" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorProcurador_OnClick" />
                                </td>
                            </tr>
                              <tr runat="server" id="pnlCodigoFigura">
                                <td class="th3">
                                    <asp:Label ID="Label13" runat="server" Text="Código figura"></asp:Label>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label14" runat="server" Text="Classificação de Viena"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtCodigosViena" runat="server">
                                                </telerik:RadTextBox>
                                                <asp:Label ID="Label16" runat="server" Text="Ex: 1.3.1|25.5.5"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label15" runat="server" Text="NCL"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadTextBox ID="txtNCLCodigoViena" runat="server">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td colspan="2" style="text-align: left">
                                    <asp:ImageButton ID="btnPesquisarPorCodigoDaFigura" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorCodigoDaFigura_OnClick" />
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
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdProcessosDeMarcas_OnPageIndexChanged"
                                    OnItemCommand="grdProcessosDeMarcas_OnItemCommand" OnItemDataBound="grdProcessosDeMarcas_OnItemDataBound">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView Width="100%">
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="NumeroProcessoDeMarca" HeaderText="Processo" />
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="NumeroProcessoDeMarca" />
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
                                         <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Detalhar" FilterImageToolTip="Detalhar"
                                                HeaderTooltip="Detalhar" ImageUrl="~/imagens/edit.gif" UniqueName="column000">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID"
                                                UniqueName="column00" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumeroDaRevista" HeaderText="RPI" UniqueName="column0">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumeroProcessoDeMarca" HeaderText="Processo"
                                                UniqueName="column1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Natureza" HeaderText="Natureza" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Apresentacao" HeaderText="Apresentação" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Titular" HeaderText="Titular" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Procurador" HeaderText="Procurador" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CodigoDoDespacho" HeaderText="Código despacho"
                                                UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NomeDoDespacho" HeaderText="Despacho" UniqueName="column8">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CodigoClasseNice" HeaderText="Classe" UniqueName="column9">
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
