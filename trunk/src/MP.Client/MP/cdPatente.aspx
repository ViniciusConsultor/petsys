<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdPatente.aspx.cs" Inherits="MP.Client.MP.cdPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/MP/ctrlPatente.ascx" TagName="ctrPatente" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Width="100%" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.006.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.006.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.006.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Patentes cadastradas" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                    <uc1:ctrPatente ID="ctrPatente" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Cadastro de Patentes" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Patente" Selected="True"> </telerik:RadTab>
                            <telerik:RadTab Text="Complemento"></telerik:RadTab>
                            <telerik:RadTab Text="Obrigações"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlDadosDaMarca" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label1" runat="server" Text="Cliente:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc5:ctrlCliente ID="ctrlCliente" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Apresentação:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc3:ctrlApresentacao ID="ctrlApresentacao" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Natureza:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc1:ctrlNatureza ID="ctrlNatureza" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label23" runat="server" Text="Imagem da marca"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:Image ID="imgImagemMarca" runat="server" />
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
                                            <asp:Label ID="Label5" runat="server" Text="Classificação de NCL:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlNCL ID="ctrlNCL" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="tabela">
                                    <tr>
                                        <td class="th3" colspan="2">
                                            <asp:Label ID="Label6" runat="server" Text="Classificação anterior de produtos e serviços:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlClassificacao" runat="server" GroupingText="" BorderWidth="1" BorderColor="#F5F5F5">
                                                <table class="tabela">
                                                    <tr>
                                                        <td class="th3">
                                                            <asp:Label ID="Label7" runat="server" Text="Classe:"></asp:Label>
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
                                                            <asp:Label ID="Label8" runat="server" Text="Sub - Classes:"></asp:Label>
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
                                            </asp:Panel>
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
                                            <asp:Label ID="Label9" runat="server" Text="Especificação de produtos e serviços:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtEspecificacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Rows="5" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label10" runat="server" Text="Observação:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Rows="5" Width="450px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label11" runat="server" Text="Radical:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtRadical" runat="server" Width="400px" MaxLength="50">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label4" runat="server" Text="Selecione um NCL:"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <uc2:ctrlNCL ID="ctrlNCLRadical" runat="server" />
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
                                    PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdRadicais_ItemCommand" OnItemCreated="grdRadicais_ItemCreated" 
                                    OnPageIndexChanged="grdRadicais_PageIndexChanged">
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
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
