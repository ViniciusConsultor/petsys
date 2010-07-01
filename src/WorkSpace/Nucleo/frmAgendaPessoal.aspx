<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="frmAgendaPessoal.aspx.vb" Inherits="WorkSpace.frmAgendaPessoal" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register src="ctrlAgenda.ascx" tagname="ctrlAgenda" tagprefix="uc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Agenda" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                      <uc1:ctrlAgenda ID="ctrlAgenda1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>