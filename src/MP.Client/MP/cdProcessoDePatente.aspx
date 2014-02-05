<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdProcessoDePatente.aspx.cs" Inherits="MP.Client.MP.cdProcessoDePatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlPatente.ascx" TagName="ctrlPatente" TagPrefix="uc1" %>
<%@ Register Src="ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc2" %>
<%@ Register Src="ctrlDespachoDePatentes.ascx" TagName="ctrlDespachoDePatentes" TagPrefix="uc3" %>
<%@ Register Src="ctrlPasta.ascx" TagName="ctrlPasta" TagPrefix="uc4" %>
<%@ Register Src="ctrlNaturezaPatente.ascx" TagName="ctrlNaturezaPatente" TagPrefix="uc5" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc6" %>
<%@ Register Src="ctrlInventor.ascx" TagName="ctrlInventor" TagPrefix="uc7" %>
<%@ Register Src="ctrlTitular.ascx" TagName="ctrlTitular" TagPrefix="uc8" %>
<%@ Register Src="~/ctrlPais.ascx" TagName="ctrlPais" TagPrefix="uc9" %>
<%@ Register Src="ctrlPeriodo.ascx" TagName="ctrlPeriodo" TagPrefix="uc10" %>
<%@ Register Src="ctrlMes.ascx" TagName="ctrlMes" TagPrefix="uc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de patente"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Patente" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Complemento">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Prioridade unionista">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Classificação">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Radicais">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Anuidade">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Manutenção">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="rpvDadosPatentes" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosPatente" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Processo"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadNumericTextBox ID="txtProcesso" runat="server" DataType="System.Int64" Type="Number" Width="20%" MaxLength="20">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                                </NumberFormat>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label5" runat="server" Text="Está ativo?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblEstaAtivo" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Data de cadastro"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeCadastro" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label6" runat="server" Text="Data de concessão"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeConcessao" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label7" runat="server" Text="Data da publicação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDePublicacao" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label8" runat="server" Text="Data do depósito"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDoDeposito" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label11" runat="server" Text="Data da vigência"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDaVigencia" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label12" runat="server" Text="Data do exame"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDoExame" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblTituloPatente" runat="server" Text="Título da Patente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtTituloPatente" runat="server" Rows="5" TextMode="MultiLine"
                                                Width="100%" MaxLength="4000">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblNaturezaPatente" runat="server" Text="Natureza"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc5:ctrlNaturezaPatente ID="ctrlNaturezaPatente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblPais" runat="server" Text="País" />
                                        </td>
                                        <td class="td">
                                            <uc9:ctrlPais ID="ctrlPaisProcesso" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc6:ctrlCliente ID="ctrlCliente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarCliente" runat="server" Text="Adicionar" ToolTip="Adicionar Cliente"
                                                OnClick="btnAdicionarCliente_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                PageSize="10" GridLines="None" OnItemCommand="grdClientes_ItemCommand" OnItemCreated="grdClientes_ItemCreated"
                                                OnPageIndexChanged="grdClientes_PageIndexChanged" Skin="Vista">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="Pessoa.Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblInventor" runat="server" Text="Inventor"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc7:ctrlInventor ID="ctrlInventor" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarInventor" runat="server" Text="Adicionar" ToolTip="Adicionar Inventor"
                                                OnClick="btnAdicionarInventor_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdInventores" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                Skin="Vista" PageSize="10" GridLines="None" OnItemCommand="grdInventores_ItemCommand"
                                                OnItemCreated="grdInventores_ItemCreated" OnPageIndexChanged="grdInventores_PageIndexChanged">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="Pessoa.Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblTitular" runat="server" Text="Titular"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc8:ctrlTitular ID="ctrlTitular" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarTitular" runat="server" Text="Adicionar" ToolTip="Adicionar Titular"
                                                OnClick="btnAdicionarTitular_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdTitulares" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                Skin="Vista" PageSize="10" GridLines="None" OnItemCommand="grdTitulares_ItemCommand"
                                                OnItemCreated="grdTitulares_ItemCreated" OnPageIndexChanged="grdTitulares_PageIndexChanged">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="Pessoa.Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvComplemento" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlComplemento" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label13" runat="server" Text="Despacho"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc3:ctrlDespachoDePatentes ID="ctrlDespachoDePatentes" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label4" runat="server" Text="Processo é de terceiro?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblProcessoEhDeTerceiro" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label10" runat="server" Text="Procurador"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlProcurador ID="ctrlProcurador" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Processo é estrangeiro?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblProcessoEhEstrangeiro" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label20" runat="server" Text="Pasta"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc4:ctrlPasta ID="ctrlPasta" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblResumoDaPatente" runat="server" Text="Resumo da Patente" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtResumoDaPatente" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" MaxLength="4000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblObservacoes" runat="server" Text="Observações" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacoes" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" MaxLength="4000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblReivindicacoes" runat="server" Text="Reivindicações" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadNumericTextBox ID="txtReivindicacoes" runat="server" DataType="System.Int64"
                                                Type="Number" Width="10%" MaxLength="8">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                                </NumberFormat>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label19" runat="server" Text="É PCT?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblEHPCT" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblEHPCT_OnSelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table class="tabela" id="pnlPCT" runat="server">
                                    <tr>
                                        <td class="th3" colspan="2">
                                            <asp:Label ID="Label14" runat="server" Text="Informações do PCT"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label15" runat="server" Text="Número"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNumeroPCT" runat="server" Width="87px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label16" runat="server" Text="Data do depósito"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDoDepositoPCT" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label17" runat="server" Text="Número WO"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNumeroPCTWO" runat="server" Width="87px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label18" runat="server" Text="Data da publicação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDaPublicacaoPCT" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlPrioridadeUnionista" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td colspan="2">
                                            <fieldset class="field">
                                                <legend class="fieldlegend">
                                                    <asp:Label ID="lblTituloPrioridadeUnionista" runat="server" Text="Prioridade Unionista" />
                                                </legend>
                                                <table class="tabela">
                                                    <tr>
                                                        <td colspan="2">
                                                            <table class="tabela">
                                                                <tr>
                                                                    <td class="th3">
                                                                        <asp:Label ID="lblDataPrioridade" runat="server" Text="Data" />
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadDatePicker ID="txtDataPrioridade" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="th3">
                                                                        <asp:Label ID="lblPaisDeOrigem" runat="server" Text="País de Origem" />
                                                                    </td>
                                                                    <td class="td">
                                                                        <uc9:ctrlPais ID="ctrlPais" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="th3">
                                                                        <asp:Label ID="lblNumeroPrioridade" runat="server" Text="Número Prioridade" />
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadTextBox ID="txtNumeroPrioridade" runat="server" Width="10%" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="right">
                                                            <telerik:RadButton ID="btnAdicionarPrioridadeUnionista" runat="server" Text="Adicionar"
                                                                ToolTip="Adicionar Prioridade Unionista" OnClick="btnAdicionarPrioridadeUnionista_ButtonClick" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="campodependente" colspan="2">
                                                            <telerik:RadGrid ID="grdPrioridadeUnionista" runat="server" AutoGenerateColumns="False"
                                                                Skin="Vista" AllowPaging="True" PageSize="10" GridLines="None" OnItemCommand="grdPrioridadeUnionista_ItemCommand"
                                                                OnItemCreated="grdPrioridadeUnionista_ItemCreated" OnPageIndexChanged="grdPrioridadeUnionista_PageIndexChanged">
                                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                                <MasterTableView GridLines="Both">
                                                                    <RowIndicatorColumn>
                                                                        <HeaderStyle Width="20px" />
                                                                    </RowIndicatorColumn>
                                                                    <ExpandCollapseColumn>
                                                                        <HeaderStyle Width="20px" />
                                                                    </ExpandCollapseColumn>
                                                                    <Columns>
                                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                                        </telerik:GridButtonColumn>
                                                                        <telerik:GridDateTimeColumn DataField="DataPrioridade" HeaderText="Data" UniqueName="colunaData"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <telerik:GridBoundColumn DataField="Pais.Nome" HeaderText="País de Origem" UniqueName="colunaPais" />
                                                                        <telerik:GridBoundColumn DataField="NumeroPrioridade" HeaderText="Nº Prioridade"
                                                                            UniqueName="colunaPrioridade" />
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlClassificacao" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td colspan="2">
                                            <table class="tabela">
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblClassficacao" runat="server" Text="Classificação" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtClassificacao" runat="server" Width="100%" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblDescricaoClassificacao" runat="server" Text="Descrição da classificação" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtDescricaoClassificacao" runat="server" TextMode="MultiLine"
                                                            Rows="5" Width="100%" MaxLength="4000" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblTipoDeClassificacao" runat="server" Text="Tipo de classificação" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadComboBox ID="cboTipoDeClassificacao" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarClassificacao" runat="server" Text="Adicionar"
                                                ToolTip="Adicionar Classificação da Patente" OnClick="btnAdicionarClassificacao_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdClassificacaoPatente" runat="server" AutoGenerateColumns="False"
                                                Skin="Vista" AllowPaging="True" PageSize="10" GridLines="None" OnItemCommand="grvClassificacaoPatente_ItemCommand"
                                                OnItemCreated="grvClassificacaoPatente_ItemCreated" OnPageIndexChanged="grvClassificacaoPatente_PageIndexChanged">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="Classificacao" HeaderText="Classificação" UniqueName="colunaClassificacao" />
                                                        <telerik:GridBoundColumn DataField="DescricaoClassificacao" HeaderText="Descrição"
                                                            UniqueName="colunaDescricao" />
                                                        <telerik:GridBoundColumn DataField="TipoClassificacao.Descricao" HeaderText="Tipo Classificação"
                                                            UniqueName="colunaTipoDeClassificacao" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlRadicais" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td colspan="2">
                                            <table class="tabela">
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblRadical" runat="server" Text="Radical" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtRadical" runat="server" Width="100%" MaxLength="50" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarRadical" runat="server" Text="Adicionar" ToolTip="Adicionar Radical"
                                                OnClick="btnAdicionarRadical_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grvRadicais" runat="server" AutoGenerateColumns="False" Skin="Vista"
                                                AllowPaging="True" PageSize="10" GridLines="None" OnItemCommand="grvRadicais_ItemCommand"
                                                OnItemCreated="grvRadicais_ItemCreated" OnPageIndexChanged="grvRadicais_PageIndexChanged">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="Colidencia" HeaderText="Radical" UniqueName="colunaRadical" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvAnuidades" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlAnuidades" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblDescricaoDaAnuidade" runat="server" Text="Descrição da Anuidade" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtDescricaoDaAnuidade" MaxLength="45" runat="server" Width="100%">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblInicioPrazoPagamento" runat="server" Text="Início Prazo de Pagto" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtInicioPrazoPagamento" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblPagamentoSemMulta" runat="server" Text="Limite Pagto S/ Multa" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtPagamentoSemMulta" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblPagamentoComMulta" runat="server" Text="Limite Pagto C/ Multa" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtPagamentoComMulta" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblDataPagamento" runat="server" Text="Data Pagamento" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataPagamento" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblValorPagamento" runat="server" Text="Valor Pagamento" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadNumericTextBox ID="txtValorPagamento" runat="server" Width="10%" MaxLength="8">
                                                <NumberFormat DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnBaixar" runat="server" Text="Baixar" OnClick="btnBaixar_ButtonClick"
                                                Visible="False" />
                                            <telerik:RadButton ID="btnCancelarBaixaAnuidade" runat="server" Text="Cancelar" OnClick="btnCancelarBaixaAnuidade_ButtonClick"
                                                Visible="False" />
                                            <telerik:RadButton ID="btnGerarTodas" runat="server" Text="Gerar todas" OnClick="btnGerarTodas_ButtonClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdAnuidades" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                Skin="Vista" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grvObrigacoes_ItemCommand"
                                                OnItemCreated="grvObrigacoes_ItemCreated" OnPageIndexChanged="grvObrigacoes_PageIndexChanged"
                                                OnItemDataBound="grvObrigacoes_ItemDataBound">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                            HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="colunaExcluir" />
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Baixar" FilterImageToolTip="Baixar"
                                                            HeaderTooltip="Baixar" ImageUrl="~/imagens/yes.gif" UniqueName="colunaBaixar" />
                                                        <telerik:GridBoundColumn DataField="DescricaoAnuidade" HeaderText="Descrição Parcela"
                                                            UniqueName="colunaDescricaoAnuidade" />
                                                        <telerik:GridBoundColumn DataField="DataLancamento" HeaderText="Dt. Início" UniqueName="colunaDataLancamento"
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <telerik:GridBoundColumn DataField="DataVencimentoSemMulta" HeaderText="Dt. S/Multa"
                                                            UniqueName="colunaDataVencimentoSemMulta" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <telerik:GridBoundColumn DataField="DataVencimentoComMulta" HeaderText="Dt. C/Multa"
                                                            UniqueName="colunaDataVencimentoComMulta" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <telerik:GridBoundColumn DataField="DataPagamento" HeaderText="Dt. Pagamento" UniqueName="colunaDataPagamento"
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <telerik:GridBoundColumn DataField="ValorPagamento" HeaderText="Valor" UniqueName="colunaValorPagamento" />
                                                        <telerik:GridBoundColumn DataField="AnuidadePaga" HeaderText="Paga" UniqueName="colunaAnuidadePaga" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlManutencao" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label29" runat="server" Text="Pagamento de manutenção?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblPagaManutencao" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblPagaManutencao_OnSelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlDadosDaManutencao" runat="server">
                                    <table class="tabela">
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label30" runat="server" Text="Período"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <uc10:ctrlPeriodo ID="ctrlPeriodo" runat="server" />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="pnlMesInicioCobranca">
                                            <td class="th3">
                                                <asp:Label ID="lblMes" runat="server" Text="Mês da cobrança"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <uc11:ctrlMes ID="ctrlMes" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label31" runat="server" Text="Forma de cobrança"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <asp:RadioButtonList ID="rblFormaDeCobranca" runat="server" AutoPostBack="false"
                                                    RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="lblValor" runat="server" Text="Valor"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <telerik:RadNumericTextBox ID="txtValor" runat="server" Width="87px" Type="Currency"
                                                    DataType="System.Double">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
