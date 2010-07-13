<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAgendaPessoal.aspx.vb" Inherits="WorkSpace.frmAgendaPessoal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register Src="ctrlAgenda.ascx" TagName="ctrlAgenda" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <uc1:ctrlAgenda ID="ctrlAgenda1" runat="server" />
    <asp:Timer ID="Timer1" runat="server" Interval="30000">
    </asp:Timer>
    <asp:Label ID="lblInconsistencia" runat="server" Text=""></asp:Label>
</asp:Content>
