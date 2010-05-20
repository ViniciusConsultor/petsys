<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPessoa.ascx.vb"
    Inherits="WorkSpace.ctrlPessoa" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlPessoa" runat="server">
    <table class="tabela">
        <tr id="pnlTipoDePessoa" runat="server">
            <td class="th3">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="rblTipo" Text="Tipo"></asp:Label>
            </td>
            <td class="td">
                <asp:RadioButtonList ID="rblTipo" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" CellPadding="0" CellSpacing="2">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Nome"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboPessoaFisica" runat="server" EmptyMessage="Selecione uma pessoa"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="480px" Skin="Vista" CausesValidation="False" OnItemsRequested="cboPessoaFisica_ItemsRequested"
                    OnSelectedIndexChanged="cboPessoaFisica_SelectedIndexChanged" AutoPostBack="True">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td style="width: 200px;">
                                    Nome
                                </td>
                                <td style="width: 80px;">
                                    Data de nascimento
                                </td>
                                <td style="width: 200px;">
                                    Nome da mãe
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 200px;">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 80px;">
                                    <%#DataBinder.Eval(Container, "Attributes['DataNascimento']")%>
                                </td>
                                <td style="width: 200px;">
                                    <%#DataBinder.Eval(Container, "Attributes['NomeMae']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>                
                <telerik:RadComboBox ID="cboPessoaJuridica" runat="server" EmptyMessage="Selecione uma pessoa"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="480px" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td style="width: 400px;">
                                    Razão social
                                </td>
                                 <td style="width: 200px;">
                                    Nome fantasia
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 400px;">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                 <td style="width: 200px;">
                                    <%#DataBinder.Eval(Container, "Attributes['NomeFantasia']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="imagens/new.gif" CausesValidation="False" CommandArgument="OPE.NCL.006.0001" />
                <asp:ImageButton ID="btnDetalhar" runat="server" ImageUrl="imagens/details.gif" CausesValidation="False" CommandArgument="OPE.NCL.006.0002" />
                <asp:RequiredFieldValidator ID="rfvPessoaFisica" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboPessoaFisica"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
