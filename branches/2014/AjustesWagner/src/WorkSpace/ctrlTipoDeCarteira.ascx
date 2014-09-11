<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTipoDeCarteira.ascx.vb" Inherits="WorkSpace.ctrlTipoDeCarteira" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboTipoDeCarteira" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
    LoadingMessage="Carregando..." ShowDropDownOnTextboxClick="False" MarkFirstMatch="false"
    AllowCustomText="True" HighlightTemplatedItems="True" Width="90%" Skin="Vista"
    CausesValidation="False" EmptyMessage="Selecione um tipo de carteira" MaxLength="100"  OnSelectedIndexChanged="cboTipoDeCarteira_SelectedIndexChanged">
    <HeaderTemplate>
        <table style="width: 96%" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width: 70%;">
                    Descrição
                </td>
                <td style="width: 30%;">
                    Sigla
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table style="width: 100%" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width: 70%;">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td style="width: 30%;">
                    <%#DataBinder.Eval(Container, "Attributes['Sigla']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>