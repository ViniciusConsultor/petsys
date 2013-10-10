<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCliente.ascx.vb"
    Inherits="WorkSpace.ctrlCliente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboCliente" runat="server" EmptyMessage="Selecione um cliente"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" OnItemsRequested="cboCliente_OnItemsRequested"  OnSelectedIndexChanged="cboCliente_OnSelectedIndexChanged"  AutoPostBack="True">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="40%">
                    Nome
                </td>
                <td width="16%">
                    Nascimento
                </td>
                <td width="22%">
                    Tel. comercial
                </td>
                <td width="22%">
                    Celular
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
                    <%#DataBinder.Eval(Container, "Attributes['DataNascimento']")%>
                </td>
                <td width="22%">
                    <%#DataBinder.Eval(Container, "Attributes['TelefoneComercial']")%>
                </td>
                <td width="22%">
                    <%#DataBinder.Eval(Container, "Attributes['TelefoneCelular']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.NCL.008.0001" />
