<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdPatente.aspx.cs" Inherits="MP.Client.MP.cdPatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/MP/ctrlPatente.ascx" TagName="ctrlPatente" TagPrefix="uc1" %>
<%@ Register Src="~/MP/ctrlNaturezaPatente.ascx" TagName="ctrlNaturezaPatente" TagPrefix="uc2" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc3" %>
<%@ Register Src="~/MP/ctrlInventor.ascx" TagName="ctrlInventor" TagPrefix="uc4" %>
<%@ Register Src="~/ctrlPais.ascx" TagName="ctrlPais" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Width="100%" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.006.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.006.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.006.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="rdlPatente" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="rdComboPatente" runat="server" Title="Patentes cadastradas" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Patente"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlPatente ID="ctrlPatente" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Cadastro de Patentes" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Patente" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Complemento">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Anuidade">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="rpvDadosPatentes" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosPatente" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblTituloPatente" runat="server" Text="Título da Patente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtTituloPatente" runat="server" Rows="5" TextMode="MultiLine"
                                                Width="100%" MaxLength="500">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblNaturezaPatente" runat="server" Text="Natureza"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlNaturezaPatente ID="ctrlNaturezaPatente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc3:ctrlCliente ID="ctrlCliente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
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
                                            <br/>                                                  
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
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblInventor" runat="server" Text="Inventor"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc4:ctrlInventor ID="ctrlInventor" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarInventor" runat="server" Text="Adicionar" ToolTip="Adicionar Inventor" 
                                            OnClick="btnAdicionarInventor_ButtonClick"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
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
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3" colspan="2">     
                                            <asp:Label ID="lblTituloPrioridadeUnionista" runat="server" Text="Prioridade Unionista"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
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
                                                        <uc5:ctrlPais ID="ctrlPais" runat="server" />
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
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarPrioridadeUnionista" runat="server" Text="Adicionar" ToolTip="Adicionar Prioridade Unionista" 
                                            OnClick="btnAdicionarPrioridadeUnionista_ButtonClick"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
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
                                                        <telerik:GridDateTimeColumn DataField="DataPrioridade" HeaderText="Data" UniqueName="colunaData"/>
                                                        <telerik:GridBoundColumn DataField="Pais.Nome" HeaderText="País de Origem" UniqueName="colunaPais" />
                                                        <telerik:GridBoundColumn DataField="NumeroPrioridade" HeaderText="Nº Prioridade" UniqueName="colunaPrioridade" />
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
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblResumoDaPatente" runat="server" Text="Resumo da Patente" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtResumoDaPatente" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" MaxLength="1000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblObservacoes" runat="server" Text="Observações" />
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacoes" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" MaxLength="250" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <table class="tabela">
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblClassficacao" runat="server" Text="Classificação" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtClassificacao" runat="server" Width="100%"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblDescricaoClassificacao" runat="server" Text="Descrição da classificação" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtDescricaoClassificacao" runat="server" TextMode="MultiLine" Rows="5" Width="100%" />
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
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarClassificacao" runat="server" Text="Adicionar" ToolTip="Adicionar Classificação da Patente" 
                                            OnClick="btnAdicionarClassificacao_ButtonClick"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
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
                                                        <telerik:GridBoundColumn DataField="DescricaoClassificacao" HeaderText="Descrição" UniqueName="colunaDescricao" />
                                                        <telerik:GridBoundColumn DataField="TipoClassificacao.Descricao" HeaderText="Tipo Classificação" UniqueName="colunaTipoDeClassificacao" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
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
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <table class="tabela">
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblRadical" runat="server" Text="Radical" />
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadTextBox ID="txtRadical" runat="server" Width="100%"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnAdicionarRadical" runat="server" Text="Adicionar" ToolTip="Adicionar Radical" 
                                            OnClick="btnAdicionarRadical_ButtonClick"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grvRadicais" runat="server" AutoGenerateColumns="False"
                                                Skin="Vista" AllowPaging="True" PageSize="10" GridLines="None" OnItemCommand="grvRadicais_ItemCommand"
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
                                                        <telerik:GridBoundColumn DataField="string" HeaderText="Radical" UniqueName="colunaRadical" />
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
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
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
                                                <NumberFormat DecimalDigits="2"/>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <telerik:RadButton ID="btnNovaAnuidade" runat="server" Text="Adicionar" OnClick="btnNovaAnuidade_ButtonClick" />
                                            <telerik:RadButton ID="btnBaixar" runat="server" Text="Baixar" OnClick="btnBaixar_ButtonClick" Visible="False" />
                                            <telerik:RadButton ID="btnGerarTodas" runat="server" Text="Gerar todas" OnClick="btnGerarTodas_ButtonClick"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">     
                                            <br/>                                                  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2">
                                            <telerik:RadGrid ID="grdAnuidades" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                Skin="Vista" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grvObrigacoes_ItemCommand"
                                                OnItemCreated="grvObrigacoes_ItemCreated" OnPageIndexChanged="grvObrigacoes_PageIndexChanged" OnItemDataBound="grvObrigacoes_ItemDataBound">
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
                                                        <telerik:GridBoundColumn DataField="DescricaoAnuidade" HeaderText="Descrição Parcela" UniqueName="colunaDescricaoAnuidade" />
                                                        <telerik:GridBoundColumn DataField="DataLancamento" HeaderText="Dt. Início" UniqueName="colunaDataLancamento" />
                                                        <telerik:GridBoundColumn DataField="DataVencimentoSemMulta" HeaderText="Dt. S/Multa" UniqueName="colunaDataVencimentoSemMulta" />
                                                        <telerik:GridBoundColumn DataField="DataVencimentoComMulta" HeaderText="Dt. C/Multa" UniqueName="colunaDataVencimentoComMulta" />
                                                        <telerik:GridBoundColumn DataField="DataPagamento" HeaderText="Dt. Pagamento" UniqueName="colunaDataPagamento" />
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
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
