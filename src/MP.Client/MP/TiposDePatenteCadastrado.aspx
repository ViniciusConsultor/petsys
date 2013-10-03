<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="TiposDePatenteCadastrado.aspx.cs" Inherits="MP.Client.MP.TiposDePatenteCadastrado" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" 
    Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.CTP.001.0001" />          
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <asp:Panel ID="PanelTpoDePatenteCadastrada" runat="server" 
            GroupingText="Tipos de patentes cadastradas">            
            <table class="tabela">
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="RadGridTipoDePatente" runat="server" AutoGenerateColumns="False" AllowPaging="True" GridLines="None" 
                        PageSize="10" Skin="Vista">
                        <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                            <MasterTableView GridLines="Both">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="DescricaoTipoDePatente" UniqueName="column1" Visible="True" HeaderText="Descrição do Tipo de Patente">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SiglaTipo" UniqueName="column2" Visible="True" HeaderText="Sigla">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                          </MasterTableView>
                        </telerik:RadGrid>
                   </td>                    
                </tr>               
            </table>
        </asp:Panel>
    </telerik:RadDockLayout>
</asp:Content>
