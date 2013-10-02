<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdPessoaJuridica.aspx.vb" Inherits="WorkSpace.cdPessoaJuridica" %>

<%@ Register Src="~/ctrlMunicipios.ascx" TagName="ctrlMunicipios" TagPrefix="uc2" %>
<%@ Register Src="~/ctrlBancosEAgencias.ascx" TagName="ctrlBancosEAgencias" TagPrefix="uc3" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" AutoPostBack="true" runat="server" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.006.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="rdkPessoa" runat="server" Title="Pessoa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlPessoa" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label24" runat="server" Text="Nome"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNome" runat="server" MaxLength="100" Skin="Vista" Width="400px"
                                        SelectionOnFocus="CaretToBeginning">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkDadosPessoa" runat="server" Title="Dados da pessoa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Dados básicos" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Endereços">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Contatos">
                            </telerik:RadTab>
                             <telerik:RadTab Text="Dados bancários">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <asp:Panel ID="pnlDadosBasicos" runat="server">
                                <table class="tabela">
                                      <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label23" runat="server" Text="Logomarca"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:Image ID="imgFoto" runat="server" />
                                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                <script type="text/javascript">
                                //<![CDATA[

                                                    function updateFotoPessoa() {
                                                        var upload = $find("<%=uplFoto.ClientID %>");

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
                                            <table>
                                                <tr>
                                                    <td>
                                                        <telerik:RadAsyncUpload runat="server" ID="uplFoto" MaxFileInputsCount="1" AllowedFileExtensions=".jpg,.jpeg,.bmp,.png"
                                                            PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                                            Localization-Select="Procurar" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Button ID="ButtonSubmit" runat="server" Text="Enviar para o servidor" OnClientClick="updateFotoPessoa(); return false;"
                                                            CausesValidation="False" CssClass="RadUploadSubmit" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Nome fantasia"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeFantasia" runat="server" MaxLength="100" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlEndereco" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label8" runat="server" Text="Logradouro"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtLogradouro" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label9" runat="server" Text="Complemento"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtComplemento" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label18" runat="server" Text="Bairro"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtBairro" runat="server" MaxLength="100" Skin="Vista" Width="300px"
                                                SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label21" runat="server" Text="Município"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlMunicipios ID="ctrlMunicipios2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label10" runat="server" Text="UF"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboUFEndereco" Enabled="false" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label11" runat="server" Text="CEP"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtCEPEndereco" runat="server" Mask="##.###-###" Skin="Vista"
                                                LabelCssClass="radLabelCss_Vista">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDocumentos" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label12" runat="server" Text="CNPJ"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtCNPJ" runat="server" Mask="##.###.###/####-##" Skin="Vista"
                                                LabelCssClass="radLabelCss_Vista">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Inscrição estadual"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtInscricaoEstadual" runat="server" Mask="##.###.###-#"
                                                Skin="Vista" LabelCssClass="radLabelCss_Vista">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Inscrição municipal"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtInstricaoMunicipal" runat="server" Mask="###.###-#"
                                                Skin="Vista" LabelCssClass="radLabelCss_Vista">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlContatos" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label6" runat="server" Text="E-mail"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="255" Skin="Vista" Width="300px"
                                                SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label25" runat="server" Text="Site"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtSite" runat="server" MaxLength="255" Skin="Vista" Width="300px"
                                                SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label26" runat="server" Text="Telefone"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label27" runat="server" Text="Tipo"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboTipoTelefone" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label28" runat="server" Text="DDD"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtDDDTelefone" runat="server" Mask="##">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label29" runat="server" Text="Número"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtNumeroTelefone" runat="server" Mask="####-####">
                                            </telerik:RadMaskedTextBox>
                                            <asp:Button ID="btnAdicionarTelefone" runat="server" CssClass="RadUploadSubmit" Text="Adicionar telefone" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdTelefones" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Skin="Vista">
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column7"
                                                            ImageUrl="~/imagens/delete.gif">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="DDD" UniqueName="column1" Visible="True" HeaderText="DDD">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Numero" UniqueName="column2" Visible="True" HeaderText="Número">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Tipo.Descricao" UniqueName="column3" HeaderText="Tipo">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                           <telerik:RadPageView ID="RadPageView5" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosBancarios" runat="server">
                                <uc3:ctrlBancosEAgencias ID="ctrlBancosEAgencias1" runat="server" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
