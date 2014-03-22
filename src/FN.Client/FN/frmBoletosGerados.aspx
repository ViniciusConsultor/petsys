<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" 
CodeBehind="frmBoletosGerados.aspx.cs" Inherits="FN.Client.FN.frmBoletosGerados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
        <telerik:RadDock ID="rdkBoletosGerados" runat="server" Title="Boletos Gerados"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                    <tr>
                            <td colspan="2">
                            <telerik:RadGrid ID="grdBoletosGerados" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdBoletosGerados_OnPageIndexChanged"
                                    OnItemCommand="grdBoletosGerados_OnItemCommand">
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
                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column11">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column12"
                                                ConfirmDialogType="RadWindow" ConfirmText="Deseja mesmo excluir o boleto gerado?"
                                                ConfirmTitle="Apagar boleto gerado">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column1"  Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cedente" HeaderText="Cedente"  UniqueName="column2">
                                            <ItemStyle Width="20%"></ItemStyle> 
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente" HeaderText="Cliente"  UniqueName="column3">
                                            <ItemStyle Width="20%"></ItemStyle> 
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NumeroBoleto" HeaderText="Num. Boleto" UniqueName="column4">
                                                <ItemStyle Width="6%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="NossoNumero" HeaderText="Nosso Número" UniqueName="column5">
                                                <ItemStyle Width="7%"></ItemStyle>
                                            </telerik:GridBoundColumn>                                            
                                            <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor R$" UniqueName="column6">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataGeracao" HeaderText="Data Geração" UniqueName="column7">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataVencimento" HeaderText="Data Vencimento" UniqueName="column8">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Observacao" HeaderText="Observação" UniqueName="column10">
                                                <ItemStyle Width="30%"></ItemStyle> 
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
