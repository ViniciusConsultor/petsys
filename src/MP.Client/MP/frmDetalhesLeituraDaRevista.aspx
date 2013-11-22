<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true"
    CodeBehind="frmDetalhesLeituraDaRevista.aspx.cs" Inherits="MP.Client.MP.frmDetalhesLeituraDaRevista" %>

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
                                    <asp:Label ID="Label1" runat="server" Text="Processo:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcesso" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Data de depósito:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDeposito" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Data de concessão:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDeConcessao" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Data de vigência:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataDeVigencia" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Texto Complementar:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTextoComplementar" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label25" runat="server" Text="Protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroProtocoloDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label26" runat="server" Text="Data do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataProtocoloDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label27" runat="server" Text="Código do serviço do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtCodigoServicoProtocoloDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label28" runat="server" Text="Requerente do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtRazaoSocialRequerenteProtocoloDespacho" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label29" runat="server" Text="País do requerente do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPaisRequerenteProtocoloDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label30" runat="server" Text="Estado do requerente do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEstadoRequerenteProtocoloDespacho" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label31" runat="server" Text="Procurador do protocolo do despacho:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcuradorProtocoloDespacho" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label7" runat="server" Text="Titular:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTitular" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Estado:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEstado" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" Text="País:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPais" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" Text="Marca:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtMarca" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" Text="Apresentação:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtApresentacao" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label12" runat="server" Text="Natureza:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNatureza" runat="server" Width="100px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label13" runat="server" Text="Tradução da marca:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtTraducao" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label14" runat="server" Text="NCL:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNCL" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label15" runat="server" Text="Edição NCL:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEdicaoNCL" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label16" runat="server" Text="Especificação NCL:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtEspecificacaoNCL" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label17" runat="server" Text="Classificação viena:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboClassificacaoViena" runat="server" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="50%" Skin="Vista"
                                        CausesValidation="False" AutoPostBack="True" >
                                        <HeaderTemplate>
                                            <table width="56%">
                                                <tr>
                                                    <td width="28%">
                                                        Edição
                                                    </td>
                                                    <td width="28%">
                                                        Código
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="60%">
                                                <tr>
                                                    <td width="30%">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td width="30%">
                                                        <%#DataBinder.Eval(Container, "Attributes['Codigo']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label18" runat="server" Text="Classificação nacional:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboClassificacaoNacional" runat="server" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="80%" Skin="Vista"
                                        CausesValidation="False" AutoPostBack="True" >
                                        <HeaderTemplate>
                                            <table width="86%">
                                                <tr>
                                                    <td width="13%">
                                                        Código
                                                    </td>
                                                    <td width="60%">
                                                        Especificação
                                                    </td>
                                                    <td width="13%">
                                                        Sub Classes
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="90%">
                                                <tr>
                                                    <td width="15%">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td width="60%">
                                                        <%#DataBinder.Eval(Container, "Attributes['Especificacao']")%>
                                                    </td>
                                                    <td width="15%">
                                                        <%#DataBinder.Eval(Container, "Attributes['SubClasse']")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label19" runat="server" Text="Apostila:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtApostila" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="5" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label20" runat="server" Text="Procurador:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtProcurador" runat="server" MaxLength="4000" TextMode="MultiLine"
                                        Rows="2" Width="470px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label21" runat="server" Text="Data da prioridade unionista:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtDataPrioridadeUnionista" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label22" runat="server" Text="Número da prioridade unionista:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtNumeroPriUnionista" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr>
                                <td class="th3">
                                    <asp:Label ID="Label23" runat="server" Text="País da prioridade unionista:"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtPaisPriUnionista" runat="server" Width="87px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label24" runat="server" Text="Sobrestador(es):"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboSobrestador" runat="server" EnableLoadOnDemand="True"
                                        LoadingMessage="Carregando..." MarkFirstMatch="false" ShowDropDownOnTextboxClick="False"
                                        AllowCustomText="True" HighlightTemplatedItems="True" Width="80%" Skin="Vista"
                                        CausesValidation="False" AutoPostBack="True" >
                                        <HeaderTemplate>
                                            <table width="86%">
                                                <tr>
                                                    <td width="25%">
                                                        Processo
                                                    </td>
                                                    <td width="61%">
                                                        Marca
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="90%">
                                                <tr>
                                                    <td width="27%">
                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                    </td>
                                                    <td width="63%">
                                                        <%#DataBinder.Eval(Container, "Attributes['Marca']")%>
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
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
