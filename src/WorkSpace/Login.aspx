<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master"
    CodeBehind="Login.aspx.vb" Inherits="WorkSpace.Login" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table id="Table3" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="left">
                            <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="right" class="label">
                                        <asp:Label ID="Label5" runat="server">Login:</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLogin" runat="server" CssClass="areadetexto" MaxLength="255"
                                            Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Static"
                                            ErrorMessage="Login deve ser informado." ControlToValidate="txtLogin" SetFocusOnError="True"
                                            ForeColor="White"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="label">
                                        <asp:Label ID="Label4" runat="server">Senha:</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSenha" runat="server" CssClass="areadetexto" TextMode="Password"
                                            Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Static"
                                            ErrorMessage="Senha deve ser informada." ControlToValidate="txtSenha" SetFocusOnError="True"
                                            ForeColor="White"></asp:RequiredFieldValidator>
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
                                        <asp:Button ID="btnEntrar" runat="server" CssClass="botao" Text="Iniciar" ToolTip="Clique para iniciar a sessão.">
                                        </asp:Button>
                                        <asp:Button ID="btnLimpar" runat="server" CssClass="botao" Text="Limpar" ToolTip="Clique para limpar o conteúdo dos campos.">
                                        </asp:Button>
                                        <%-- <br>
                                            <asp:LinkButton ID="btnEsqueciMinhaSenha" runat="server" Visible="True" Width="100%"
                                                Font-Underline="True" ForeColor="Blue">Esqueci minha senha</asp:LinkButton>
                                            <br></br>
                                        </br>--%>
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
