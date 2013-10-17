<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProcurador.ascx.cs"
    Inherits="MP.Client.MP.ctrlProcurador" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboProcurador" runat="server" EmptyMessage="Selecione um procurador"
    OnItemsRequested="cboProcurador_OnItemsRequested" EnableLoadOnDemand="True" LoadingMessage="Carregando..."
    MarkFirstMatch="false" ShowDropDownOnTextboxClick="False" AllowCustomText="True"
    HighlightTemplatedItems="True" Width="90%" Skin="Vista" CausesValidation="False"
    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboProcurador_SelectedIndexChanged">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="56%">
                    Nome
                </td>
                <td width="20%">
                    Matricula API
                </td>
                <td width="20%">
                    Nº Registro Profissional
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="60%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
                <td width="20%">
                    <%#DataBinder.Eval(Container, "Attributes['MatriculaAPI']")%>
                </td>
                <td width="20%">
                    <%#DataBinder.Eval(Container, "Attributes['NumeroRegistroProfissional']")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
