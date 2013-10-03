<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdCliente.aspx.vb" Inherits="WorkSpace.cdCliente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register Src="~/ctrlGrupoDeAtividade.ascx" TagName="ctrlGrupo" TagPrefix="uc1" %>

<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.NCL.008.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.NCL.008.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.NCL.008.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cliente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                    <table class="tabela" runat="server" id="pnlDadosDoCliente">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Data do cadastro"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtDataDoCadastro" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label7" runat="server" Text="Número do registro"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtNumeroDoRegistro" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="th3">
                                <asp:Label ID="Label8" runat="server" Text="Data do registro"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadDateInput ID="txtDataDoRegistro" runat="server">
                                </telerik:RadDateInput>
                            </td>
                        </tr>

                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label9" runat="server" Text="Grupo de atividade"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlGrupo ID="ctrlGrupo1" runat="server" /><asp:ImageButton ID="btnNovoGrupoDeAtividade" runat="server" ImageUrl="~/imagens/new.gif" ToolTip="Novo" CausesValidation="False"
                    CommandArgument="OPE.NCL.016.0001" />
                            </td>
                        </tr>

                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label1" runat="server" Text="Informações adicionais"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadTextBox ID="txtInformacoesAdicionais" runat="server" MaxLength="4000" TextMode="MultiLine" Width="450px" Rows="5">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Faixa salarial"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtFaixaSalarial" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Desconto automático"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtDescontoAutomatico" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Valor máximo para compras"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtValorMaximoParaCompras" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Saldo para compras"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadNumericTextBox ID="txtSaldoParaCompras" runat="server">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
