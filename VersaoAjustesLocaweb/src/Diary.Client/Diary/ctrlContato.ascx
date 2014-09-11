<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlContato.ascx.vb"
    Inherits="Diary.Client.ctrlContato" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
        </td>
        <td class="td">
            <telerik:RadComboBox ID="cboContato" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                AllowCustomText="True" HighlightTemplatedItems="True" Width="100%" Skin="Vista"
                CausesValidation="False" EmptyMessage="Selecione um contato">
                <HeaderTemplate>
                    <table width="96%">
                        <tr>
                            <td style="width: 30%;">
                                Nome
                            </td>
                            <td style="width: 24%;">
                                Telefone
                            </td>
                            <td style="width: 24%;">
                                Celular
                            </td>
                            <td style="width: 22%;">
                                Cargo
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table  width="100%">
                        <tr>
                            <td style="width: 30%;">
                                <%# DataBinder.Eval(Container, "Text")%>
                            </td>
                            <td style="width: 24%;">
                                <%#DataBinder.Eval(Container, "Attributes['Telefone']")%>
                            </td>
                            <td style="width: 24%;">
                                <%#DataBinder.Eval(Container, "Attributes['Celular']")%>
                            </td>
                            <td style="width: 22%;">
                                <%#DataBinder.Eval(Container, "Attributes['Cargo']")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
