<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmConfiguracoesPessoais.aspx.vb" Inherits="WorkSpace.frmConfiguracoesPessoais" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
 
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="mltPage" SelectedIndex="0" CausesValidation="false">
        <Tabs>
            <telerik:RadTab runat="server" Text="Atalhos" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Temas">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Papel de parede">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Agenda">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mltPage" runat="server">
        <!--Atalhos -->
        <telerik:RadPageView ID="rpvAtalhos" runat="server" Selected="true">
            <telerik:RadDockLayout ID="RadDockLayout2" runat="server" Skin="Vista">
                <telerik:RadDockZone ID="RadDockZone2" runat="server" Skin="Vista">
                    <telerik:RadDock ID="RadDock2" runat="server" Title="Atalhos de funcionalidades do sistema"
                        DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadTreeView ID="trwMenuDoUsuario" runat="server" CheckBoxes="True" CheckChildNodes="True"
                                            TriStateCheckBoxes="True" Skin="Vista">
                                        </telerik:RadTreeView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadDock>
                    <telerik:RadDock ID="RadDock2_2" runat="server" Title="Atalhos externos" DefaultCommands="ExpandCollapse"
                        EnableAnimation="True" Skin="Vista" DockMode="Docked">
                        <ContentTemplate>
                            <table class="tabela">
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label5" runat="server" Text="Nome" ></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtNomeAtalhoExterno" runat="server" MaxLength="30" Skin="Vista"
                                            Width="300px" SelectionOnFocus="CaretToBeginning">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label40" runat="server" Text="Imagem"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <radscriptblock id="RadScriptBlock1" runat="server">
                                                <script type="text/javascript">
                                                    function conditionalPostback(sender, args) {
                                                        if (args.get_eventTarget() == "<%= ButtonSubmit2.UniqueID %>") {
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
                                                        <asp:Button ID="ButtonSubmit2" runat="server" Text="Salvar foto" CausesValidation="False"
                                                            CssClass="RadUploadSubmit" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadAjaxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label50" runat="server" Text="URL"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <telerik:RadTextBox ID="txtURLAtalhoExterno" runat="server" MaxLength="255" Skin="Vista"
                                            Width="300px" SelectionOnFocus="CaretToBeginning">
                                        </telerik:RadTextBox>
                                        <asp:Button ID="btnAdicionarItem" runat="server" Text="Adicionar atalho" CssClass="RadUploadSubmit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="grdAtalhosExternos" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" Skin="Vista">
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
                                                    <telerik:GridBoundColumn DataField="ID" UniqueName="column" Visible="False" HeaderText="ID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nome" UniqueName="column3" HeaderText="Nome">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="URL" UniqueName="column6" HeaderText="URL">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Imagem" UniqueName="column5" HeaderText="Imagem">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </telerik:RadDock>
                </telerik:RadDockZone>
            </telerik:RadDockLayout>
        </telerik:RadPageView>
        <!-- Atalhos -->
        <telerik:RadPageView ID="rpvTemas" runat="server">
            <table class="tabela">
                <tr>
                    <td class="th3">
                        <asp:Label ID="Label1" runat="server" Text="Tema"></asp:Label>
                    </td>
                    <td class="td">
                        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="True" Skin="Vista">
                        </telerik:RadSkinManager>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <!--Papel de parede-->
        <telerik:RadPageView ID="rpvPapelDeParede" runat="server">
            <table class="tabela">
                <tr>
                    <td class="th3">
                        <asp:Label ID="Label6" runat="server" Text="Papel de parede:"></asp:Label>
                    </td>
                    <td class="td">
                        <radscriptblock id="RadScriptBlock2" runat="server">
                                                <script type="text/javascript">
                                                    function conditionalPostback(sender, args) {
                                                        if (args.get_eventTarget() == "<%= btnSalvarPapelDeParede.UniqueID %>") {
                                                            args.set_enableAjax(false);
                                                        }
                                                    }
                                                </script>
                                            </radscriptblock>
                        <asp:Image ID="imgPapelDeParede" runat="server" />
                        <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel2" ClientEvents-OnRequestStart="conditionalPostback">
                            <telerik:RadUpload ID="RadUpload1" runat="server" AllowedFileExtensions=".jpg,.jpeg,.gif,.bmp"
                                ControlObjectsVisibility="None" Culture="Portuguese (Brazil)" OverwriteExistingFiles="True"
                                Skin="Vista" TargetFolder="~/Loads">
                            </telerik:RadUpload>
                            <asp:Button ID="btnSalvarPapelDeParede" runat="server" Text="Salvar" CausesValidation="False" />
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <!--Agenda-->
        <telerik:RadPageView ID="rpvAgenda" runat="server">
            <telerik:RadDockLayout ID="RadDockLayout3" runat="server" Skin="Vista">
                <telerik:RadDockZone ID="RadDockZone3" runat="server" Skin="Vista">
                    <telerik:RadDock ID="RadDock3" runat="server" Title="Dados da agenda" DefaultCommands="ExpandCollapse"
                        EnableAnimation="True" Skin="Vista" DockMode="Docked">
                        <ContentTemplate>
                            <table class="tabela">
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label7" runat="server" Text="Habilitar agenda"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:CheckBox ID="chkHabilitaAgenda" runat="server" AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlDadosDaAgenda" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Horário de início"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTimePicker ID="txtHorarioDeInicio" runat="server">
                                            </telerik:RadTimePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Horário final"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTimePicker ID="txtHorarioFinal" runat="server">
                                            </telerik:RadTimePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label4" runat="server" Text="Intervalo entre os compromissos"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTimePicker ID="txtIntervaloEntreCompromissos" runat="server">
                                            </telerik:RadTimePicker>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlPessoaPadraoDaAgenda" runat="server">
                                <uc1:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                    </telerik:RadDock>
                </telerik:RadDockZone>
            </telerik:RadDockLayout>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
