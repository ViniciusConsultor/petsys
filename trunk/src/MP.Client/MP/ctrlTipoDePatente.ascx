<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlTipoDePatente.ascx.cs" Inherits="MP.Client.MP.ctrlTipoDePatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlTipoDePatente" runat="server">
<table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Tipo de patente:"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboTipoDePatente" runat="server" EmptyMessage="Selecione um tipo de patente"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" 
                    onitemsrequested="cboTipoDePatente_ItemsRequested" 
                    onselectedindexchanged="cboTipoDePatente_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="40%">
                                    Descrição do tipo da patente
                                </td>
                                <td width="16%">
                                    Sigla
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
                                    <%#DataBinder.Eval(Container, "Attributes['SiglaTipo']")%>
                                </td>                               
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" CausesValidation="False"
                    CommandArgument="OPE.CTP.001.0001" onclick="btnNovo_Click" />
                <asp:ImageButton ID="btnDetalhar" runat="server" ImageUrl="~/imagens/details.gif"
                    CausesValidation="False" CommandArgument="OPE.CTP.001.0004" 
                    onclick="btnDetalhar_Click" />
                <asp:RequiredFieldValidator ID="rfvTipoDePatente" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboTipoDePatente"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>

