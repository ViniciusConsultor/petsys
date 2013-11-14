<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdPessoaFisica.aspx.vb" Inherits="WorkSpace.cdPessoaFisica" %>

<%@ Register Src="~/ctrlMunicipios.ascx" TagName="ctrlMunicipios" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlBancosEAgencias.ascx" TagName="ctrlBancosEAgencias" TagPrefix="uc3" %>
<%@ Register Src="~/ctrlTipoEndereco.ascx" TagName="ctrlTipoEndereco" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:radtoolbar id="rtbToolBar" autopostback="true" runat="server" skin="Vista"
        style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.006.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.006.0004" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="N�o"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:radtoolbar>
    <telerik:raddocklayout id="RadDockLayout1" runat="server" skin="Vista">
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
                                <telerik:RadTextBox ID="txtNome" runat="server" MaxLength="100" Skin="Vista" Width="400px"
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
                            <telerik:RadTab Text="Endere�os">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Contatos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Dados banc�rios">
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
                                                            Ext.MessageBox.show({ title: 'Informa��o', msg: 'Selecione uma foto', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO });
                                                        }
                                                    }
                          //]]>
                                                </script>
                                            </telerik:RadScriptBlock>
                                            <telerik:RadAsyncUpload runat="server" ID="uplFoto" MaxFileInputsCount="1" AllowedFileExtensions=".jpg,.jpeg,.bmp,.png,.gif"
                                                            PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                                            Localization-Select="Procurar" />
                                             <asp:Button ID="ButtonSubmit" runat="server" Text="Enviar para o servidor" OnClientClick="updateFotoPessoa(); return false;"
                                                            CausesValidation="False" CssClass="RadUploadSubmit" />
                                            
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
                                            <asp:Label ID="Label20" runat="server" Text="Munic�pio de nascimento"></asp:Label>
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
                                            <asp:Label ID="Label2" runat="server" Text="Nome da m�e"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeDaMae" runat="server" MaxLength="100" Skin="Vista"
                                                Width="300px" SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Nome do pai"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeDoPai" runat="server" MaxLength="100" Skin="Vista"
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
                                            <asp:Label ID="Label5" runat="server" Text="Ra�a/Cor"></asp:Label>
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
                                            <asp:Label ID="Label30" runat="server" Text="Tipo de endere�o"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc1:ctrlTipoEndereco ID="ctrlTipoEndereco1" runat="server" />
                                             
                                        </td>
                                    </tr>
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
                                            <asp:Label ID="Label21" runat="server" Text="Munic�pio"></asp:Label>
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
                                            <asp:Button ID="btnAdicionarEndereco" runat="server" CssClass="RadUploadSubmit" Text="Adicionar endere�o" />
                                        </td>
                                    </tr>
                                    
                                      <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="grdEnderecos" runat="server" AutoGenerateColumns="False" GridLines="None"
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
                                                        <telerik:GridBoundColumn DataField="Logradouro" UniqueName="column1" Visible="True" HeaderText="Logradouro">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Complemento" UniqueName="column2" Visible="True" HeaderText="Complemento">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Municipio.Nome" UniqueName="column3" Visible="True" HeaderText="Munic�pio">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CEP" UniqueName="column4" HeaderText="CEP">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Bairro" UniqueName="column5" HeaderText="Bairro">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TipoDeEndereco.Nome" UniqueName="column6" HeaderText="Tipo">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
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
                                            <asp:Label ID="Label15" runat="server" Text="N�mero"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNumeroRG" runat="server" MaxLength="30" Skin="Vista" Width="300px"
                                                SelectionOnFocus="CaretToBeginning">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label14" runat="server" Text="Data de emiss�o"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataEmissaoRG" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label16" runat="server" Text="Org�o expeditor."></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtOrgaoExpeditorRG" runat="server" MaxLength="20" Skin="Vista"
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
                                            <telerik:RadMaskedTextBox ID="txtDDDTelefone" runat="server" Mask="##">
                                            </telerik:RadMaskedTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente">
                                            <asp:Label ID="Label29" runat="server" Text="N�mero"></asp:Label>
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
                                                        <telerik:GridBoundColumn DataField="Numero" UniqueName="column2" Visible="True" HeaderText="N�mero">
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
    </telerik:raddocklayout>
</asp:Content>
