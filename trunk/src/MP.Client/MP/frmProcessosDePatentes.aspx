<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmProcessosDePatentes.aspx.cs" Inherits="MP.Client.MP.frmProcessosDePatentes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlOperacaoFiltro.ascx" TagName="ctrlOperacaoFiltro" TagPrefix="uc1" %>
<%@ Register Src="ctrlNaturezaPatente.ascx" TagName="ctrlNaturezaPatente" TagPrefix="uc2" %>
<%@ Register Src="~/ctrlCliente.ascx" TagName="ctrlCliente" TagPrefix="uc3" %>
<%@ Register Src="ctrlInventor.ascx" TagName="ctrlInventor" TagPrefix="uc4" %>
<%@ Register Src="ctrlTitular.ascx" TagName="ctrlTitular" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo Processo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.009.0001" />
            <telerik:RadToolBarButton runat="server" Text="Recarregar" ImageUrl="~/imagens/refresh.gif"
                CommandName="btnRecarregar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ler revista" CommandArgument="OPE.MP.009.0005"
                ImageUrl="~/imagens/processarRevista.gif" CommandName="btnLerRevista" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock3" runat="server" Title="Filtro" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Opção de filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboTipoDeFiltro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTipoDeFiltro_OnSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Operação do filtro"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc1:ctrlOperacaoFiltro ID="ctrlOperacaoFiltro1" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlDataDeCadastro">
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Data de cadastro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeCadastro" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisarPorDataDeCadastro" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorDataDeCadastro_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlProcesso">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcesso" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorProcesso" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorProcesso_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlTituloPatente">
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Título da patente"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTituloPatente" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:ImageButton ID="btnPesquisarPorTituloDaPatente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorTituloDaPatente_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlNatureza">
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Natureza"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlNaturezaPatente ID="ctrlNaturezaPatente1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorTipoDePatente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorNatureza_OnClick" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlCliente">
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc3:ctrlCliente ID="ctrlCliente1" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorCliente" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorCliente_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlInventor">
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Inventor"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc4:ctrlInventor ID="ctrlInventor" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorIventor" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorInventor_OnClick_" />
                                </td>
                            </tr>
                            <tr runat="server" id="pnlTitular">
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Titular"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc5:ctrlTitular ID="ctrlTitular" runat="server" />
                                    <asp:ImageButton ID="btnPesquisarPorTitular" runat="server" ImageUrl="~/imagens/find.gif"
                                        ToolTip="Pesquisar" OnClick="btnPesquisarPorTitular_OnClick_" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="rdkProcessosDePatentes" runat="server" Title="Processos de Patentes"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="grdProcessosDePatentes" runat="server" AutoGenerateColumns="False"
                                    AllowCustomPaging="true" AllowPaging="True" PageSize="20" GridLines="None" Skin="Vista"
                                    AllowFilteringByColumn="false" OnPageIndexChanged="grdProcessosDePatentes_OnPageIndexChanged"
                                    OnItemCommand="grdProcessosDePatentes_OnItemCommand">
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
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8"
                                                ConfirmDialogType="RadWindow" ConfirmText="Deseja mesmo excluir o processo?"
                                                ConfirmTitle="Apagar processo">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>
                                            <%--<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Email" FilterImageToolTip="Enviar e-mail"
                                                HeaderTooltip="Enviar e-mail" ImageUrl="~/imagens/email.gif" UniqueName="column9">
                                                <ItemStyle Width="2%"></ItemStyle>
                                            </telerik:GridButtonColumn>--%>
                                            <telerik:GridBoundColumn DataField="IdProcessoDePatente" HeaderText="ID" UniqueName="column1"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Processo" HeaderText="Processo" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Patente.NaturezaPatente.SiglaNatureza" HeaderText="Natureza"
                                                UniqueName="column3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Patente.TituloPatente" HeaderText="Patente" UniqueName="column2">
                                                <ItemStyle Width="50%"></ItemStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDoCadastro" HeaderText="Data do cadastro"
                                                UniqueName="column6" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="15%"></ItemStyle>
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
