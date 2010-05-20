<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAtalhos.aspx.vb" Inherits="WorkSpace.frmAtalhos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Salvar" CommandName="btnSalvar" CommandArgument="OPE.NCL.011.0001"
                ImageUrl="~/imagens/save.gif" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Atalhos externos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNomeAtalhoExterno" runat="server" MaxLength="30" Skin="Vista"
                                    Width="50px" SelectionOnFocus="CaretToBeginning">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Imagem"></asp:Label>
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
                                <asp:Label ID="Label1" runat="server" Text="URL"></asp:Label>
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
                                                <telerik:GridBoundColumn DataField="ID" UniqueName="column" Visible="False"
                                                    HeaderText="ID">
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
</asp:Content>
