<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlInventor.ascx.cs"
    Inherits="MP.Client.MP.ctrlInventor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboInventor" runat="server" EmptyMessage="Selecione um inventor"
    OnItemsRequested="cboInventor_OnItemsRequested" EnableLoadOnDemand="True" LoadingMessage="Carregando..."
    MarkFirstMatch="false" ShowDropDownOnTextboxClick="False" AllowCustomText="True"
    HighlightTemplatedItems="True" Width="90%" Skin="Vista" CausesValidation="False"
    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboInventor_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="10%">
                    Nome
                </td>
                <td width="40%">
                    Data do Cadastro
                </td>
                <td width="16%">
                    Informações Adicionais
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
                    <%#DataBinder.Eval(Container, "Attributes['DataDoCadastro']")%>
                </td>
                <td width="16%">
                    <%#DataBinder.Eval(Container, "Attributes['InformacoesAdicionais']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
<asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo"
    CausesValidation="False" CommandArgument="OPE.MP.002.0001" OnClick="btnNovo_OnClick" />