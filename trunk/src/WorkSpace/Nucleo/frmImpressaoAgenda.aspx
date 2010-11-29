﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmImpressaoAgenda.aspx.vb" Inherits="WorkSpace.frmImpressaoAgenda" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Opções de impressão da Agenda"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label4" runat="server" Text="Imprimir compromissos?"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:CheckBox ID="chkCompromissos" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label5" runat="server" Text="Imprimir lembretes?"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:CheckBox ID="chkLembretes" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label6" runat="server" Text="Imprimir tarefas?"></asp:Label>
                            </td>
                            <td class="td">
                                 <asp:CheckBox ID="chkTarefas" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label2" runat="server" Text="Opções"></asp:Label>
                            </td>
                            <td class="td">
                                <telerik:RadComboBox ID="cboOpcoesDeImpressao" runat="server" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label3" runat="server" Text="Formato de saída"></asp:Label>
                            </td>
                            <td class="td">
                                <asp:RadioButtonList ID="rblFormato" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock3" runat="server" Title="Filtro" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Data de início"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataInicial" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label1" runat="server" Text=" a  "></asp:Label>
                                    <telerik:RadDatePicker ID="txtDataFinal" runat="server">
                                    </telerik:RadDatePicker>
                                    <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/imagens/find.gif" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
