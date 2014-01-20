<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlNaturezaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlNaturezaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboNaturezaPatente" runat="server" EmptyMessage="Selecione uma natureza da patente"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboNaturezaPatente_ItemsRequested"
    OnSelectedIndexChanged="cboNaturezaPatente_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="16%">
                    Sigla
                </td>
                <td width="80%">
                    Descrição da natureza patente
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="16%">
                    <%#DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="80%">
                    <%# DataBinder.Eval(Container, "Attributes['Descricao']")%>
                </td>
                
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo" 
    CausesValidation="False" CommandArgument="OPE.MP.005.0001" OnClick="btnNovo_OnClick" />
