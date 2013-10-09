<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdLayoutRevistaPatente.aspx.cs" Inherits="MP.Client.MP.cdLayoutRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlLayoutRevistaPatente.ascx" TagName="ctrlLayoutRevistaPatente" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.006.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.006.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.006.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro Layout Revista Patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlLayoutRevistaPatente ID="ctrlLayoutRevistaPatente" runat="server" />
                    <asp:Panel ID="PanelDadosDoLayout" runat="server">                        
                        <table class="tabela">                                    
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNomeDoCampo" runat="server" Text="Nome campo" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNomeDoCampo" MaxLength="50" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDescricaoResumida" runat="server" Text="Descrição Resumida" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricaoResumida" MaxLength="50" runat="server" Width="80%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDescricaoDoCampo" runat="server" Text="Descrição do campo" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricaoDoCampo" MaxLength="120" Width="80%" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTamanhoDoCampo" runat="server" Text="Tamanho do campo" />
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtTamanhoDoCampo" runat="server" MaxLength="5" Width="12%">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCampoDelimitadorDoRegistro" runat="server" Text="Este campo identifica o início do Registro na Revista?" />
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboCampoDelimitadorDoRegistro" Culture="pt-BR" runat="server" Width="10%"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCampoIdentificadorDoProcesso" runat="server" Text="Este campo identifica o Número do Processo na Revista do Registro na Revista?" />
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboCampoIdentificadorDoProcesso" runat="server" Culture="pt-BR" Width="10%"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCampoIdentificadorDeColidencia" runat="server" Text="Este campo identifica a Colidência do Processo na Revista?" />
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboCampoIdentificadorDeColidencia" runat="server" Culture="pt-BR" Width="10%"></telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>                    
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
