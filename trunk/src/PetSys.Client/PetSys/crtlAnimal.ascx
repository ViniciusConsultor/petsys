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
                <telerik:RadComboBox ID="cboAnimal" runat="server" EmptyMessage="Selecione uma animal"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="480px" Height="300px" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                    <HeaderTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 200px;">
                                    Nome
                                </td>
                                <td style="width: 80px;">
                                    Data de nascimento
                                </td>
                                <td style="width: 100px;">
                                    Espécie
                                </td>
                                <td style="width: 100px;">
                                    Raça
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 200px;">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 80px;">
                                    <%#DataBinder.Eval(Container, "Attributes['DataNascimento']")%>
                                </td>
                                <td style="width: 100px;">
                                    <%#DataBinder.Eval(Container, "Attributes['Especie']")%>
                                </td>
                                <td style="width: 100px;">
                                    <%#DataBinder.Eval(Container, "Attributes['Raca']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="imagens/new.gif" CausesValidation="False"
                    CommandArgument="OPE.PET.002.0001" />
                <asp:ImageButton ID="btnDetalhar" runat="server" ImageUrl="imagens/details.gif" CausesValidation="False"
                    CommandArgument="OPE.NCL.002.0004" />
                <asp:RequiredFieldValidator ID="rfvAnimal" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboAnimal"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
