<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmLancamentoDeServicosPrestados.aspx.vb" Inherits="T13.Client.frmLancamentoDeServicosPrestados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Vista">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.T13.002.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.T13.002.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.T13.002.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Reaproveitar lançamento"
                CommandName="btnReaproveitar" CausesValidation="False" CommandArgument="OPE.T13.002.0005" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/imprimir.png" Text="Imprimir"
                CommandName="btnImprimir" CausesValidation="False" CommandArgument="OPE.T13.002.0004" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
            
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Cliente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlCliente" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Nome"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboCliente" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="400px" Skin="Vista"
                                        CausesValidation="False" EmptyMessage="Selecione um cliente">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkLancamentosDoCliente" runat="server" Title="Laçamentos do cliente"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlLancamentosDoCliente" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label12" runat="server" Text="Lançamentos"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboLancamentos" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="400px" Skin="Vista"
                                        CausesValidation="False" EmptyMessage="Selecione um lançamento">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock1" runat="server" Title="Dados básicos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosBasicos" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label13" runat="server" Text="Número"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtNumero" runat="server" Width="70px" DataType="System.Int16"
                                        MaxLength="9">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Data do lançamento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDoLancamento" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Natureza da operação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNaturezaDaOperacao" runat="server" MaxLength="15" Skin="Vista"
                                        Width="400px" SelectionOnFocus="CaretToBeginning">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Alíquota"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadMaskedTextBox ID="txtAliquota" Runat="server" Mask="##%">
                                    </telerik:RadMaskedTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Observações"></asp:Label>
                                </td>
                                <td class="td">
                                     <telerik:RadTextBox ID="txtObservacoes" runat="server" MaxLength="50" Skin="Vista"
                                        Width="400px" SelectionOnFocus="CaretToBeginning">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Lançamento de serviços" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlLancamentos" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Serviço prestado"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboServicosPrestados" runat="server" AutoPostBack="True"
                                        EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                        ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                        Width="400px" Skin="Vista" CausesValidation="False" EmptyMessage="Selecione um serviço prestado">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Quantidade"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtQuantidade" runat="server" DataType="System.Int16"
                                        Width="70px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Unidade"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtUnidade" runat="server" MaxLength="3" Skin="Vista" SelectionOnFocus="CaretToBeginning"
                                        Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label14" runat="server" Text="Observação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtObservacaoItem" runat="server" MaxLength="20" Skin="Vista"
                                        SelectionOnFocus="CaretToBeginning" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Valor do serviço prestado"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtValorDoServico" runat="server">
                                    </telerik:RadNumericTextBox>
                                    <asp:Button ID="btnAdicionarItem" runat="server" Text="Adicionar item" CssClass="RadUploadSubmit" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="grdItensLancados" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" Skin="Vista">
                                        <MasterTableView GridLines="Both">
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column7"
                                                    ImageUrl="~/imagens/delete.gif">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="Servico.ID" UniqueName="column" Visible="False"
                                                    HeaderText="ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Quantidade" UniqueName="column1" Visible="True"
                                                    HeaderText="Quantidade">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Unid." UniqueName="column2" Visible="True" HeaderText="Unidade">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Servico.Nome" UniqueName="column3" HeaderText="Discriminação">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Observacao" UniqueName="column6" HeaderText="Observacão">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Valor" UniqueName="column4" HeaderText="Valor unitário">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Total" UniqueName="column5" HeaderText="Total">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Label ID="Label11" runat="server" Text="Valor TOTAL"></asp:Label>
                                    <asp:Label ID="lblValorTotal" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="2" align="right">
                                    <asp:Label ID="Label8" runat="server" Text="ISSQN"></asp:Label>
                                    <asp:Label ID="lblISSQN" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
