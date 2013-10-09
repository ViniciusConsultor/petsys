<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlLayoutRevistaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlLayoutRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="lblLayoutRevista" runat="server">Layout Revista</asp:Label>
        </td>
        <td class="td">          
            <telerik:RadComboBox ID="cboLayoutPatente" runat="server" EmptyMessage="Selecione um layout"
                Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnSelectedIndexChanged="cboLayoutPatente_SelectedIndexChanged" >
                <HeaderTemplate>
                    <table width="96%">
                        <tr>
                            <td width="10%">
                                Nome Campo
                            </td>
                            <td width="40%">
                                Descrição Resumida
                            </td>
                            <td width="16%">
                                Tamanho do campo
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
                            <td width="40%">
                                <%#DataBinder.Eval(Container, "Attributes['DescricaoResumida']")%>
                            </td>
                              <td width="16%">
                                <%#DataBinder.Eval(Container, "Attributes['TamanhoDoCampo']")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>