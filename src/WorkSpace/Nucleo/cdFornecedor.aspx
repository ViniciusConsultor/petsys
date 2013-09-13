<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdFornecedor.aspx.vb" Inherits="WorkSpace.cdFornecedor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.013.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.013.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.013.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Fornecedor" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                    <table class="tabela" runat="server" id="pnlDadosDoFornecedor">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Data do cadastro"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtDataDoCadastro" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Contatos do fornecedor" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                     <table class="tabela" runat="server" id="pnlDadosDosContatos">
                        <tr>
                            <td colspan="2">
                                <uc1:ctrlPessoa ID="ctrlPessoa2" runat="server" />
                            </td>
                        </tr>
                       </table>

                                <asp:Panel ID="pnlContatos" runat="server">
                                    <telerik:RadGrid ID="grdContato" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        PageSize="10" GridLines="None" Width="98%">
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
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nome" UniqueName="column1">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </asp:Panel>
                           
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
