<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdDespachoDePatentes.aspx.cs" Inherits="MP.Client.MP.cdDespachoDePatentes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlDespachoDePatentes.ascx" TagName="ctrlDespachoDePatentes" TagPrefix="uc1" %>
<%@ Register Src="~/ctrlTemplateDeEmail.ascx" TagName="ctrlTemplateDeEmail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.010.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.010.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.010.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro do despacho de patentes"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr runat="server" id="pnlDespacho">
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Despacho"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlDespachoDePatentes ID="ctrlDespachoDePatentes" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCdDespachoDePatentes" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Título"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTitulo" runat="server" MaxLength="255" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Descrição"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="450px">
                                    </telerik:RadTextBox>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Situação do processo após a publicação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtSituacao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="450px">
                                    </telerik:RadTextBox>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Prazo para providência (dias)"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtPrazoProvidencia" runat="server" Width="87px" Type="Number"
                                        DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Providência"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProvidencia" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="450px">
                                    </telerik:RadTextBox>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Desativa processo?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblDesativaProcesso" runat="server" AutoPostBack="false"
                                        RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Agenda pagamento?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblAgendaPagamento" runat="server" AutoPostBack="false"
                                        RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                         <uc2:ctrlTemplateDeEmail ID="ctrlTemplateDeEmail" runat="server" />
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
