<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="TiposDePatenteCadastrado.aspx.cs" Inherits="MP.Client.MP.TiposDePatenteCadastrado" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" 
    Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.CTP.001.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.CTP.001.0002" />            
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <asp:Panel ID="PanelTpoDePatenteCadastrada" runat="server" 
            GroupingText="Tipos de patentes cadastradas">            
            <table class="tabela">
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="RadGridTipoDePatente" runat="server" AutoGenerateColumns="False" GridLines="None" Skin="Vista">
                            <MasterTableView GridLines="Both">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="DESCRICAO_TIPO_PATENTE" UniqueName="column1" Visible="True" HeaderText="Descrição do Tipo de Patente">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SIGLA_TIPO" UniqueName="column2" Visible="True" HeaderText="Sigla">
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
