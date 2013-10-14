<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmProcessosDeMarcas.aspx.cs" Inherits="MP.Client.MP.frmProcessosDeMarcas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlApresentacao.ascx" TagName="ctrlApresentacao" TagPrefix="uc1" %>
<%@ Register Src="ctrlNatureza.ascx" TagName="ctrlNatureza" TagPrefix="uc2" %>
<%@ Register Src="ctrlNCL.ascx" TagName="ctrlNCL" TagPrefix="uc3" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc4" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc5" %>
<%@ Register Src="ctrlMarcas.ascx" TagName="ctrlMarcas" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista" Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo Processo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.007.0001" />
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
                            <tr runat="server" id="pnlApresentacao">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Apresentação"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlApresentacao ID="ctrlApresentacao1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorApresentacao" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick=btnPesquisarPorApresentacao_OnClick/>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlCliente">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc5:ctrlCliente ID="ctrlCliente1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorCliente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorCliente_OnClick_"/>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDataDeEntrada">
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Data de entrada"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeEntrada" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarPorDataDeEntrada" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDataDeEntrada_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlMarca">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Marca"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc6:ctrlMarcas ID="ctrlMarcas1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorMarca" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar"  OnClick="btnPesquisarPorMarca_OnClick"/>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlNatureza">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Natureza"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlNatureza ID="ctrlNatureza1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorNatureza" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar"  OnClick="btnPesquisarPorNatureza_OnClick"/>
                                </td>
                            </tr>
                             <tr runat="server" id="pnlNCL">
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="NCL"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc3:ctrlNCL ID="ctrlNCL1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorNCL" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorNCL_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlProcesso">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtProcesso" runat="server" DataType="System.Int64" Type="Number" >
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorProcesso" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorProcesso_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlProtocolo">
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Protocolo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtProtocolo" runat="server" DataType="System.Int64" Type="Number" >
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorProtoloco" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorProtoloco_OnClick" />
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
                                    AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista" AllowFilteringByColumn="true" OnPageIndexChanged="grdProcessosDeMarcas_OnPageIndexChanged" OnItemCommand="grdProcessosDeMarcas_OnItemCommand">
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
