<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAutorizacao.aspx.vb" Inherits="WorkSpace.frmAutorizacao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlGrupo.ascx" TagName="ctrlGrupo" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Modificar" CommandName="btnModificar"
                ImageUrl="~/imagens/edit.gif" CausesValidation="False" CommandArgument="OPE.NCL.003.0001"/>
            <telerik:RadToolBarButton runat="server" Text="Salvar" CommandName="btnSalvar" ImageUrl="~/imagens/save.gif" />
            <telerik:RadToolBarButton runat="server" Text="Cancelar" CommandName="btnCancelar"
                ImageUrl="~/imagens/cancel.gif" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Grupo" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                <table class="tabela">
                    <tr>
                        <td class="th3">
                            <asp:Label ID="Label2" runat="server" Text="Grupo"></asp:Label>
                        </td>
                        <td class="td">
                            <uc1:ctrlGrupo ID="ctrlGrupo1" runat="server" />    
                        </td>
                    </tr>
                </table>
                    
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Diretivas para autorização"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlAutorizacao" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadTreeView ID="trwModulos" runat="server" CheckBoxes="True" CheckChildNodes="True"
                                        TriStateCheckBoxes="True" Skin="Vista">
                                    </telerik:RadTreeView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
