<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlBancosEAgencias.ascx.vb"
    Inherits="WorkSpace.ctrlBancosEAgencias" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlBancosEAgencias" runat="server">
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Banco"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboBancos" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                    LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
                    AllowCustomText="True" HighlightTemplatedItems="True" Width="500px" Skin="Vista"
                    CausesValidation="False" EmptyMessage="Selecione um banco" MaxLength="50">
                    <HeaderTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 150px;">
                                    Nome
                                </td>
                                <td style="width: 300px;">
                                    Número
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 150px;">
                                    <%#DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 300px;">
                                    <%#DataBinder.Eval(Container, "Attributes['Numero']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label2" runat="server" Text="Agência"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboAgencias" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                    LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
                    AllowCustomText="True" HighlightTemplatedItems="True" Width="500px" Skin="Vista"
                    CausesValidation="False" EmptyMessage="Selecione uma agência" MaxLength="50">
                    <HeaderTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 150px;">
                                    Nome
                                </td>
                                <td style="width: 300px;">
                                    Número
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 450px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 150px;">
                                    <%#DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td style="width: 300px;">
                                    <%#DataBinder.Eval(Container, "Attributes['Numero']")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label1" runat="server" Text="Número da conta"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadTextBox ID="txtNumeroDaConta" runat="server" MaxLength="50" Skin="Vista"
                    Width="150px" SelectionOnFocus="CaretToBeginning">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label3" runat="server" Text="Tipo da conta"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadNumericTextBox ID="txtTipoConta" runat="server" DataType="System.Int16"
                    MaxLength="3" Width="65px">
                    <NumberFormat DecimalDigits="0" />
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>
</asp:Panel>
