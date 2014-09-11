<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmRelatorioDeProcessosDeMarcas.aspx.cs" Inherits="MP.Client.MP.frmRelatorioDeProcessosDeMarcas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc1" %>
<%@ Register Src="~/ctrlGrupoDeAtividade.ascx" TagName="ctrlGrupoDeAtividade" TagPrefix="uc2" %>
<%@ Register Src="ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Gerar" ImageUrl="~/imagens/imprimir.png"
                CommandName="btnImprimir" CausesValidation="False" CommandArgument="OPE.MP.012.0001"  />
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
                                    <asp:Label ID="Label7" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlCliente ID="ctrlCliente" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Grupo de atividade"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlGrupoDeAtividade ID="ctrlGrupoDeAtividade" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Despachos"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc3:ctrlDespachoDeMarcas ID="ctrlDespacho" runat="server" /><asp:ImageButton ID="btnAdicionarDespacho"                                       
                                    runat="server" ImageUrl="~/imagens/add.gif" ToolTip="Adicionar despacho na lista" OnClick="btnAdicionarDespacho_OnClick" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td" colspan="2">
                                 <telerik:RadGrid ID="grdDespachos" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="5" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdDespachos_OnPageIndexChanged" OnItemCommand="grdDespachos_OnItemCommand" >
                                    
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
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="IdDespacho" HeaderText="ID" UniqueName="column1" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CodigoDespacho" HeaderText="Código do despacho" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
