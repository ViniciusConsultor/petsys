﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdPessoaJuridica.aspx.vb" Inherits="WorkSpace.cdPessoaJuridica" %>

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
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <asp:Panel ID="pnlDadosBasicos" runat="server">
                                <table class="tabela">
                                    <%--<tr>
                                        <td class="th3">
                                            <asp:Label ID="Label23" runat="server" Text="Logomarca"></asp:Label>
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
                                                            <asp:Button ID="ButtonSubmit" runat="server" Text="Salvar imagem" CausesValidation="False"
                                                                CssClass="RadUploadSubmit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </td>
                                    </tr>--%>
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
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
