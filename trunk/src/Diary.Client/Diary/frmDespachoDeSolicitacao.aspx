<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="frmDespachoDeSolicitacao.aspx.vb" Inherits="Diary.Client.frmDespachoDeSolicitacao" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
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
                                    <asp:Panel ID="pnlComponenteDespacho" runat="server">
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
                                <telerik:RadGrid ID="grdDespachos" runat="server" 
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                                    GridLines="None" Skin="Vista">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" 
                                                FilterImageToolTip="Modificar" HeaderTooltip="Modificar" 
                                                ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" 
                                                FilterImageToolTip="Excluir" HeaderTooltip="Excluir" 
                                                ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" 
                                                Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" 
                                                UniqueName="column30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Assunto" HeaderText="Assunto" 
                                                UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" 
                                                UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDaSolicitacao" 
                                                HeaderText="Data da solicitação" UniqueName="column1" Visible="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contato.Pessoa.Nome" HeaderText="Contato" 
                                                UniqueName="column2" Visible="True">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Despachar" 
                                                FilterImageToolTip="Despachar" HeaderTooltip="Despachar" 
                                                ImageUrl="~/imagens/accordian.gif" UniqueName="column7">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Finalizar" 
                                                FilterImageToolTip="Finalizar" HeaderTooltip="Finalizar" 
                                                ImageUrl="~/imagens/yes.gif" UniqueName="column9">
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
