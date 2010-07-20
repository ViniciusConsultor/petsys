<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmDespachoDeSolicitacao.aspx.vb" Inherits="Diary.Client.frmDespachoDeSolicitacao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlDespachoAgenda.ascx" TagName="ctrlDespachoAgenda" TagPrefix="uc1" %>
<%@ Register Src="ctrlDespachoTarefa.ascx" TagName="ctrlDespachoTarefa" TagPrefix="uc2" %>
<%@ Register Src="../ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc3" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Alvo do despacho" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc3:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock1" runat="server" Title="Despacho" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDespacho" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo de despacho"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboDespacho" runat="server" Skin="Vista" AutoPostBack="True">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlComponenteDespachoAgenda" runat="server">
                                        <uc1:ctrlDespachoAgenda ID="ctrlDespachoAgenda1" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlComponenteDespachoTarefa" runat="server">
                                        <uc2:ctrlDespachoTarefa ID="ctrlDespachoTarefa1" runat="server" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Despachos da solicitação" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdDespachos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="10" GridLines="None" Skin="Vista">
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
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDoDespacho" HeaderText="Data do despacho" UniqueName="column30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tipo.Descricao" HeaderText="Tipo" UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Solicitante.Nome" HeaderText="Pessoa solicitante" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Alvo.Nome" HeaderText="Pessoa alvo"
                                                UniqueName="column1" Visible="True">
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
