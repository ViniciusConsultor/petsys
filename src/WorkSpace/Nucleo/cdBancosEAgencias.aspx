<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdBancosEAgencias.aspx.vb" Inherits="WorkSpace.cdBancosEAgencias" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBarBancos" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.009.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.009.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.009.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Banco" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDoBanco" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Nome do banco"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboBanco" runat="server" EmptyMessage="Selecione uma banco"
                                        EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                        ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                        Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True">
                                        <HeaderTemplate>
                                            <table width="96%">
                                                <tr>
                                                    <td width="80%">
                                                        Nome
                                                    </td>
                                                    <td width="20%">
                                                        Número
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td width="80%">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td width="20%">
                                                        <%#DataBinder.Eval(Container, "Attributes['Numero']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Número do banco"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtNumeroDoBanco" runat="server" DataType="System.Int16">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="pnlAgencias" runat="server" Title="Agências" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadToolBar ID="rtbToolBarAgencias" runat="server" Skin="Vista" Style="width: 100%;">
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.FUN.NCL.009.0004" />
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.009.0005" />
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.009.0006" />
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
                    <uc1:ctrlPessoa ID="ctrlPessoa2" runat="server" />
                    <asp:Panel ID="pnlDadosDoAgencia" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Número da agência"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDaAgencia" runat="server" MaxLength="50" Skin="Vista"
                                        SelectionOnFocus="CaretToBeginning">
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
