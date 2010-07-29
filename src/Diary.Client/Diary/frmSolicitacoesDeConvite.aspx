<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmSolicitacoesDeConvite.aspx.vb" Inherits="Diary.Client.frmSolicitacoesDeConvite" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.DRY.003.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/imprimir.png" Text="Imprimir"
                CommandName="btnImprimir" CausesValidation="False" CommandArgument="OPE.DRY.003.0006" />
            <telerik:RadToolBarButton runat="server" Text="Recarregar" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnRecarregar" CausesValidation="False" />
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
                                    <asp:RadioButtonList ID="rblOpcaoFiltro" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                    </asp:RadioButtonList>
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
                                    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/imagens/find.gif" />
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
                                    <asp:ImageButton ID="btnPesquisarPorCodigo" runat="server" ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlContato">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Contato"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboContato" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="480px" Skin="Vista"
                                        CausesValidation="False" EmptyMessage="Selecione um contato">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 200px;">
                                                        Nome
                                                    </td>
                                                    <td style="width: 80px;">
                                                        Telefone
                                                    </td>
                                                    <td style="width: 200px;">
                                                        Celular
                                                    </td>
                                                    <td style="width: 200px;">
                                                        Cargo
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td style="width: 200px;">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td style="width: 80px;">
                                                        <%#DataBinder.Eval(Container, "Attributes['Telefone']")%>
                                                    </td>
                                                    <td style="width: 200px;">
                                                        <%#DataBinder.Eval(Container, "Attributes['Celular']")%>
                                                    </td>
                                                    <td style="width: 200px;">
                                                        <%#DataBinder.Eval(Container, "Attributes['Cargo']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                    <asp:ImageButton ID="btnPesquisarPorContato" runat="server" ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Considerar solicitações finalizadas?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:CheckBox ID="chkConsiderarSolicitacoesFinalizadas" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkLancamentos" runat="server" Title="Solicitações de convites ativas"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdItensLancados" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="10" GridLines="None" Skin="Vista">
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
                                            <telerik:GridBoundColumn DataField="DataEHorario" HeaderText="Data e hora" UniqueName="column33">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Local" HeaderText="Local" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDaSolicitacao" HeaderText="Data da solicitação"
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
