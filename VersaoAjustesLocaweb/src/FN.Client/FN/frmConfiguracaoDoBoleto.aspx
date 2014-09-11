<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmConfiguracaoDoBoleto.aspx.cs" Inherits="FN.Client.FN.frmConfiguracaoDoBoleto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="ctrlCedente" Src="~/ctrlCedente.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" CommandArgument="OPE.FN.003.0001" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da configuração do boleto"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlBoletoBancario" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Cedente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlCedente ID="ctrlCedente" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Imagem cabeçalho recibo"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:Image ID="imgImagem" runat="server" CssClass="imagemUpLoad" />
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                        <script type="text/javascript">
                                //<![CDATA[

                                            function updateImagem() {
                                                var upload = $find("<%=uplImagem.ClientID %>");

                                                if (upload.getUploadedFiles().length > 0) {
                                                    __doPostBack('ButtonSubmit', 'RadButton1Args');
                                                }
                                                else {
                                                    Ext.MessageBox.show({ title: 'Informação', msg: 'Selecione uma imagem', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO });
                                                }
                                            }
                          //]]>
                                        </script>
                                    </telerik:RadScriptBlock>
                                    <telerik:RadAsyncUpload runat="server" ID="uplImagem" MaxFileInputsCount="1" AllowedFileExtensions=".jpg,.jpeg,.bmp,.png"
                                        PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                        Localization-Select="Procurar" OnFileUploaded="uplImagem_OnFileUploaded" />
                                    <asp:Button ID="ButtonSubmit" runat="server" Text="Enviar para o servidor" OnClientClick="updateImagem(); return false;"
                                        CausesValidation="False" CssClass="RadUploadSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
