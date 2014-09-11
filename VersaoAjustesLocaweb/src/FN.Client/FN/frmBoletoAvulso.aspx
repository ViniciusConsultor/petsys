<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmBoletoAvulso.aspx.cs" Inherits="FN.Client.FN.frmBoletoAvulso" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc5" TagName="ctrlCliente" Src="~/ctrlCliente.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ctrlCedente" Src="~/ctrlCedente.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Boleto bancário" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <table class="tabela">
                        <tr>
                            <td class="th3">
                                <asp:Label ID="Label13" runat="server" Text="Cedente"></asp:Label>
                            </td>
                            <td class="td">
                                <uc1:ctrlCedente ID="ctrlCedente" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="th3">
                                <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                            </td>
                            <td class="td">
                                <uc5:ctrlCliente ID="ctrlCliente" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlDados" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Vencimento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtVencimento" runat="server">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Valor"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadNumericTextBox ID="txtValor" runat="server" Width="87px" DataType="System.Currency">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label14" runat="server" Text="Descrição do número do boleto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDoBoleto" runat="server" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Nome / Razão Social"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNome" runat="server" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="CNPJ / CPF"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCNPJCPF" runat="server" Width="150px">
                                    </telerik:RadTextBox>
                                    Ex.: (xx.xxx.xxx/xxxx-xx ou xxx.xxx.xxx-xx)
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Endereço"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEndereco" runat="server" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Cep"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCep" runat="server" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Bairro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtBairro" runat="server" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Cidade"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCidade" runat="server" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Estado"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEstado" runat="server" Width="87px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label15" runat="server" Text="Instruções para o agente financeiro"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtInstrucoes" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="470px">
                                    </telerik:RadTextBox>
                                    <br/>
                                    (Ex: Não Receber após o vencimento.)
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Informações do Recibo do Sacado"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtFinalidadeBoleto" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="470px" >
                                    </telerik:RadTextBox>
                                    <br/>
                                    (Informar qual parcela, ou se é anuidade e também referente a qual marca ou patente.)
                                </td>
                            </tr>                            
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" Text="Gerar boleto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadButton ID="btnGerarBoleto" runat="server" Text="Gerar Boleto" Skin="Vista"
                                        OnClick="btnGerarBoleto_ButtonClick">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
