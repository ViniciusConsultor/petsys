<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisualizarBoletoGerado.aspx.cs"
    Inherits="MP.Client.MP.frmVisualizarBoletoGerado" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

       <%-- <script src="html2canvas.js"></script>
        <script src="jquery.js"></script>

</head>
<script>
    $("#btnExportar").click(function () {
        html2canvas($('#pnlBoletoGerado'), {
            onrendered: function (canvas) {
                var img = canvas.toDataURL("image/png")

                //var image = document.getElementById("myCanvas").toDataURL("image/png");
                //image = image.replace('data:image/png;base64,', '');


                imgsamp.src = img;
                img = img.replace('data:image/png;base64,', '');

                $.ajax(
{
    type: 'POST',
    url: 'frmEnviaEmail.aspx',
    data: { 'imageData': img },
    //contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    success: function (msg) {
        alert('Image saved successfully !');
    }
});

            }
        });
    });

</script>--%>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="pnlBoletoGerado" runat="server">
    </div>
    <telerik:RadButton ID="btnExportar" runat="server" Text="Exportar" Skin="Vista">
    </telerik:RadButton>
    </form>
</body>
</html>
