<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlDespachoDeMarcas.ascx.cs"
    Inherits="MP.Client.MP.ctrlDespachoDeMarcas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlDespachoDeMarcas" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Despachos cadastrados:"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboDespachoDeMarcas" runat="server" EmptyMessage="Selecione um despacho"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboDespachoDeMarcas_ItemsRequested"
                    OnSelectedIndexChanged="cboDespachoDeMarcas_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="15%">
                                    Despacho
                                </td>
                                <td width="61%">
                                    Situação do processo
                                </td>
                                <td width="20%">
                                    Concessão do registro
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="15%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="61%">
                                    <%#DataBinder.Eval(Container, "Attributes['SituacaoProcesso']")%>
                                </td>
                                <td width="20%">
                                    <%#DataBinder.Eval(Container, "Attributes['Registro']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label1" runat="server" Text="Descrição detalhada do despacho"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PanelDescricaoDetalhada" runat="server">
        <table class="tabela">
            <tr>
                <td class="td">
                    <telerik:RadTextBox ID="txtDescricaoDetalhada" runat="server" MaxLength="4000" TextMode="MultiLine"
                        Rows="5" Width="99%" Enabled="false">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
