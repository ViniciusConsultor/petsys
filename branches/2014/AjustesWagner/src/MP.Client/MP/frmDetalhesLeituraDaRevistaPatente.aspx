<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WorkSpace.Master"
 CodeBehind="frmDetalhesLeituraDaRevistaPatente.aspx.cs" Inherits="MP.Client.MP.frmDetalhesLeituraDaRevistaPatente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Detalhes do Processo" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDetalhes" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNumeroDoProcesso" runat="server" Text="Número do processo:"/>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDoProcesso" runat="server" Width="120px" Enabled="false" />
                                </td>
                            </tr>    
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNumeroDaRevista" runat="server" Text="Número da revista:"/>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDaRevista" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>                            
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataPublicacao" runat="server" Text="Data de publicação:"/>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataPublicacao" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>                            
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataProcessamento" runat="server" Text="Data de processamento:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataProcessamento" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataDeDeposito" runat="server" Text="Data de depósito:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDeDeposito" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNumeroProcessoDaPatente" runat="server" Text="Número do processo da patente:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroProcessoDaPatente" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNumeroDoPedido" runat="server" Text="Número do pedido:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroDoPedido" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataDaPublicacaoDoPedido" runat="server" Text="Data de publicação do pedido:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDaPublicacaoDoPedido" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataDeConcessao" runat="server" Text="Data de concessão:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDeConcessao" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPrioridadeUnionista" runat="server" Text="Prioridade unionista:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPrioridadeUnionista" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblClassificacaoInternacional" runat="server" Text="Classificação interncional:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtClassificacaoInternacional" runat="server" Width="120px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTitulo" runat="server" Text="Título:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTitulo" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblResumo" runat="server" Text="Resumo:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtResumo" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDadosDoPedidoDaPatente" runat="server" Text="Dados do pedido de patente:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDadosDoPedidoDaPatente" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDadosDoPedidoOriginal" runat="server" Text="Dados do pedido original:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDadosDoPedidoOriginal" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPrioridadeInterna" runat="server" Text="Prioridade interna:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPrioridadeInterna" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDepositante" runat="server" Text="Depositante:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDepositante" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblInventor" runat="server" Text="Inventor:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtInventor" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTitular" runat="server" Text="Titular:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTitular" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblProcurador" runat="server" Text="Procurador:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcurador" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDadosDepositoInternacional" runat="server" Text="Dados do depósito internacional:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDadosDepositoInternacional" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDadosPublicacaoInternacional" runat="server" Text="Dados de publicação internacional:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDadosPublicacaoInternacional" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCodigoDoDespacho" runat="server" Text="Código do despacho:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCodigoDoDespacho" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblResponsavelPagamentoImpostoDeRenda" runat="server" Text="Responsável pelo pagamento do IR (Imposto de Renda):" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtResponsavelPagamentoImpostoDeRenda" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblComplemento" runat="server" Text="Complemento:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtComplemento" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDecisao" runat="server" Text="Decisão:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDecisao" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCedente" runat="server" Text="Cedente:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCedente" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCessionaria" runat="server" Text="Cessionária:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCessionaria" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblObservacao" runat="server" Text="Observação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtObservacao" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblUltimaInformacao" runat="server" Text="Ultima informação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtUltimaInformacao" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCertificadoDeAverbacao" runat="server" Text="Certificado de averbação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCertificadoDeAverbacao" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPaisCedente" runat="server" Text="País cedente:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPaisCedente" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPaisDaCessionaria" runat="server" Text="País da cessionária:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPaisDaCessionaria" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblSetor" runat="server" Text="Setor:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtSetor" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblEnderecoDaCessionaria" runat="server" Text="Endereço da cessionária:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEnderecoDaCessionaria" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblNaturezaDoDocumento" runat="server" Text="Natureza do documento:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNaturezaDoDocumento" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblMoedaDePagamento" runat="server" Text="Moeda de pagamento:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtMoedaDePagamento" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblValor" runat="server" Text="Valor:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtValor" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPagamento" runat="server" Text="Pagamento:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPagamento" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblPrazo" runat="server" Text="Prazo:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPrazo" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblServicosIsentosDeAverbacao" runat="server" Text="Serviços isentos de averbação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtServicosIsentosDeAverbacao" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCriador" runat="server" Text="Criador:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCriador" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblLinguagem" runat="server" Text="Linguagem:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtLinguagem" runat="server" Width="100%" Rows="5" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblCampoDeAplicacao" runat="server" Text="Campo de aplicação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCampoDeAplicacao" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblTipoDePrograma" runat="server" Text="Tipo de programa:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTipoDePrograma" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataDaCriacao" runat="server" Text="Data de criação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDaCriacao" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblRegimeDeGuarda" runat="server" Text="Regime de guarda:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtRegimeDeGuarda" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblRequerente" runat="server" Text="Requerente:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtRequerente" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblRedacao" runat="server" Text="Redação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtRedacao" runat="server" Width="100%" Rows="5" TextMode="MultiLine" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblDataDaProrrogacao" runat="server" Text="Data da prorrogação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDaProrrogacao" runat="server" Width="87px" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblClassificacaoNacional" runat="server" Text="Classificação nacional:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtClassificacaoNacional" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="lblClassificacao" runat="server" Text="Classificação:" />
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtClassificacao" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
