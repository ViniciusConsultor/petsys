<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdCedente.aspx.vb" Inherits="WorkSpace.cdCedente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc2" TagName="ctrlTipoDeCarteira" Src="~/ctrlTipoDeCarteira.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.019.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.019.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.019.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cedente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label7" runat="server" Text="Cedente Padrão"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:CheckBox ID="chkCedentePadrao" runat="server" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label13" runat="server" Text="Tipo de carteira:"></asp:Label>
                            </td>
                            <td class="td">
                                <uc2:ctrlTipoDeCarteira ID="ctrlTipoDeCarteira1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Início do nosso número:"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtIniNossoNumero" runat="server" MaxLength="8" Skin="Vista"
                                    Width="87px" SelectionOnFocus="CaretToBeginning">
                                </telerik:RadTextBox>
                                Ex.: (10001001)
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Banco"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadComboBox ID="cboBanco" runat="server" EmptyMessage="Selecione um banco"
                                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                                    <HeaderTemplate>
                                        <table width="96%">
                                            <tr>
                                                <td width="80%">
                                                    Nome
                                                </td>
                                                <td width="20%">
                                                    Número
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td width="20%">
                                                    <%#DataBinder.Eval(Container, "Attributes['Numero']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Número da agência"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNumeroAgencia" runat="server" MaxLength="10" Skin="Vista"
                                    Width="87px" SelectionOnFocus="CaretToBeginning">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Número da conta"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNumeroDaConta" runat="server" MaxLength="20" Skin="Vista"
                                    Width="87px" SelectionOnFocus="CaretToBeginning">
                                </telerik:RadTextBox>
                                Ex.: (Informar somente números incluindo o dígito verificador caso o mesmo exista)
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Tipo da conta"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtTipoConta" runat="server" MaxLength="3" Width="65px">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                Ex.: (Informar somente números: 001 (para conta corrente da caixa econômica))
                            </td>
                        </tr>
                    </table>
                    <table class="tabela">
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
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
