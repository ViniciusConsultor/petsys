<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdPatente.aspx.cs" Inherits="MP.Client.MP.cdPatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/MP/ctrlPatente.ascx" TagName="ctrlPatente" TagPrefix="uc1" %>
<%@ Register Src="~/MP/ctrlTipoDePatente.ascx" TagName="ctrlTipoDePatente" TagPrefix="uc2" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc3" %>
<%@ Register Src="~/MP/ctrlInventor.ascx" TagName="ctrlInventor" TagPrefix="uc4" %>
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
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Patentes cadastradas" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td>
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
                                        <td class="th3">
                                            <asp:Label ID="lblTituloPatente" runat="server" Text="Título da Patente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtTituloPatente" runat="server" Rows="5" TextMode="MultiLine"
                                                Width="100%">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblTipoDaPatente" runat="server" Text="Tipo da Patente"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlTipoDePatente ID="ctrlTipoDePatente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table class="tabela">
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
                                                        <telerik:RadGrid ID="grdClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                            PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdClientes_ItemCommand"
                                                            OnItemCreated="grdClientes_ItemCreated" OnPageIndexChanged="grdClientes_PageIndexChanged">
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
                                                                    <telerik:GridBoundColumn DataField="CPF" HeaderText="CNPJ/CPF" UniqueName="colunaCNPJCPF"
                                                                        Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table class="tabela">
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
                                                        <telerik:RadGrid ID="grdInventores" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                            PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdInventores_ItemCommand"
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
                                                                    <telerik:GridBoundColumn DataField="CPF" HeaderText="CNPJ/CPF" UniqueName="colunaCNPJCPF"
                                                                        Visible="false" />
                                                                    <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdPrioridadeUnionista" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdPrioridadeUnionista_ItemCommand"
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
                                                        <telerik:GridDateTimeColumn DataField="Data" HeaderText="Data" UniqueName="colunaData"
                                                            Visible="false" />
                                                        <telerik:GridBoundColumn DataField="Pais" HeaderText="País de Origem" UniqueName="colunaPais" />
                                                        <telerik:GridBoundColumn DataField="Prioridade" HeaderText="Nº Prioridade" UniqueName="colunaPrioridade" />
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
                                            <asp:Label ID="lblResumoDaPatente" runat="server" Text="Resumo da Patente" />
                                        </td>
                                        <td class="th3">
                                            <telerik:RadTextBox ID="txtResumoDaPatente" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblObservacoes" runat="server" Text="Observações" />
                                        </td>
                                        <td class="th3">
                                            <telerik:RadTextBox ID="txtObservacoes" runat="server" TextMode="MultiLine" Rows="5"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdClassificacaoPatente" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grvClassificacaoPatente_ItemCommand"
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
                                                        <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="colunaDescricao" />
                                                        <telerik:GridBoundColumn DataField="TipoDeClassificacao" HeaderText="Tipo Classificação"
                                                            UniqueName="colunaTipoDeClassificacao" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="lblReivindicacoes" runat="server" Text="Reivindicações" />
                                        </td>
                                        <td class="th3">
                                            <telerik:RadNumericTextBox ID="txtReivindicacoes" runat="server" DataType="System.Int64"
                                                Type="Number">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                                </NumberFormat>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvAnuidades" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlAnuidades" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btnNovaAnuidade" runat="server" Text="Nova" />
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnBaixar" runat="server" Text="Baixar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdAnuidades" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                PageSize="10" GridLines="None" Width="98%" OnItemCommand="grvObrigacoes_ItemCommand"
                                                OnItemCreated="grvObrigacoes_ItemCreated" OnPageIndexChanged="grvObrigacoes_PageIndexChanged">
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
                                                        <telerik:GridBoundColumn DataField="DescricaoParcela" HeaderText="Classificação"
                                                            UniqueName="colunaClassificacao" />
                                                        <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="colunaDescricao" />
                                                        <telerik:GridBoundColumn DataField="TipoDeClassificacao" HeaderText="Tipo Classificação"
                                                            UniqueName="colunaTipoDeClassificacao" />
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
