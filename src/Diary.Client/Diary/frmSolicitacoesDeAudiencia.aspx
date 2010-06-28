<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmSolicitacoesDeAudiencia.aspx.vb" Inherits="Diary.Client.frmSolicitacoesDeAudiencia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Vista">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Pesquisa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
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
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkLancamentos" runat="server" Title="Laçamentos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
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
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Agendar" UniqueName="column7"
                                                    ImageUrl="~/imagens/delete.gif">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column7"
                                                    ImageUrl="~/imagens/delete.gif">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="Solicitacao.ID" UniqueName="column" Visible="False"
                                                    HeaderText="ID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitacao.DataDaSolicitacao" UniqueName="column1" Visible="True"
                                                    HeaderText="Data da solicitação">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitacao.Contato.Pessoa.Nome" UniqueName="column2" Visible="True" HeaderText="Contato">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitacao.Assunto" UniqueName="column3" HeaderText="Assunto">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitacao.Descricao" UniqueName="column6" HeaderText="Descrição">
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