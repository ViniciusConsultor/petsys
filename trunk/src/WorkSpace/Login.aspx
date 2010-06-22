<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master"
    CodeBehind="Login.aspx.vb" Inherits="WorkSpace.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="titulo">
        ::
        <asp:Label ID="Label1" runat="server" Text="Login"></asp:Label>
        ::
    </div>
    <table >
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Login:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLogin" runat="server" CssClass="areadetexto" MaxLength="255"
                    Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin"
                    Display="Static" ErrorMessage="Login deve ser informado." SetFocusOnError="True"
                    ForeColor="White"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Senha:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSenha" runat="server" CssClass="areadetexto" TextMode="Password"
                    Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Static"
                    ErrorMessage="Senha deve ser informada." ControlToValidate="txtSenha" SetFocusOnError="True"
                    ForeColor="White"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
   <%-- <div class="esqueci">
        <asp:LinkButton ID="btnEsqueceuASenha" runat="server" CausesValidation="False">Esqueceu sua senha?</asp:LinkButton>
    </div>--%>
    <div>
        <asp:Button ID="btnEntrar" runat="server" CssClass="botao" Text="Entrar" />
    </div>
    
    
</asp:Content>
