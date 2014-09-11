<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="EsqueceuSenha.aspx.vb" Inherits="WorkSpace.EsqueceuSenha" %>
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
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <br/>
                                        <asp:Button ID="btnEnviarEmail" runat="server" CssClass="botao130X30" Text="Definir nova senha" ToolTip="Clique para enviar um e-mail para definição de nova senha.">
                                        </asp:Button>
                                          <asp:Button ID="btnSair" runat="server" CssClass="botao130X30" Text="Sair" ToolTip="Clique para voltar para o login." CausesValidation="False">
                                        </asp:Button>
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
