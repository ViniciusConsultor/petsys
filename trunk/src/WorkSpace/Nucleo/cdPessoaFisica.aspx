<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdPessoaFisica.aspx.vb" Inherits="WorkSpace.cdPessoaFisica" %>

<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<%@ Register Src="~/ctrlMunicipios.ascx" TagName="ctrlMunicipios" TagPrefix="uc2" %>
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
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label24" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNome" runat="server" MaxLength="255" Skin="Vista" Width="400px"
                                    SelectionOnFocus="CaretToBeginning">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkDadosPessoa" runat="server" Title="Dados da pessoa" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Pessoais" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Endereços">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Contatos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <asp:Panel ID="pnlDadosPessoais" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label23" runat="server" Text="Foto"></asp:Label>
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
                                            <asp:Image ID="imgFoto" runat="server" />
                                            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" ClientEvents-OnRequestStart="conditionalPostback">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadUpload ID="uplFoto" runat="server" AllowedFileExtensions=".jpg,.jpeg,.gif,.bmp"
                                                                ControlObjectsVisibility="None" Culture="Portuguese (Brazil)" OverwriteExistingFiles="True"
                                                                Skin="Vista" Width="225px">
                                                                <Localization Select="Procurar" />
                                                            </telerik:RadUpload>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:Button ID="ButtonSubmit" runat="server" Text="Salvar foto" CausesValidation="False"
                                                                CssClass="RadUploadSubmit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label4" runat="server" Text="Sexo"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblSexo" runat="server" CellPadding="0" CellSpacing="2"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Data de nascimento"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeNascimento" runat="server">
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataDeNascimento"
                                                ErrorMessage="Campo deve ser informado"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label22" runat="server" Text="Nacionalidade"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboNacionalidade" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label20" runat="server" Text="Município de nascimento"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlMunicipios ID="ctrlMunicipios1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label19" runat="server" Text="UF"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboUFNascimento" Enabled="false" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Nome da mãe"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeDaMae" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeDaMae"
                                                ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Nome do pai"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeDoPai" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label7" runat="server" Text="Estado civil"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboEstadoCivil" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label5" runat="server" Text="Raça/Cor"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboRaca" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
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
                                            <telerik:RadTextBox ID="txtBairro" runat="server" MaxLength="255" Skin="Vista" Width="300px"
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
                                            <asp:Label ID="Label12" runat="server" Text="CPF"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadMaskedTextBox ID="txtCPF" runat="server" Mask="###.###.###-###" Skin="Vista"
                                                LabelCssClass="radLabelCss_Vista">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label13" runat="server" Text="RG"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label15" runat="server" Text="Número"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNumeroRG" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label14" runat="server" Text="Data de emissão"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataEmissaoRG" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label16" runat="server" Text="Orgão expeditor."></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtOrgaoExpeditorRG" runat="server" MaxLength="255" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label17" runat="server" Text="UF"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadComboBox ID="cboUFRG" runat="server" Skin="Vista">
                                            </telerik:RadComboBox>
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
                                            <telerik:RadNumericTextBox ID="txtDDD" runat="server">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label29" runat="server" Text="Número"></asp:Label>
                                        </td>
                                        <td class="td">
                                          <telerik:RadNumericTextBox ID="txtNumero" runat="server">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="lnhRamal">
                                        <td class="campodependente">
                                            <asp:Label ID="Label30" runat="server" Text="Ramal"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtRamal" runat="server">
                                            </telerik:RadTextBox>
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
