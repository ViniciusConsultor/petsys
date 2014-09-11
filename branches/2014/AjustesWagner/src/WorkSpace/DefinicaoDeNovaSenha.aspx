<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master"
    CodeBehind="DefinicaoDeNovaSenha.aspx.vb" Inherits="WorkSpace.DefinicaoDeNovaSenha" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table id="Table3" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="left">
                            <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="right" class="label">
                                        <asp:Label ID="Label5" runat="server">Nova senha:</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtNovaSenha" runat="server" TextMode="Password" Skin="Vista"
                                            MaxLength="255">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo deve ser informado"
                                            ControlToValidate="txtNovaSenha"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="label">
                                        <asp:Label ID="Label1" runat="server">Confirmação da nova senha:</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtConfirmacaoNovaSenha" runat="server" TextMode="Password"
                                            Skin="Vista" MaxLength="255">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo deve ser informado"
                                            ControlToValidate="txtConfirmacaoNovaSenha"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btnRedefinir" runat="server" CssClass="botao130X30" Text="Redefinir"
                                            ToolTip="Clique para redifir a sua senha."></asp:Button>
                                         <asp:Button ID="btnIrParaLogin" runat="server" CssClass="botao130X30" Text="Entrar"
                                            ToolTip="Clique para entrar no sistema." CausesValidation="False"></asp:Button>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
