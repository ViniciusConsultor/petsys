﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOperacaoFiltro.ascx.vb" Inherits="WorkSpace.ctrlOperacaoFiltro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadComboBox ID="cboOperacaoFiltro" runat="server" EmptyMessage="Selecione uma operação para o filtro"
    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
    <HeaderTemplate>
        <table width="96%">
            <tr>
                <td width="6%">
                    Código
                </td>
                <td width="90%">
                    Descrição
                </td>
            </tr>
        </table>
    </HeaderTemplate>
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td width="10%">
                    <%#DataBinder.Eval(Container, "Attributes['Codigo']")%>
                </td>
                <td width="90%">
                    <%# DataBinder.Eval(Container, "Text")%>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</telerik:RadComboBox>
