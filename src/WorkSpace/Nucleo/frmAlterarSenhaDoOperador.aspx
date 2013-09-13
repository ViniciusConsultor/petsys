<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAlterarSenhaDoOperador.aspx.vb" Inherits="WorkSpace.frmAlterarSenhaDoOperador" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:radtoolbar id="rtbToolBar" runat="server" autopostback="True" skin="Vista"
        style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:radtoolbar>
    <telerik:raddocklayout id="RadDockLayout1" runat="server" skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="pnlOperador" runat="server" Title="Operadores" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                     <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Operador"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadComboBox ID="cboOperador" runat="server" EmptyMessage="Selecione um operador"
                                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                                    <HeaderTemplate>
                                        <table width="96%">
                                            <tr>
                                                <td width="43%">
                                                    Nome
                                                </td>
                                                <td width="43%">
                                                    Login
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td width="50%">
                                                    <%#DataBinder.Eval(Container, "Attributes['Login']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                                </td>
                                </tr>
                                </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="pnlSenha" runat="server" Title="Senha" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Nova senha"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNovaSenha" runat="server" TextMode="Password" Skin="Vista"
                                    MaxLength="255">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Confirmação da nova senha"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtConfirmacaoNovaSenha" runat="server" TextMode="Password"
                                    Skin="Vista" MaxLength="255">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:raddocklayout>
</asp:Content>
