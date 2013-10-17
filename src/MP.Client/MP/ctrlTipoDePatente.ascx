<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlTipoDePatente.ascx.cs"
    Inherits="MP.Client.MP.ctrlTipoDePatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboTipoDePatente" runat="server" EmptyMessage="Selecione um tipo de patente"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboTipoDePatente_ItemsRequested"
    OnSelectedIndexChanged="cboTipoDePatente_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="80%">
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
                <td width="80%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="16%">
                    <%#DataBinder.Eval(Container, "Attributes['SiglaTipo']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
