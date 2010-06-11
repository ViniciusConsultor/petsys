<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAposta.aspx.vb" Inherits="WorkSpace.frmAposta" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Vista">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.LTF.001.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.LTF.001.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Aposta" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlAposta" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboApostas" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="400px" Skin="Vista"
                                        CausesValidation="False" EmptyMessage="Selecione uma aposta">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da aposta" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDaAposta" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Número do concurso"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtNumeroDoConcurso" runat="server">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Data do concurso"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDateInput ID="txtDataDoConcurso" runat="server">
                                    </telerik:RadDateInput>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Dezenas"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:CheckBoxList ID="cklDezenas" runat="server" EnableTheming="True" RepeatColumns="4"
                                        RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="60%" >
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr runat="server" id="pnlGerarJogos">
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Gerar jogos da aposta"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:ImageButton ID="btnGerarAposta" runat="server" ImageUrl="~/imagens/system-run.png" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock3" runat="server" Title="Jogos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlJogosDaAposta" runat="server">
                        <table class="tabela">
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="grdJogosDaAposta" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" Skin="Vista">
                                        <MasterTableView GridLines="Both">
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Jogo.DezenasToString" UniqueName="column" Visible="true" HeaderText="Dezenas">
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
