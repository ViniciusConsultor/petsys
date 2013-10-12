﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdProcessoDeMarca.aspx.cs" Inherits="MP.Client.MP.cdProcessoDeMarca" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlMarcas.ascx" TagName="ctrlMarcas" TagPrefix="uc1" %>
<%@ Register Src="ctrlDespachoDeMarcas.ascx" TagName="ctrlDespachoDeMarcas" TagPrefix="uc2" %>
<%@ Register Src="ctrlProcurador.ascx" TagName="ctrlProcurador" TagPrefix="uc3" %>
<%@ Register Src="ctrlSituacaoDoProcesso.ascx" TagName="ctrlSituacaoDoProcesso" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;"
        OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do processo de marca"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlProcessoDeMarca" runat="server">
                        <uc1:ctrlMarcas ID="ctrlMarcas1" runat="server" />
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Protocolo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtProtocolo" runat="server" Width="87px" Type="Number">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                        </NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Data de entrada"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeEntrada" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtProcesso" runat="server" Width="87px" Type="Number">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false">
                                        </NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Data de concessão"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeConcessao" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Processo é de terceiro?"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblProcessoEhDeTerceiro" runat="server">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <uc2:ctrlDespachoDeMarcas ID="ctrlDespacho" runat="server" />
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Data de renovação"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataRenovacao" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                        <uc3:ctrlProcurador ID="ctrlProcurador" runat="server" />
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Situação do processo"></asp:Label>
                                </td>
                                <td class="td">
                                    <uc4:ctrlSituacaoDoProcesso ID="ctrlSituacao" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
