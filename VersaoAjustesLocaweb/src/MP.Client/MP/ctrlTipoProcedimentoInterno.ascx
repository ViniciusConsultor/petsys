<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlTipoProcedimentoInterno.ascx.cs"
    Inherits="MP.Client.MP.ctrlTipoProcedimentoInterno" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboProcedimentosInternos" runat="server" EmptyMessage="Selecione um tipo de procedimento"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboProcedimentosInternos_ItemsRequested"
    OnSelectedIndexChanged="cboProcedimentosInternos_SelectedIndexChanged" MaxLength="255">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="16%">
                    Código
                </td>
                <td width="80%">
                    Descrição do tipo de procedimento
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="16%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="80%">
                    <%#DataBinder.Eval(Container, "Attributes['DescricaoTipo']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
