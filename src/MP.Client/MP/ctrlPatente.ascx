<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlPatente.ascx.cs"
    Inherits="MP.Client.MP.ctrlPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="lblPatente" runat="server" Text="Patente"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboPatente" runat="server" EmptyMessage="Selecione uma patente"
                    OnItemsRequested="cboProcurador_OnItemsRequested" EnableLoadOnDemand="True" LoadingMessage="Carregando..."
                    MarkFirstMatch="false" ShowDropDownOnTextboxClick="False" AllowCustomText="True"
                    HighlightTemplatedItems="True" Width="90%" Skin="Vista" CausesValidation="False"
                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboProcurador_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="26%">
                                    Título Patente
                                </td>
                                <td width="20%">
                                    Tipo De Patente
                                </td>
                                <td width="20%">
                                    Resumo
                                </td>
                                <td width="20%">
                                    Data Cadastro
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
                                <td width="20%">
                                    <%#DataBinder.Eval(Container, "Attributes['TipoDePatente']")%>
                                </td>
                                <td width="20%">
                                    <%#DataBinder.Eval(Container, "Attributes['Resumo']")%>
                                </td>
                                <td width="20%">
                                    <%#DataBinder.Eval(Container, "Attributes['DataDeCadastro']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
</asp:Panel>
