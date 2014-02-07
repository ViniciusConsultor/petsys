<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmLeituraRevistaPatente.aspx.cs" Inherits="MP.Client.MP.frmLeituraRevistaPatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlFiltroRevistaPatente.ascx" TagName="ctrlFitroRevistaPatente" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Revista de patentes" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                    <asp:Panel ID="pnlRevistaPatentePrincipal" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label23" runat="server" Text="Selecione a revista:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                        <script type="text/javascript">
                                        //<![CDATA[

                                            function updateRevistaPatente() {
                                                var upload = $find("<%=uplRevistaPatente.ClientID %>");

                                                if (upload.getUploadedFiles().length > 0) { __doPostBack('ButtonSubmit', 'RadButton1Args'); }
                                                else { Ext.MessageBox.show({ title: 'Informação', msg: 'Selecione uma revista', buttons: Ext.MessageBox.OK, icon: Ext.MessageBox.INFO }); }
                                            }
                                            //]]>
                                        </script>
                                    </telerik:RadScriptBlock>
                                    <telerik:RadAsyncUpload runat="server" ID="uplRevistaPatente" MaxFileInputsCount="1"
                                        AllowedFileExtensions=".zip" PostbackTriggers="ButtonSubmit" Skin="Vista" HttpHandlerUrl="~/AsyncUploadHandlerCustom.ashx"
                                        Localization-Select="Procurar" OnFileUploaded="uplRevistaPatente_OnFileUploaded" />
                                    <asp:Button ID="ButtonSubmit" runat="server" Text="Adicionar" OnClientClick="updateRevistaPatente(); return false;"
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
                                                <telerik:GridBoundColumn DataField="NumeroRevistaPatente" HeaderText="Revista(s) a processar"
                                                    UniqueName="colunaNumeroRevista" />
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
                                                    FilterImageToolTip="ProcessarRevista" HeaderTooltip="Processar Revista" ImageUrl="~/imagens/processarRevista.gif"
                                                    UniqueName="column4">
                                                    <ItemStyle Width="4%"></ItemStyle>
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="NumeroRevistaPatente" HeaderText="Revista(s) já processada(s)"
                                                    UniqueName="colunaNumeroRevista">
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
                                    <asp:Label ID="Label11" runat="server" Text="Publicações próprias"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPublicacoesProprias" runat="server" Enabled="false" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Processos"></asp:Label>
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
                            MultiPageID="RadMultiPage1" CausesValidation="False" OnTabClick="RadTabStrip1_OnTabClick">
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
                                            <td class="th3">
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
                                                            <telerik:GridBoundColumn DataField="IdProcessoDePatente" HeaderText="Identificador"
                                                                UniqueName="IdProcessoPatente" Display="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="IdProcessoDePatente" HeaderText="Id Processo Patente"
                                                                UniqueName="IdProcessoDePatente" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Processo" HeaderText="Processo" UniqueName="column5">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Patente.NaturezaPatente.DescricaoNaturezaPatente"
                                                                HeaderText="Natureza" UniqueName="column3">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Patente.TituloPatente" HeaderText="Patente" UniqueName="column2">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DataDoCadastro" HeaderText="Data do cadastro"
                                                                UniqueName="column6" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Procurador.Pessoa.Nome" HeaderText="Procurador"
                                                                UniqueName="column2">
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
                                                <asp:Label ID="lblCampo" runat="server" Text="Campo:"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <uc3:ctrlFitroRevistaPatente ID="ctrlFitroRevistaPatente1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="th3">
                                                <asp:Label ID="lblValor" runat="server" Text="Valor:"></asp:Label>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtValor" runat="server" Width="80%" />
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
                                        <td class="th3">
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
                                                        <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Identificador"
                                                            UniqueName="NumeroDoProcesso" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Id Processo Patente"
                                                            UniqueName="IdProcessoDePatente" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Processo" UniqueName="column5">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NaturezaDoDocumento" HeaderText="Natureza" UniqueName="column3">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Titulo" HeaderText="Patente" UniqueName="column2">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DataDeDeposito" HeaderText="Data do depósito"
                                                            UniqueName="column6" DataFormatString="{0:dd/MM/yyyy}">
                                                            <ItemStyle Width="15%"></ItemStyle>
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Procurador" HeaderText="Procurador" UniqueName="column2">
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
                                                    DataKeyNames="IdPatente" Skin="Vista" OnPageIndexChanged="listRadical_OnPageIndexChanged">
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
                                                                        <asp:Label ID="Label5" runat="server" Text="Colidência:"></asp:Label>
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadTextBox ID="txtRadical" runat="server" Enabled="false" Width="87px" Text='<%# Eval("Colidencia") %>'>
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                    <td class="th3">
                                                                        <asp:Label ID="Label7" runat="server" Text="Classificação:"></asp:Label>
                                                                    </td>
                                                                    <td class="td">
                                                                        <telerik:RadTextBox ID="txtClassificações" runat="server" Enabled="false" Width="87px"
                                                                            Text='<%# Eval("Classificacao")%>'>
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
                                                        <asp:Label ID="Label7" runat="server" Text="Marcas de clientes:"></asp:Label>
                                                    </legend>
                                                    <table class="tabela">
                                                        <tr>
                                                            <td class="td" colspan="2">
                                                                <telerik:RadGrid ID="grdPatenteClientes" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdPatenteClientes_ItemCommand"
                                                                    OnItemCreated="grdPatenteClientes_ItemCreated" OnPageIndexChanged="grdPatenteClientes_PageIndexChanged">
                                                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                                    <MasterTableView GridLines="Both">
                                                                        <RowIndicatorColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="IdProcessoDePatente" HeaderText="ID" UniqueName="column6"
                                                                                Display="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Processo" HeaderText="Processo" UniqueName="column3">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Patente.TituloPatente" HeaderText="Patente" UniqueName="column2">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Patente.ClassificacoesConcatenadas" HeaderText="Classificação"
                                                                                UniqueName="colunaClassificacao">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                        </tr>
                                    </table>
                                    <table class="tabela">
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="field">
                                                    <legend class="fieldlegend">
                                                        <asp:Label ID="Label8" runat="server" Text="Patentes Colidentes:"></asp:Label>
                                                    </legend>
                                                    <table class="tabela">
                                                        <tr>
                                                            <td class="td" colspan="2">
                                                                <telerik:RadGrid ID="grdPatentesColidentes" runat="server" AutoGenerateColumns="False"
                                                                    AllowPaging="True" PageSize="10" GridLines="None" Width="98%" OnItemCommand="grdPatentesColidentes_ItemCommand"
                                                                    OnItemCreated="grdPatentesColidentes_ItemCreated" OnPageIndexChanged="grdPatentesColidentes_PageIndexChanged">
                                                                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                                                                    <MasterTableView GridLines="Both">
                                                                        <RowIndicatorColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </RowIndicatorColumn>
                                                                        <ExpandCollapseColumn>
                                                                            <HeaderStyle Width="20px" />
                                                                        </ExpandCollapseColumn>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="IdRevistaPatente" HeaderText="ID" UniqueName="column6"
                                                                                Display="false">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NumeroDoProcesso" HeaderText="Processo" UniqueName="column3">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Titulo" HeaderText="Patente" UniqueName="column2">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Classificacao" HeaderText="Classificação" UniqueName="colunaClassificacao">
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
