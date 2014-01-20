<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlAnimalResumido.ascx.vb"
    Inherits="PetSys.Client.crtlAnimalResumido" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblAnimal" runat="server" Text=""></asp:Label>
        </td>
        <td class="th3">
            <asp:Label ID="Label6" runat="server" Text="Sexo"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblSexo" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="th3">
            <asp:Label ID="Label1" runat="server" Text="Data de nascimento"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblDataDeNascimento" runat="server" Text=""></asp:Label>
        </td>
        <td class="th3">
            <asp:Label ID="Label5" runat="server" Text="Idade"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblIdade" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="th3">
            <asp:Label ID="Label3" runat="server" Text="Espécie"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblEspecie" runat="server" Text=""></asp:Label>
        </td>
        <td class="th3">
            <asp:Label ID="Label4" runat="server" Text="Raça"></asp:Label>
        </td>
        <td class="td">
            <asp:Label ID="lblRaca" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
