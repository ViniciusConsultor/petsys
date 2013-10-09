<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdTipoDeProcedimentoInterno.aspx.cs" Inherits="MP.Client.MP.cdTipoDeProcedimentoInterno" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.003.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.003.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.003.0003" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro de tipo de procedimento interno"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Tipo de procedimento"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadComboBox ID="cboTipoDeProcedimentosInternos" runat="server" EmptyMessage="Selecione um tipo de procedimento"
                                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" OnItemsRequested="cboProcedimentosInternos_ItemsRequested"
                                    OnSelectedIndexChanged="cboProcedimentosInternos_SelectedIndexChanged" MaxLength="255">
                                    <HeaderTemplate>
                                        <table width="96%">
                                            <tr>
                                                <td width="16%">
                                                    ID
                                                </td>
                                                <td width="80%">
                                                    Descrição do tipo de procedimento
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="16%">
                                                    <%#DataBinder.Eval(Container, "Attributes['ID']")%>
                                                </td>
                                                <td width="80%">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
