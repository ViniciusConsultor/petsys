<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="frmAlterarSenha.aspx.vb" Inherits="WorkSpace.frmAlterarSenha" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Senha" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Senha antiga"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtSenhaAntiga" runat="server" TextMode="Password" Skin="Vista" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="Campo deve ser informado" ControlToValidate="txtSenhaAntiga"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Nova senha"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNovaSenha" runat="server" TextMode="Password" Skin="Vista" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ErrorMessage="Campo deve ser informado" ControlToValidate="txtNovaSenha"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Confirmação da nova senha"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtConfirmacaoNovaSenha" runat="server" TextMode="Password" Skin="Vista" MaxLength="255">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ErrorMessage="Campo deve ser informado" 
                                    ControlToValidate="txtConfirmacaoNovaSenha"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
