<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmVisibilidadePorEmpresa.aspx.vb" Inherits="WorkSpace.frmVisibilidadePorEmpresa" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlEmpresa.ascx" TagName="ctrlEmpresa" TagPrefix="uc2" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:radtoolbar id="rtbToolBar" runat="server" skin="Vista" style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.015.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.015.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.015.0003" />
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
    </telerik:radtoolbar>
    <telerik:raddocklayout id="RadDockLayout1" runat="server" skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Operadores" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                 <asp:Panel ID="pnlDadosDoOperador" runat="server">
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label7" runat="server" Text="Operador"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadComboBox ID="cboOperador" runat="server" EmptyMessage="Selecione um operador"
                                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                                    <HeaderTemplate>
                                        <table width="96%">
                                            <tr>
                                                <td width="43%">
                                                    Nome
                                                </td>
                                                <td width="43%">
                                                    Login
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td width="50%">
                                                    <%#DataBinder.Eval(Container, "Attributes['Login']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                         </tr>
                        </table>
                     </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="DokEmpresasVisiveis" runat="server" Title="Empresas visíveis"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlEmpresasVisiveis" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Empresa"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc2:ctrlEmpresa ID="ctrlEmpresa1" runat="server" />
                                </td>
                            </tr>
                          </table>
                    </asp:Panel>
                    </br>
                     <telerik:RadGrid ID="grdEmpresasVisiveis" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="5" GridLines="None" Width="98%">
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
                                        HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8"  >
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="Pessoa.ID" HeaderText="ID" UniqueName="column" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Pessoa.Nome" UniqueName="column1">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:raddocklayout>
</asp:Content>
