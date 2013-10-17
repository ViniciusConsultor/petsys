<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlDespachoDeMarcas.ascx.cs"
    Inherits="MP.Client.MP.ctrlDespachoDeMarcas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
