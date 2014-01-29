<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmEnviaEmail.aspx.cs" Inherits="MP.Client.MP.frmEnviaEmail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlTemplateDeEmail.ascx" TagName="ctrlTemplateDeEmail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Width="100%" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Enviar"
                CommandName="btnEnviar" CausesValidation="False" CommandArgument="OPE.MP.002.0001" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Enviar e-mail" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDoEmail" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Assunto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtAssunto" runat="server" MaxLength="255" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <uc1:ctrlTemplateDeEmail ID="ctrlTemplateDeEmail" runat="server" />
                        <table class="tabela">
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlEscolhaDeDestinariosDeMarca">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Escolha os destinários"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:CheckBox ID="chkProcuradorMarca" runat="server" Text="Procurador" AutoPostBack="True"
                                        OnCheckedChanged="chkProcuradorMarca_OnCheckedChanged" />
                                    <asp:CheckBox ID="chkClienteMarca" runat="server" Text="Cliente" AutoPostBack="True"
                                        OnCheckedChanged="chkClienteMarca_OnCheckedChanged" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlEscolhaDeDestinatoriosPatente">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Escolha os destinários"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:CheckBox ID="chkProcuradorPatente" runat="server" Text="Procurador" AutoPostBack="True"
                                        OnCheckedChanged="chkProcuradorPatente_OnCheckedChanged" />
                                    <asp:CheckBox ID="chkClientesPatente" runat="server" Text="Clientes" AutoPostBack="True"
                                        OnCheckedChanged="chkClientesPatente_OnCheckedChanged" />
                                    <asp:CheckBox ID="chkInventoresPatente" runat="server" Text="Invetores" AutoPostBack="true"
                                        OnCheckedChanged="chkInventoresPatente_OnCheckedChanged" />
                                    <asp:CheckBox ID="chkTitularesPatente" runat="server" Text="Titulares" AutoPostBack="True"
                                        OnCheckedChanged="chkTitularesPatente_OnCheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="E-mail"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDestinarioCCManual" runat="server" Width="450px">
                                    </telerik:RadTextBox>
                                    <telerik:RadButton ID="btnAdicionarDestinatarioCC" runat="server" Text="Adicionar"
                                        Skin="Vista" OnClick="btnAdicionarDestinatarioCC_OnClick">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Destinatários CC"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadGrid ID="grdDestinatariosCC" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdDestinatariosCC_OnItemCommand" OnPageIndexChanged=grdDestinatariosCC_OnPageIndexChanged> 
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
                                                <telerik:GridBoundColumn HeaderText="E-mail" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Destinatários CCo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDestinariosCCo" runat="server" MaxLength="4000" Width="450px"
                                        TextMode="MultiLine" Rows="3">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Anexos"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                        <script type="text/javascript">
                                //<![CDATA[

                                            function updateAnexo() {
                                                var upload = $find("<%=uplAnexos.ClientID %>");

                                                if (upload.getUploadedFiles().length > 0) {
                                                    __doPostBack('ButtonSubmit', 'RadButton1Args');
                                                }
                                                else {
                                                    Ext.MessageBox.show({ title: 'Informação', msg: 'Selecione pelo menos um anexo', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO });
                                                }
                                            }
                          //]]>
                                        </script>
                                    </telerik:RadScriptBlock>
                                    <telerik:RadAsyncUpload runat="server" ID="uplAnexos" MaxFileInputsCount="50" PostbackTriggers="ButtonSubmit"
                                        Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx" Localization-Select="Procurar"
                                        OnFileUploaded="uplAnexos_OnFileUploaded" />
                                    <asp:Button ID="ButtonSubmit" runat="server" Text="Anexar" OnClientClick="updateAnexo(); return false;"
                                        CausesValidation="False" CssClass="RadUploadSubmit" />
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Arquivos anexados"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadGrid ID="grdAnexos" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="10" GridLines="None" Width="98%" > 
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
                                                <telerik:GridBoundColumn HeaderText="Arquivo anexado" UniqueName="column">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
