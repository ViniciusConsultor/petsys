<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmConfiguracaoDoModulo.aspx.cs" Inherits="MP.Client.MP.frmConfiguracaoDoModulo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="ctrlCedente" Src="~/ctrlCedente.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" CommandArgument="OPE.MP.018.0001"  />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da configuração do módulo"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Índices financeiros" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Boleto bancário">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosIndiceFinanceiro" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label9" runat="server" Text="Valor Salário Mínimo"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadNumericTextBox ID="txtValorSalarioMinimo" runat="server" Width="87px"
                                                Type="Currency" DataType="System.Double">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
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
                                          <asp:Image ID="imgImagem" runat="server"  CssClass="imagemUpLoad"/>
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
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
