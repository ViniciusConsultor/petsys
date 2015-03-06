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
                            <tr runat="server" id="pnlApresentacao">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Apresentação"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnPesquisarPorApresentacao" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorApresentacao_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlTitular">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Titular"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTitular" runat="server">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="td">
                                    <asp:Label ID="Label4" runat="server" Text="UF"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtUF" runat="server">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="td">
                                    <asp:Label ID="Label9" runat="server" Text="Pais"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPais" runat="server">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <asp:ImageButton ID="btnPesquisarPorCliente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorTitular_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlMarca">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Marca"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnPesquisarPorMarca" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorMarca_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlNatureza">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Natureza"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnPesquisarPorNatureza" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorNatureza_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlNCL">
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="NCL"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnPesquisarPorNCL" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorNCL_OnClick" />
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
                            <tr runat="server" id="pnlDespacho">
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" Text="Despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/find.gif" ToolTip="Pesquisar"
                                        OnClick="btnPesquisarPorDespacho_OnClick" />
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
                                        <%--  <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="NumeroProcessoDeMarca" HeaderText="Processo" />
                                                     <telerik:GridGroupByField FieldName="Marca" HeaderText="Marca" />
                                                  
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="NumeroProcessoDeMarca" />
                                                  
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>--%>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="NumeroProcessoDeMarca" HeaderText="Processo"
                                                UniqueName="column0">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Apresentacao" HeaderText="Apresentação" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Titular" HeaderText="Titular" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CodigoClasseNice" HeaderText="Classe" UniqueName="column4">
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
