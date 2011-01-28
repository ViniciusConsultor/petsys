<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdAtendimento.aspx.vb" Inherits="PetSys.Client.cdAtendimento" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="crtlAnimalResumido.ascx" TagName="crtlAnimalResumido" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="pnlDadosDoAnimal" runat="server" Title="Animal" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:crtlAnimalResumido ID="crtlAnimalResumido1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="pnlDadosGerais" runat="server" Title="Dados do gerais" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Data e hora do atendimento"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblDataEHora" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Data do retorno"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtDataDoRetorno" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Veterinário"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:Label ID="lblVeterinario" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock3" runat="server" Title="Dados específicos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Vista"
                        MultiPageID="RadMultiPage1" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab Text="Vacinas" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Vermífugos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Prontuário do atendimento">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Exames">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Receituário">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <asp:Panel ID="pnlDadosDaVacina" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label2" runat="server" Text="Data da vacinação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDateInput ID="txtDataDaVacinacao" runat="server">
                                            </telerik:RadDateInput>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label3" runat="server" Text="Nome"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeDaVacina" runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label11" runat="server" Text="Observação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacaoDaVacina" runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label12" runat="server" Text="Revacinar em"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDateInput ID="txtRevacinar" runat="server">
                                            </telerik:RadDateInput>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2" align="right">
                                            <asp:ImageButton ID="btnAdicionarVacina" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdVacinas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="10" GridLines="None" Width="100%">
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
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column2">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column3" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DataDaVacinacao" HeaderText="Aplicada em" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Observacao" HeaderText="Observação" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RevacinarEm" HeaderText="Reaplicar em" UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView5" runat="server">
                            <asp:Panel ID="pnlDadosDoVermifugo" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label13" runat="server" Text="Data"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDateInput ID="txtDataVermifugo" runat="server">
                                            </telerik:RadDateInput>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label14" runat="server" Text="Nome"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtNomeVermifugo" runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label15" runat="server" Text="Observação"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadTextBox ID="txtObservacaoVermifugo" runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label16" runat="server" Text="Próxima dose"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <telerik:RadDateInput ID="txtProximaDose" runat="server">
                                            </telerik:RadDateInput>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="campodependente" colspan="2" align="right">
                                            <asp:ImageButton ID="btnAdicionarVermifugo" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <telerik:RadGrid ID="grdVermifugos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    PageSize="10" GridLines="None" Width="100%">
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
                                                HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column2">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column3" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" UniqueName="column4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Data" HeaderText="Data" UniqueName="column5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Observacao" HeaderText="Observação" UniqueName="column6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProximaDoseEm" HeaderText="Próxima dose" UniqueName="column7">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" SkinID="Vista">
                            <asp:Panel ID="pnlProntuario" runat="server">
                                <table class="tabela">
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label10" runat="server" Text="Peso do animal"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtPesoDoAnimal" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label6" runat="server" Text="Queixa"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtQueixas" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label7" runat="server" Text="Sinais clínicos"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtSinaisClinicos" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label8" runat="server" Text="Prognóstico"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtPrognostico" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="th3">
                                            <asp:Label ID="Label9" runat="server" Text="Tratamento"></asp:Label>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtTratamento" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server" SkinID="Vista">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server" SkinID="Vista">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
