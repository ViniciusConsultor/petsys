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
                    Código do despacho
                </td>
                <td width="81%">
                    Descrição
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

                 <td width="85%">
                    <%#DataBinder.Eval(Container, "Attributes['Descricao']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.MP.004.0001" OnClick="btnNovo_OnClick" />
