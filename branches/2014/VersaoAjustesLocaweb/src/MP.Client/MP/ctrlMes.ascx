<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMes.ascx.cs" Inherits="MP.Client.MP.ctrlMes" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboMes" runat="server" EmptyMessage="Selecione um mês:"
    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
    Width="50%" Skin="Vista" CausesValidation="False" AutoPostBack="False" >
    <HeaderTemplate>
        <table width="56%">
            <tr>
                <td width="50%">
                    Descrição
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="60%">
            <tr>
                <td width="56%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>

