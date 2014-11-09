<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmLeituraRevistaMarca.aspx.cs" Inherits="MP.Client.MP.frmLeituraRevistaMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/MP/ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc1" %>
<%@ Register Src="~/MP/ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas"
    TagPrefix="uc2" %>
<%@ Register Src="~/ctrlUF.ascx" TagName="ctrlUF" TagPrefix="uc3" %>
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
                                    <asp:Label ID="Label23" runat="server" Text="Selecione a revista:"></asp:Label>
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
                                        AllowedFileExtensions=".zip" PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                        Localization-Select="Procurar" OnFileUploaded="uplRevistaMarca_OnFileUploaded" />
                                    <asp:Button ID="ButtonSubmit" runat="server" Text="Adicionar" OnClientClick="updateRevistaMarca(); return false;"
                                        CausesValidation="False" CssClass="RadUploadSubmit" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td width="50%">
                                    <telerik:RadGrid ID="grdRevistasAProcessar" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="10" GridLines="None" Width="98%" Skin="Vista" OnItemCommand="grdRevistasAProcessar_ItemCommand"
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
                                                    HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column3">
                                                    <ItemStyle Width="2%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="ProcessarRevista"
                                                    FilterImageToolTip="ProcessarRevista" HeaderTooltip="Processar Revista" ImageUrl="~/imagens/processarRevista.gif"
                                                    UniqueName="column4">
                                                    <ItemStyle Width="4%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="LerRevista"
                                                    FilterImageToolTip="LerRevista" HeaderTooltip="Ler Revista" ImageUrl="~/imagens/lerRevista.gif"
                                                    UniqueName="column5">
                                                    <ItemStyle Width="4%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="NumeroRevistaMarcas" HeaderText="Revista(s) a processar"
                                                    UniqueName="column1">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                                <td width="50%">
                                    <telerik:RadGrid ID="grdRevistasJaProcessadas" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="10" GridLines="None" Width="98%" Skin="Vista" OnItemCommand="grdRevistasJaProcessadas_ItemCommand"
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
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                    HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                                    <ItemStyle Width="2%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="ReprocessarRevista"
                                                    FilterImageToolTip="ProcessarRevista" HeaderTooltip="Reprocessar Revista" ImageUrl="~/imagens/processarRevista.gif"
                                                    UniqueName="column4">
                                                    <ItemStyle Width="4%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="LerRevista"
                                                    FilterImageToolTip="LerRevista" HeaderTooltip="Ler Revista" ImageUrl="~/imagens/lerRevista.gif"
                                                    UniqueName="column5">
                                                    <ItemStyle Width="4%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="NumeroRevistaMarcas" HeaderText="Revista(s) já processada(s)"
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
                                <td class="td">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" Text="Publicações próprias:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPublicacoesProprias" runat="server" Enabled="false" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Processos:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtQuantdadeDeProcessos" runat="server" Enabled="false" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlRelatorios" Visible="false" runat="server">
                            <table class="tabela">
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label10" runat="server" Text="Gerar relatório Analítico:"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnRelPublicPropriasAnalitico" runat="server" ImageUrl="~/imagens/imprimir.png"
                                            ToolTip="Gerar Relatório Analítico das publicações próprias" OnClick="btnRelPublicPropriasAnalitico_OnClick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="th3">
                                        <asp:Label ID="Label9" runat="server" Text="Gerar relatório Sintético:"></asp:Label>
                                    </td>
                                    <td class="td">
                                        <asp:ImageButton ID="btnRelPublicPropriasSintetico" runat="server" ImageUrl="~/imagens/imprimir.png"
                                            ToolTip="Gerar Relatório Sintético das publicações próprias" OnClick="btnRelPublicPropriasSintetico_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                            MultiPageID="RadMultiPage1" CausesValidation="False" AutoPostBack="True" OnTabClick="RadTabStrip1_OnTabClick">
                            <Tabs>
                                <telerik:RadTab Text="Publicações Próprias" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Consulta por Filtros">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Radicais">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="RadPageView1" runat="server" SkinID="Vista">
                                <asp:Panel ID="pnlDadosDaRevistaProcesso" runat="server">
                                    <table class="tabela">
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="gridRevistaProcessos" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="gridRevistaProcessos_ItemCommand"
                                                    OnItemCreated="gridRevistaProcessos_ItemCreated" OnPageIndexChanged="gridRevistaProcessos_PageIndexChanged">
                                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                    <MasterTableView GridLines="Both">
                                                        <RowIndicatorColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" FilterImageToolTip="Modificar"
                                                                HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                                                <ItemStyle Width="2%"></ItemStyle>
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Email" FilterImageToolTip="Enviar e-mail"
                                                                HeaderTooltip="Enviar e-mail" ImageUrl="~/imagens/email.gif" UniqueName="column9">
                                                                <ItemStyle Width="2%"></ItemStyle>
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridBoundColumn DataField="IdProcessoDeMarca" HeaderText="ID" UniqueName="column6"
                                                                Display="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Processo" HeaderText="Processo" UniqueName="column1">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DataDoDeposito" HeaderText="Data do depósito"
                                                                UniqueName="column2" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Despacho.CodigoDespacho" HeaderText="Despacho"
                                                                UniqueName="column3">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Marca.Cliente.Pessoa.Nome" HeaderText="Titular"
                                                                UniqueName="column4">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Marca.DescricaoDaMarca" HeaderText="Marca" UniqueName="column5">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
                                <asp:Panel ID="pnlFiltro" runat="server">
                                    <table class="tabela">
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="Label1" runat="server" Text="Filtros para a revista"></asp:Label>
                                            </td>
                                            <td class="td">
                                                &nbsp;
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
                                                <uc3:ctrlUF ID="ctrlUF" runat="server" />
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
                                                &nbsp;
                                            </td>
                                            <td class="td">
                                                <telerik:RadButton ID="btnFiltrar" runat="server" Text="Filtrar" Skin="Vista" OnClick="btnFiltrar_ButtonClick">
                                                </telerik:RadButton>
                                                <telerik:RadButton ID="btnLimpar" runat="server" Text="Limpar" Skin="Vista" OnClick="btnLimpar_ButtonClick">
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <table class="tabela">
                                    <tr>
                                        <td >
                                            <telerik:RadGrid ID="grdFiltros" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdFiltros_ItemCommand"
                                                OnItemCreated="grdFiltros_ItemCreated" OnPageIndexChanged="grdFiltros_PageIndexChanged">
                                                <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                <MasterTableView GridLines="Both">
                                                    <RowIndicatorColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn>
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="DetalharProcesso"
                                                            FilterImageToolTip="Detalhes" HeaderTooltip="Detalhes" ImageUrl="~/imagens/find.gif"
                                                            UniqueName="column10">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Processo" UniqueName="column1">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DataDeDeposito" HeaderText="Data de depósito"
                                                            UniqueName="column2" DataFormatString="{0:dd/MM/yyyy}">
                                                            <ItemStyle Width="15%"></ItemStyle>
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CodigoDoDespacho" HeaderText="Despacho" UniqueName="column3">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Titular" HeaderText="Titular" UniqueName="column4">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Procurador" HeaderText="Procurador" UniqueName="column5">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server" SkinID="Vista">
                                <asp:Panel ID="pnlRadicais" runat="server">
                                    <table class="tabela">
                                        <tr>
                                            <td class="td">
                                                <telerik:RadListView ID="listRadical" runat="server" AllowPaging="True" ItemPlaceholderID="pnlPlaceHoder"
                                                    DataKeyNames="IdLeitura" Skin="Vista" OnPageIndexChanged="listRadical_OnPageIndexChanged">
                                                    <LayoutTemplate>
                                                        <asp:Panel ID="pnlPlaceHoder" runat="server" />
                                                        <table class="tabela">
                                                            <tr>
                                                                <td class="td">
                                                                    <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="listRadical"
                                                                        PageSize="1" Skin="Vista">
                                                                        <Fields>
                                                                            <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                                                            <telerik:RadDataPagerButtonField FieldType="Numeric" PageButtonCount="5" />
                                                                            <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                                                            <telerik:RadDataPagerTemplatePageField>
                                                                                <PagerTemplate>
                                                                                    <div style="float: right">
                                                                                        <b>Radical
                                                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.Owner.TotalRowCount > (Container.Owner.StartRowIndex+Container.Owner.PageSize) ? Container.Owner.StartRowIndex+Container.Owner.PageSize : Container.Owner.TotalRowCount %>" />
                                                                                            de
                                                                                            <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.TotalRowCount%>" />
                                                                                            <br />
                                                                                        </b>
                                                                                    </div>
                                                                                </PagerTemplate>
                                                                            </telerik:RadDataPagerTemplatePageField>
                                                                        </Fields>
                                                                    </telerik:RadDataPager>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <div style="float: left;">
                                                            <table class="tabela">
                                                                <tr>
                                                                    <td class="th3">
                                                                        <asp:Label ID="Label5" runat="server" Text="Radical:"></asp:Label>
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadTextBox ID="txtRadical" runat="server" Enabled="false" Width="87px" Text='<%# Eval("Radical") %>'>
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                    <td class="th3">
                                                                        <asp:Label ID="Label7" runat="server" Text="NCL:"></asp:Label>
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadTextBox ID="txtRadicalNCL" runat="server" Enabled="false" Width="87px"
                                                                            Text='<%# Eval("RadicalNCL")%>'>
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:RadListView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="tabela">
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="field">
                                                    <legend class="fieldlegend">
                                                        <asp:Label ID="Label12" runat="server" Text="Marcas de clientes:"></asp:Label>
                                                    </legend>
                                                    <table class="tabela">
                                                        <tr>
                                                            <td class="td" colspan="2">
                                                                <telerik:RadGrid ID="grdMarcasClientes" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdMarcasClientes_ItemCommand"
                                                                    OnItemCreated="grdMarcasClientes_ItemCreated" OnPageIndexChanged="grdMarcasClientes_PageIndexChanged">
                                                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                                    <MasterTableView GridLines="Both">
                                                                        <RowIndicatorColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="IdLeitura" HeaderText="ID" UniqueName="column6"
                                                                                Display="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NCL" HeaderText="NCL" UniqueName="column1">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="column2">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Processo" UniqueName="column3">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="tabela">
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="field">
                                                    <legend class="fieldlegend">
                                                        <asp:Label ID="Label8" runat="server" Text="Marcas Colidentes:"></asp:Label>
                                                    </legend>
                                                    <table class="tabela">
                                                        <tr>
                                                            <td class="td" colspan="2">
                                                                <telerik:RadGrid ID="grdMarcasColidentes" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdMarcasColidentes_ItemCommand"
                                                                    OnItemCreated="grdMarcasColidentes_ItemCreated" OnPageIndexChanged="grdMarcasColidentes_PageIndexChanged">
                                                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                                    <MasterTableView GridLines="Both">
                                                                        <RowIndicatorColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="IdLeitura" HeaderText="ID" UniqueName="column6"
                                                                                Display="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CodigoDoDespacho" HeaderText="Despacho" UniqueName="column1">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NCL" HeaderText="NCL" UniqueName="column2">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Marca" HeaderText="Marca" UniqueName="column3">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Processo" UniqueName="column4">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
