<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="cdTipoDePatente.aspx.cs" Inherits="MP.Client.MP.cdTipoDePatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ctrlTipoDePatente.ascx" TagName="ctrlTipoDePatente" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Width="100%" OnButtonClick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.MP.001.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.MP.001.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.MP.001.0003" />
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
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro de tipo de patente"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista">
                <ContentTemplate>
                    <uc1:ctrlTipoDePatente ID="ctrlTipoDePatente" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do tipo de patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDoTipo" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Sigla"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtSigla" runat="server" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Tempo (em anos) da data de protocolo para início dos Pagtos."></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtTempoInicioPagamentos" runat="server" Width="87px"
                                        Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Quantidade de Pagtos."></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtQuantidadePagamentos" runat="server" Width="87px"
                                        Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Intervalo entre os Pagtos. (em anos)"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtIntervaloPagamentos" runat="server" Width="87px"
                                        Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Iniciar Pagtos. da sequência (Ex: 2ª..,3ª..)"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtIniciarPagamentoSequencia" runat="server" Width="87px"
                                        Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Descrição para o Pagto."></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricaoPagamento" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Possui Pagtos. intermediários?"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cbPgtoIntermediario" runat="server" Culture="pt-BR" Width="87px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Não" Value="Nao" />
                                            <telerik:RadComboBoxItem runat="server" Text="Sim" Value="Sim" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="Número de sequência que inicia o(s) Pagto(s) intermediário(s)"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtSequenciaInicioPagamentoIntermediario" runat="server"
                                        Width="87px" Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Quantidade de Pagtos. intermediários"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtQuantidadePagamentoIntermediario" runat="server"
                                        Width="87px" Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" Text="Intervalo Pagtos. intermediários (em anos)"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtIntervaloPagamentoIntermediario" runat="server"
                                        Width="87px" Type="Number" DataType="System.Uint32">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label12" runat="server" Text="Pagto. intermediário é o pedido de exame?"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cbPgtoInterPedidoExame" runat="server" Culture="pt-BR" Width="87px"
                                        MarkFirstMatch="false">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" Text="Não"
                                                Value="Nao" />
                                            <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" Text="Sim"
                                                Value="Sim" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label13" runat="server" Text="Descrição Pagto. intermediário"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDescricaoPagamentoIntermediario" runat="server" MaxLength="4000"
                                        TextMode="MultiLine" Width="450px" Rows="5">
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
