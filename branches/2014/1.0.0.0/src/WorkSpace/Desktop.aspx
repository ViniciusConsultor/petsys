<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Desktop.aspx.vb" Inherits="WorkSpace.Desktop" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Workspace</title>
    <link href="ext-all.css" rel="stylesheet" type="text/css" />

    <script src="js/ext-base.js" type="text/javascript"></script>

    <script src="js/ext-all.js" type="text/javascript"></script>

    <script src="js/StartMenu.js" type="text/javascript"></script>

    <script src="js/TaskBar.js" type="text/javascript"></script>

    <script src="js/Desktop.js" type="text/javascript"></script>

    <script src="js/App.js" type="text/javascript"></script>

    <script src="js/Module.js" type="text/javascript"></script>

    <link href="desktop.css" rel="stylesheet" type="text/css" />

    <script src="js/loading.js" type="text/javascript"></script>

</head>
<body scroll="no" runat="server" id="Corpo">
    <div id="x-desktop">
        <dl id="shortcuts" runat="server">
        </dl>
    </div>
    <div id="ux-taskbar">
        <div id="ux-taskbar-start">
        </div>
        <div id="ux-taskbuttons-panel">
        </div>
        <div class="x-clear">
        </div>
    </div>
    <form id="form1" runat="server">
   
    </form>
</body>
</html>
