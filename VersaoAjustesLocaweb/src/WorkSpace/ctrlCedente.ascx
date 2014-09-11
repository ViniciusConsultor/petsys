<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCedente.ascx.vb" Inherits="WorkSpace.ctrlCedente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboCedente" runat="server" EmptyMessage="Selecione um cedente"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False"   AutoPostBack="True">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="96%">
                    Nome
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="100%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.NCL.019.0001" />