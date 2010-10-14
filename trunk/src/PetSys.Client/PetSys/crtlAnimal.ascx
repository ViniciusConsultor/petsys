<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlAnimal.ascx.vb"
    Inherits="PetSys.Client.crtlAnimal" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlAnimal" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Nome"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboAnimal" runat="server" EmptyMessage="Selecione um animal"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="40%">
                                    Nome
                                </td>
                                <td width="16%">
                                    Nascimento
                                </td>
                                <td width="22%">
                                    Espécie
                                </td>
                                <td width="22%">
                                    Raça
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="40%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="16%">
                                    <%#DataBinder.Eval(Container, "Attributes['DataNascimento']")%>
                                </td>
                                <td width="22%">
                                    <%#DataBinder.Eval(Container, "Attributes['Especie']")%>
                                </td>
                                <td width="22%">
                                    <%#DataBinder.Eval(Container, "Attributes['Raca']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" CausesValidation="False"
                    CommandArgument="OPE.PET.002.0001" />
                <asp:ImageButton ID="btnDetalhar" runat="server" ImageUrl="~/imagens/details.gif"
                    CausesValidation="False" CommandArgument="OPE.NCL.002.0004" />
                <asp:RequiredFieldValidator ID="rfvAnimal" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboAnimal"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
