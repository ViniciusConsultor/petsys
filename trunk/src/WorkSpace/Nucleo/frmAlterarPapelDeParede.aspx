<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAlterarPapelDeParede.aspx.vb" Inherits="WorkSpace.frmAlterarPapelDeParede" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Black" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="false" CommandArgument="OPE.NCL.004.0001"/>
        </Items>
    </telerik:RadToolBar>
    <table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Papel de parede:"></asp:Label>
            </td>
            <td class="td">
                 <radscriptblock id="RadScriptBlock1" runat="server">
                                                <script type="text/javascript">
                                                    function conditionalPostback(sender, args) {
                                                        if (args.get_eventTarget() == "<%= ButtonSubmit.UniqueID %>") {
                                                            args.set_enableAjax(false);
                                                        }
                                                    }
                                                </script>
                                            </radscriptblock>
                <asp:Image ID="imgPapelDeParede" runat="server" />
                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" ClientEvents-OnRequestStart="conditionalPostback">
                    <telerik:RadUpload ID="uplFoto" runat="server" AllowedFileExtensions=".jpg,.jpeg,.gif,.bmp"
                        ControlObjectsVisibility="None" Culture="Portuguese (Brazil)" OverwriteExistingFiles="True"
                        Skin="Vista" TargetFolder="~/Loads">
                    </telerik:RadUpload>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Button" CausesValidation="False" />
                </telerik:RadAjaxPanel>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label1" runat="server" Text="Tema:"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="True" Skin="Vista">
                </telerik:RadSkinManager>
            </td>
        </tr>
    </table>
</asp:Content>
