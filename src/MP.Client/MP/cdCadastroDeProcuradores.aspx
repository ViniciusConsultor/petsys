<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdCadastroDeProcuradores.aspx.cs" Inherits="MP.Client.MP.cdCadastroDeProcuradores" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<%@ Register Src="ctrlProcuradores.ascx" TagName="ctrlProcuradores" TagPrefix="uc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.005.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.005.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.005.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cliente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc2:ctrlProcuradores ID="ctrlProcuradores" runat="server"/>                                        
                    <asp:Panel ID="PanelDadosDoProcurador" runat="server">                        
                    <uc1:ctrlPessoa ID="ctrlPessoa" runat="server" />
                        <table>                                    
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblMatriculaAPI" runat="server" Text="Matrícula API" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtMatriculaAPI" MaxLength="10" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblSiglaOrgao" runat="server" Text="Sigla Orgão" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtSiglaOrgao" MaxLength="10" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNumeroRegistro" runat="server" Text="Número Registro" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroRegistro" MaxLength="17" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataRegistro" runat="server" Text="Data Registro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDateInput ID="txtDataRegistro" runat="server"> </telerik:RadDateInput>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblContato" runat="server" Text="Contato" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtContato" MaxLength="20" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>                    
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
