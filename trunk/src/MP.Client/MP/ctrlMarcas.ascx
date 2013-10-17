<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMarcas.ascx.cs"
    Inherits="MP.Client.MP.ctrlMarcas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboMarcas" runat="server" EmptyMessage="Selecione uma marca..."
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboMarca_ItemsRequested"
    OnSelectedIndexChanged="cboMarca_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="58%">
                    Marca
                </td>
                <td width="17%">
                    Apresentação
                </td>
                <td width="17%">
                    Natureza
                </td>
                <td width="6%">
                    NCL
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="57%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="17%">
                    <%#DataBinder.Eval(Container, "Attributes['Apresentacao']")%>
                </td>
                <td width="18%">
                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                </td>
                <td width="8%">
                    <%#DataBinder.Eval(Container, "Attributes['NCL']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.MP.008.0001" OnClick="btnNovo_OnClick" />