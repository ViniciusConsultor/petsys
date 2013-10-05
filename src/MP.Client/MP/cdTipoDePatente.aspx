<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="cdTipoDePatente.aspx.cs" Inherits="MP.Client.MP.cdTipoDePatente" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="ctrlTipoDePatente.ascx" TagName="ctrlTipoDePatente" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" 
    Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
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
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">           
            <telerik:RadDock ID="RadDock1" runat="server" Title="Cadastro de tipo de patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked" Height="400px" 
                Width="260px">
                <ContentTemplate>
                    <uc1:ctrlTipoDePatente ID="ctrlTipoDePatente" runat="server" />
                    <asp:Panel ID="PanelCdTipoDePatente" runat="server">
        <table class="tabela">
            <tr>
                <td>
                    <asp:Panel ID="PanelTipoDePatente" runat="server" GroupingText="Tipo de Patente">
                    <table class="tabela">
                        <tr>
                            <td class="th3" style="width: 2%">
                            <asp:Label ID="Label1" runat="server" Text="Descrição:"></asp:Label>
                            </td>
                            <td class="th3" style="width: 8%">
                            <asp:TextBox ID="txtDescricao" runat="server" Width="310px"></asp:TextBox>
                            </td>
                            <td class="th3" style="width: 1%">
                            <asp:Label ID="Label2" runat="server" Text="Sigla:"></asp:Label>
                            </td>
                            <td class="th3">
                            <asp:TextBox ID="txtSigla" runat="server" Width="60px"></asp:TextBox>
                            </td>
                        </tr>                        
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelConfigObrigaTipoDePatente" runat="server" GroupingText="Configuração de obrigações para o tipo de patente">
                    <table class="tabela">
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label3" runat="server" Text="Tempo (em anos) da data de protocolo para início dos pagamentos:"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtTempoInicioPagamentos" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label4" runat="server" Text="Quantidade de pagamentos:"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtQuantidadePagamentos" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label5" runat="server" Text="Intervalo entre os pagamentos (em anos):"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtIntervaloPagamentos" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label6" runat="server" Text="Iniciar pagamentos da sequência (Ex: 2ª...,3ª...):"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtIniciarPagamentoSequencia" runat="server" Width="80px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td class="th3" colspan="2">
                                <asp:Label ID="Label7" runat="server" Text="Descrição para o Pagamento:"></asp:Label>
                            </td>   
                        </tr>
                        <tr>
                            <td class="th3" colspan="2">
                                <asp:TextBox ID="txtDescricaoPagamento" runat="server" Width="485px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label8" runat="server" Text="Possui pagamentos intermediários:"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <telerik:RadComboBox ID="cbPgtoIntermediario" Runat="server" Culture="pt-BR" Width="80px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Não" Value="Nao" />
                                        <telerik:RadComboBoxItem runat="server" Text="Sim" Value="Sim" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>   
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label9" runat="server" Text="Número de sequência que inicia o(s) pagamento(s) intermediário(s):"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtSequenciaInicioPagamentoIntermediario" runat="server" Width="80px"></asp:TextBox>
                            </td>   
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label10" runat="server" Text="Quantidade de pagamentos intermediários:"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtQuantidadePagamentoIntermediario" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label11" runat="server" Text="Intervalo entre os pagamentos intermediários (em anos):"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <asp:TextBox ID="txtIntervaloPagamentoIntermediario" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" style="width: 10%">
                                <asp:Label ID="Label12" runat="server" Text="O pagamento intermediário é o pedido de exame:"></asp:Label>
                            </td>
                            <td class="td" style="height: 24px">
                                <telerik:RadComboBox ID="cbPgtoInterPedidoExame" Runat="server" Culture="pt-BR" Width="80px" MarkFirstMatch="false">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" Text="Não" Value="Nao" />
                                        <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" Text="Sim" Value="Sim" />
                                   </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" colspan="2">
                                <asp:Label ID="Label13" runat="server" Text="Descrição para o pagamento internediário:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="th3" colspan="2">
                                <asp:TextBox ID="txtDescricaoPagamentoIntermediario" runat="server" Width="485px"></asp:TextBox>
                            </td>
                        </tr>
                </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
            </telerik:RadDock>      
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
