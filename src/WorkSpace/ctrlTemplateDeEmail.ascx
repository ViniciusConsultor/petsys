﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTemplateDeEmail.ascx.vb" Inherits="WorkSpace.ctrlTemplateDeEmail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table class="tabela">
    <tr>
        <td class="th3">
            <asp:Label ID="Label3" runat="server" Text="Corpo do e-mail"></asp:Label>
        </td>
        <td class="td">
            <telerik:RadEditor ID="txtTextoDoTemplate" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                Language="pt-BR" MaxHtmlLength="4000" Skin="Vista" AutoResizeHeight="false"  Width="500px"
                ContentAreaCssFile="~/RadEditor/StyleSheetRadEditor.css">
                <Content>
                </Content>
            </telerik:RadEditor>
        </td>
    </tr>
</table>
