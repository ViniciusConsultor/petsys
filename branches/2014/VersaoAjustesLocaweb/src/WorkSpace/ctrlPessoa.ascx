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
                    RepeatLayout="Flow">
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
                    Width="90%" Skin="Vista" CausesValidation="False" OnItemsRequested="cboPessoaFisica_ItemsRequested"
                    OnSelectedIndexChanged="cboPessoaFisica_SelectedIndexChanged" AutoPostBack="True" >
                    <HeaderTemplate >
                        <table width="96%" >
                            <tr>
                                <td width="40%">
                                    Nome
                                </td>
                                <td width="16%" >
                                    Nascimento
                                </td>
                                <td width="22%">
                                    CPF
                                </td>
                                <td width="22%">
                                    RG
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table  width="100%">
                            <tr>
                                <td width="40%" >
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="16%">
                                    <%#DataBinder.Eval(Container, "Attributes['DataNascimento']")%>
                                </td>
                                <td width="22%">
                                    <%#DataBinder.Eval(Container, "Attributes['CPF']")%>
                                </td>
                                <td width="22%">
                                    <%#DataBinder.Eval(Container, "Attributes['RG']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <telerik:RadComboBox ID="cboPessoaJuridica" runat="server" EmptyMessage="Selecione uma pessoa"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                    <HeaderTemplate>
                        <table  width="96%">
                            <tr>
                                <td width="50%">
                                    Razão social
                                </td>
                                <td width="50%">
                                    Nome fantasia
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table  width="100%">
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="50%">
                                    <%#DataBinder.Eval(Container, "Attributes['NomeFantasia']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="imagens/new.gif" ToolTip="Novo" CausesValidation="False"
                    CommandArgument="OPE.NCL.006.0001" />
                <asp:ImageButton ID="btnDetalhar" runat="server" ImageUrl="imagens/details.gif" ToolTip="Detalhar" CausesValidation="False"
                    CommandArgument="OPE.NCL.006.0002" />
            </td>
        </tr>
    </table>
</asp:Panel>
