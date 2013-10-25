<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmLeituraRevistaMarca.aspx.cs" Inherits="MP.Client.MP.frmLeituraRevistaMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/MP/ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc1" %>
<%@ Register Src="~/MP/ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Revista de marcas" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                <asp:Panel ID="pnlRevistaPrincipal" runat="server">
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label23" runat="server" Text="Selecione a revista para processamento:"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                    <script type="text/javascript">
                                //<![CDATA[

                                        function updateRevistaMarca() {
                                            var upload = $find("<%=uplRevistaMarca.ClientID %>");

                                            if (upload.getUploadedFiles().length > 0) {
                                                __doPostBack('ButtonSubmit', 'RadButton1Args');
                                            }
                                            else {
                                                Ext.MessageBox.show({ title: 'Informação', msg: 'Selecione uma revista', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO });
                                            }
                                        }
                          //]]>
                                    </script>
                                </telerik:RadScriptBlock>
                                <telerik:RadAsyncUpload runat="server" ID="uplRevistaMarca" MaxFileInputsCount="1"
                                    AllowedFileExtensions=".xml" PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                    Localization-Select="Procurar" OnFileUploaded="uplRevistaMarca_OnFileUploaded" />
                                <asp:Button ID="ButtonSubmit" runat="server" Text="Adicionar" OnClientClick="updateRevistaMarca(); return false;"
                                    CausesValidation="False" CssClass="RadUploadSubmit" />
                            </td>
                        </tr>
                        </table>
                        <table class="tabela">
                           <tr>
                            <td class="td">
                                <telerik:RadGrid ID="grdRevistasAProcessar" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdRevistasAProcessar_ItemCommand"
                                    OnItemCreated="grdRevistasAProcessar_ItemCreated" OnPageIndexChanged="grdRevistasAProcessar_PageIndexChanged">
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
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="ProcessarRevista" FilterImageToolTip="ProcessarRevista"
                                                HeaderTooltip="Processar Revista" ImageUrl="~/imagens/processarRevista.gif" UniqueName="column9">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="IdRevista" HeaderText="IdRevista" UniqueName="column"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NUMERO_REVISTA_MARCA_LIDA" HeaderText="Revista a processar"
                                                UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <td class="td">
                                <telerik:RadGrid ID="grdRevistasJaProcessadas" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdRevistasJaProcessadas_ItemCommand"
                                    OnItemCreated="grdRevistasJaProcessadas_ItemCreated" OnPageIndexChanged="grdRevistasJaProcessadas_PageIndexChanged">
                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                    <MasterTableView GridLines="Both">
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="IdRevista" HeaderText="IdRevista" UniqueName="column"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NUMERO_REVISTA_MARCA_LIDA" HeaderText="Revista já processada"
                                                UniqueName="column1">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>                                       
                        </tr>
                    </table>
                    <table class="tabela">
                        <tr>
                            <td class="th3" colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Filtros para revista:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Processo:"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtProcesso" runat="server" DataType="System.Int64"
                                    Type="Number">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                    </NumberFormat>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                            </td>
                            <td class="td">
                                <%-- controle uf --%>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Procurador:"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlProcurador ID="ctrlProcurador" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Despacho:"></asp:Label>
                            </td>
                            <td class="td">
                                <uc2:ctrlDespachoDeMarcas ID="ctrlDespachoDeMarcas" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <telerik:RadButton ID="btnFiltrar" runat="server" Text="Filtrar" Skin="Vista" OnClick="btnFiltrar_ButtonClick">
                                </telerik:RadButton>
                            </td>
                            <td class="td">
                                <telerik:RadButton ID="btnLimpar" runat="server" Text="Limpar" Skin="Vista" OnClick="btnLimpar_ButtonClick">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label11" runat="server" Text="Publicações próprias:"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtPublicacoesProprias" runat="server" Enabled="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Processos:"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtQuantdadeDeProcessos" runat="server" Enabled="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                  </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
