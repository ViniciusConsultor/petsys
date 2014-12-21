<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisualizarBoletoGerado.aspx.cs" Inherits="FN.Client.FN.frmVisualizarBoletoGerado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" >

    <title></title>
    <script type="text/javascript">
        function imprimir() {
            document.getElementById("btnImprimir").style.display = "none";
            document.getElementById("btnPdf").style.display = "none";
            window.print();
            document.getElementById("btnImprimir").style.display = "inline-block";
            document.getElementById("btnPdf").style.display = "inline-block";
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pnlBoletoGerado" runat="server">
    </div>
    <%
        var deveHabilitarBotaoImprimir = (bool)Session["HabilitarBotaoImprimir"];
        var displayBtnImprimir = "display: none";
        
        if(deveHabilitarBotaoImprimir)
        {
            displayBtnImprimir = "display: inline-block";
        }

    %>
        
    <input id="btnImprimir" type="button" value="Imprimir" style="<%= displayBtnImprimir %>" onclick="imprimir()" /> 
    <asp:Button runat="server" ID="btnPdf" Text="Salvar PDF" OnClick="btnPdf_click"/>
    </form>
</body>
</html>

