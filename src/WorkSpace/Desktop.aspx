<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Desktop.aspx.vb" Inherits="WorkSpace.Desktop" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
        <ContentTemplate>
            <div style="color: white; font-weight: bold; font-family: Arial;
                font-size: small; display: inline; position: fixed; top: 0%; left: 80%; padding: 2px;">
                <telerik:RadComboBox ID="cboPesquisa" runat="server" EmptyMessage="Digite para fazer uma pesquisa..."
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="100%" CausesValidation="False" OnItemsRequested="cboPesquisa_OnItemsRequested"
                    OnSelectedIndexChanged="cboPesquisa_OnSelectedIndexChanged" AutoPostBack="True">
                </telerik:RadComboBox>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
