<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdProcedimentosInternos.aspx.cs" Inherits="MP.Client.MP.cdProcedimentosInternos" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="ctrlProcedimentosInternos.ascx" TagName="ctrlProcedimentosInternos" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" 
    Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.003.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.003.0002" />           
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
                 <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.003.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>

    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">           
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro de procedimentos internos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked" Height="450px" 
                Width="260px">
                <ContentTemplate>
                    <uc1:ctrlProcedimentosInternos ID="ctrlProcedimentosInternos" runat="server" />
                    <asp:Panel ID="PanelCdProcedimentosInternos" runat="server">
                    <table class="tabela">
                        <tr>
                            <td>
                                <asp:Panel ID="PanelTipoDeProcedimento" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3" style="width: 2%">
                                        <asp:Label ID="Label1" runat="server" Text="Tipo de procedimento:"></asp:Label>
                                        &nbsp;
                                        </td>
                                        <td class="td" style="width: 8%">
                                        <asp:TextBox ID="txtDescricaoTipo" runat="server" Width="350px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="th3"
                                                    ErrorMessage="Campo deve ser informado." 
                                                    ControlToValidate="txtDescricaoTipo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>                        
                                </table>
                                </asp:Panel>
                             </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    </ContentTemplate>
            </telerik:RadDock>      
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
