<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProcuradores.ascx.cs" Inherits="MP.Client.MP.ctrlProcuradores" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlProcuradores" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="lblProcuradores" runat="server" Text="Procuradores" />
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboProcuradores" runat="server" EmptyMessage="Selecione um procurador"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" 
                    onitemsrequested="cboProcuradores_ItemsRequested" 
                    onselectedindexchanged="cboProcuradores_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="10%">
                                    Código
                                </td>
                                <td width="46%">
                                    Nome
                                </td>                                
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="10%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="46%">
                                    <%#DataBinder.Eval(Container, "Attributes['DescricaoTipo']")%>
                                </td>                               
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>                
                <asp:RequiredFieldValidator ID="rfvProcurador" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboProcuradores"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
