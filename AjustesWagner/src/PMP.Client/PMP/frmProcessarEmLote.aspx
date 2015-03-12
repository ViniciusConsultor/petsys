<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmProcessarEmLote.aspx.cs" Inherits="PMP.Client.PMP.frmProcessarEmLote" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de marca"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3" rowspan="5">
                                <asp:Label ID="Label253" runat="server" Text="Revistas de marcas"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                    <script type="text/javascript">
                                //<![CDATA[

                                        function updateRevistasMarca() {
                                            var upload = $find("<%=uplRevista.ClientID %>");

                                            if (upload.getUploadedFiles().length > 0) {
                                                __doPostBack('ButtonSubmit', 'RadButton1Args');
                                            }
                                            else {
                                                Ext.MessageBox.show({ title: 'Informação', msg: 'Selecione ao menos uma revista', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO });
                                            }
                                        }
                          //]]>
                                    </script>
                                </telerik:RadScriptBlock>
                                <telerik:RadAsyncUpload runat="server" ID="uplRevista" MaxFileInputsCount="1" AllowedFileExtensions=".zip"
                                    PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                    Localization-Select="Procurar" OnFileUploaded="uplRevista_OnFileUploaded" MultipleFileSelection="Automatic" />
                                <asp:Button ID="ButtonSubmit" runat="server" Text="Processar" OnClientClick="updateRevistasMarca(); return false;"
                                    CausesValidation="False" CssClass="RadUploadSubmit" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
