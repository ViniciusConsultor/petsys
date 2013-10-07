﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="cdGrupoDeAtividade.aspx.vb" Inherits="WorkSpace.cdGrupoDeAtividade" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlGrupoDeAtividade.ascx" TagName="ctrlGrupo" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.016.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.016.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.016.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Grupo de atividade" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlGrupo ID="ctrlGrupo1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>

</asp:Content>