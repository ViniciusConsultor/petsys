<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRelatorioDeManutencoes.aspx.cs" Inherits="MP.Client.MP.frmRelatorioDeManutencoes" 
MasterPageFile="~/WorkSpace.Master"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc5" TagName="ctrlCliente" Src="~/ctrlCliente.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista" Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Gerar Relatório" ImageUrl="~/imagens/imprimir.png" CommandName="btnRelatorio" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Limpar" ImageUrl="~/imagens/limpar.gif" CommandName="btnLimpar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Relatório de Manutenções" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente" />
                            </td>
                            <td class="td">
                                <uc5:ctrlCliente ID="ctrlCliente" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <telerik:RadButton ID="btnAdicionarCliente" runat="server" Text="Adicionar" ToolTip="Adicionar Inventor" 
                                    OnClick="btnAdicionarCliente_ButtonClick" />
                            </td>
                        </tr>
                        <tr>
                            <td class="campodependente" colspan="2">
                                <telerik:RadGrid ID="grdClientes" runat="server" AutoGenerateColumns="False" AllowPaging="True" Skin="Vista" PageSize="10" 
                                    GridLines="None" OnItemCommand="grdClientes_ItemCommand"
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
                                            <telerik:GridBoundColumn DataField="Pessoa.Nome" HeaderText="Nome" UniqueName="colunaNome" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblInicio" runat="server" Text="Início" />
                            </td>
                            <td class="td">
                                <telerik:RadDatePicker ID="rdpPeriodoInicio" runat="server" />                                
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblTermino" runat="server" Text="Término" />
                            </td>
                            <td class="td">
                                <telerik:RadDatePicker ID="rdpPeriodoTermino" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblMarcas" runat="server" Text="Marcas" />
                            </td>
                            <td class="td">
                                <asp:CheckBox ID="chkMarcas" runat="server" Checked="true"/>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblPatentes" runat="server" Text="Patentes" />
                            </td>
                            <td class="td">
                                <asp:CheckBox ID="chkPatentes" runat="server" Checked="true" />
                            </td>
                        </tr>
                    </table>                    
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
