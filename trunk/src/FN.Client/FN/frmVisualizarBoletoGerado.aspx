﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisualizarBoletoGerado.aspx.cs" Inherits="FN.Client.FN.frmVisualizarBoletoGerado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pnlBoletoGerado" runat="server">
    </div>
    
    <input id="btnImprimir" type="button" value="Imprimir" onclick="javascript:window.print();" />
 
    </form>
</body>
</html>
