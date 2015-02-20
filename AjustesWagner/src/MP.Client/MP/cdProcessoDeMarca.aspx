<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdProcessoDeMarca.aspx.cs" Inherits="MP.Client.MP.cdProcessoDeMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas" TagPrefix="uc2" %>
<%@ Register Src="ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc3" %>
<%@ Register Src="ctrlApresentacao.ascx" TagName="ctrlApresentacao" TagPrefix="uc4" %>
<%@ Register TagPrefix="uc5" TagName="ctrlCliente" Src="~/ctrlCliente.ascx" %>
<%@ Register Src="ctrlNaturezaDeMarca.ascx" TagName="ctrlNaturezaDeMarca" TagPrefix="uc6" %>
<%@ Register Src="ctrlNCL.ascx" TagName="ctrlNCL" TagPrefix="uc7" %>
<%@ Register Src="ctrlPeriodo.ascx" TagName="ctrlPeriodo" TagPrefix="uc8" %>
<%@ Register Src="ctrlEventos.ascx" TagName="ctrlEventos" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de marca"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="tbsMarcas" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Marca" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Complemento">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Classificação">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Radicais">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Manutenção">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Eventos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Publicações">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosDaMarca" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label9" runat="server" Text="Nome"></asp:Label>
                                        </td>
                                        <td class="td" colspan="3">
                                            <telerik:RadTextBox ID="txtNomeDaMarca" runat="server" MaxLength="255" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Processo"></asp:Label>
                                        </td>
                                        <td class="td" colspan="3">
                                            <telerik:RadNumericTextBox ID="txtProcesso" runat="server" Width="87px" Type="Number">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                                </NumberFormat>
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label18" runat="server" Text="Cliente"></asp:Label>
                                        </td>
                                        <td class="td" colspan="3">
                                            <uc5:ctrlCliente ID="ctrlCliente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label19" runat="server" Text="Apresentação"></asp:Label>
                                        </td>
                                        <td class="td" colspan="3">
                                            <uc4:ctrlApresentacao ID="ctrlApresentacao" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label20" runat="server" Text="Natureza"></asp:Label>
                                        </td>
                                        <td class="td" colspan="3">
                                            <uc6:ctrlNaturezaDeMarca ID="ctrlNatureza" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label12" runat="server" Text="Está ativo?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblEstaAtivo" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="th3" rowspan="5">
                                            <asp:Label ID="Label253" runat="server" Text="Imagem da marca"></asp:Label>
                                        </td>
                                        <td class="td" rowspan="5">
                                            <asp:Image ID="imgImagemMarca" runat="server" CssClass="imagemUpLoad" />
                                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                <script type="text/javascript">
                                //<![CDATA[

                                                    function updateImagemMarca() {
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
                                            <asp:Button ID="ButtonSubmit" runat="server" Text="Enviar para o servidor" OnClientClick="updateImagemMarca(); return false;"
                                                CausesValidation="False" CssClass="RadUploadSubmit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Data de cadastro"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeCadastro" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label6" runat="server" Text="Data do depósito"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeDeposito" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Data de concessão"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeConcessao" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label7" runat="server" Text="Data de vigência"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDatePicker ID="txtDataDeVigencia" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlComplemento" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label4" runat="server" Text="Processo é de terceiro?"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:RadioButtonList ID="rblProcessoEhDeTerceiro" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label10" runat="server" Text="Procurador"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc3:ctrlProcurador ID="ctrlProcurador" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <fieldset class="field">
                                                <legend class="fieldlegend">
                                                    <asp:Label ID="Label13" runat="server" Text="Despacho"></asp:Label>
                                                </legend>
                                                <table class="tabela">
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label8" runat="server" Text="Código do despacho"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <uc2:ctrlDespachoDeMarcas ID="ctrlDespacho" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label14" runat="server" Text="Descrição do despacho"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadTextBox ID="txtDescricaoDoDespacho" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                                Rows="5" Width="450px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label15" runat="server" Text="Situação do processo"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadTextBox ID="txtSituacaoDoProcesso" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                                Rows="5" Width="450px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label16" runat="server" Text="Providência"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadTextBox ID="txtProvidencia" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                                Rows="5" Width="450px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label17" runat="server" Text="Prazo para a providência (em dias)"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadTextBox ID="txtPrazoParaProvidencia" runat="server" Width="87px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label5" runat="server" Text="Texto complementar do despacho"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadTextBox ID="txtTextoComplementarDoDespacho" runat="server" MaxLength="4000"
                                                                TextMode="MultiLine" Rows="5" Width="450px">
                                                            </telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label11" runat="server" Text="Apostila"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtApostila" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Rows="5" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label25" runat="server" Text="Especificação de produtos e serviços"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtEspecificacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Rows="5" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label26" runat="server" Text="Observação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Rows="5" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlClassificacao" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label21" runat="server" Text="Classificação de NCL"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc7:ctrlNCL ID="ctrlNCL" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <fieldset class="field">
                                                <legend class="fieldlegend">
                                                    <asp:Label ID="Label22" runat="server" Text="Classificação anterior de produtos e serviços"></asp:Label>
                                                </legend>
                                                <table class="tabela">
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label23" runat="server" Text="Classe"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadNumericTextBox ID="txtClasse" runat="server" Width="87px" Type="Number"
                                                                DataType="System.Uint32">
                                                                <NumberFormat DecimalDigits="0"></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label24" runat="server" Text="Sub - Classes"></asp:Label>
                                                        </td>
                                                        <td class="td">
                                                            <telerik:RadNumericTextBox ID="txtSubClasse1" runat="server" Width="87px" Type="Number"
                                                                DataType="System.Uint32">
                                                                <NumberFormat DecimalDigits="0"></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox ID="txtSubClasse2" runat="server" Width="87px" Type="Number"
                                                                DataType="System.Uint32">
                                                                <NumberFormat DecimalDigits="0"></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                            <telerik:RadNumericTextBox ID="txtSubClasse3" runat="server" Width="87px" Type="Number"
                                                                DataType="System.Uint32">
                                                                <NumberFormat DecimalDigits="0"></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView5" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlRadicais" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label27" runat="server" Text="Radical"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtRadical" runat="server" Width="400px" MaxLength="50">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label28" runat="server" Text="Selecione uma NCL"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc7:ctrlNCL ID="ctrlNCLRadical" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <telerik:RadButton ID="btnAdicionarRadical" runat="server" Text="Adicionar" Skin="Vista"
                                                OnClick="btnRadical_ButtonClick">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdRadicais" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdRadicais_ItemCommand"
                                    OnItemCreated="grdRadicais_ItemCreated" OnPageIndexChanged="grdRadicais_PageIndexChanged">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="IdRadicalMarca" HeaderText="IdRadical" UniqueName="column"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DESCRICAORADICAL" HeaderText="Radical" UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NCL.Codigo" HeaderText="NCL" UniqueName="column2">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server" SkinID="Vista">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlManutencao" runat="server">
                                        <table class="tabela">
                                            <tr>
                                                <td class="th3">
                                                    <asp:Label ID="Label29" runat="server" Text="Pagamento de manutenção?"></asp:Label>
                                                </td>
                                                <td class="td">
                                                    <asp:RadioButtonList ID="rblPagaManutencao" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rblPagaManutencao_OnSelectedIndexChanged">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlDadosDaManutencao" runat="server">
                                            <table class="tabela">
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="Label32" runat="server" Text="Data da próxima manutenção"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadDatePicker ID="txtDataDaPrimeiraManutencao" runat="server">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="Label30" runat="server" Text="Período"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <uc8:ctrlPeriodo ID="ctrlPeriodo" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="Label31" runat="server" Text="Forma de cobrança"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <asp:RadioButtonList ID="rblFormaDeCobranca" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rblFormaDeCobranca_OnSelectedIndexChanged">
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="th3">
                                                        <asp:Label ID="lblValor" runat="server" Text="Valor"></asp:Label>
                                                    </td>
                                                    <td class="td">
                                                        <telerik:RadNumericTextBox ID="txtValor" runat="server" Width="87px" Type="Currency">
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView6" runat="server" SkinID="Vista">
                            <uc9:ctrlEventos ID="ctrlEventos" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView7" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlPublicacoes" runat="server">
                                <telerik:RadGrid ID="grdPublicacoes" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="10" GridLines="None" Width="98%" OnPageIndexChanged="grdPublicacoes_OnPageIndexChanged" OnItemDataBound="grdPublicacoes_OnItemDataBound" OnItemCommand="grdPublicacoes_OnItemCommand">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="NumeroRevistaMarcas" HeaderText="RPI" UniqueName="column">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataPublicacao" HeaderText="Data RPI" UniqueName="column1"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="10%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="MostrarDespacho" DataTextField="CodigoDespacho"
                                                HeaderText="Despacho" UniqueName="Despacho">
                                                <ItemStyle Width="30%" CssClass="hidelink"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="TextoDoDespacho" HeaderText="Complemento do despacho"
                                                UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
