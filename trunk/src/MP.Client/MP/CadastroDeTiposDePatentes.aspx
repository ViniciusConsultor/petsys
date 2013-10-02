<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="CadastroDeTiposDePatentes.aspx.cs" Inherits="MP.Client.MP.CadastroDeTiposDePatentes" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" 
    Style="width: 100%;" onbuttonclick="rtbToolBar_ButtonClick">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.CTP.001.0003" />
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
    <telerik:RadDock ID="RadDock1" runat="server" Title="Tipo de Patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked" Height="400px" 
                Width="260px">
                <ContentTemplate>
                    <asp:Panel ID="PanelTiposDePatentes" runat="server">
                        <table class="tabela">
                        <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Descrição:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="254px"></asp:TextBox>
                                </td>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Sigla:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtSigla" runat="server" Width="60px"></asp:TextBox>
                                </td>
                        </tr>
                    </asp:Panel>
                </ContentTemplate>                
    </telerik:RadDock>
    <telerik:RadDock ID="RadDock2" runat="server" 
            Title="Configuração de obrigações para o tipo de patente" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked" Height="400px" 
                Width="260px">
                <ContentTemplate>
                    <asp:Panel ID="PanelConfigTipoPatente" runat="server" Width="424px">
                        <table class="tabela">
                        <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Tempo (em anos) da data de Protocolo para início dos Pagtos.:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtTempoInicioPgtos" runat="server" Width="80px"></asp:TextBox>
                                </td>                                
                        </tr>
                        <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Quantidade de Pagtos.:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtQtdPgto" runat="server" Width="80px"></asp:TextBox>
                                </td>                                
                        </tr>
                         <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Intervalo entre os Pagtos. (em anos):"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtIntervaloPgto" runat="server" Width="80px"></asp:TextBox>
                                </td>                                
                        </tr>
                         <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Iniciar Pagtos. da sequência (Ex: 2ª...,3ª...):"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtIniciarPgto" runat="server" Width="80px"></asp:TextBox>
                                </td>                                
                        </tr>
                         <tr>
                                <td class="th3" colspan="2">
                                    <asp:Label ID="Label7" runat="server" Text="Descrição para o Pagto.:"></asp:Label>
                                </td>
                        </tr>
                            <tr>
                                <td class="th3" colspan="2">
                                    <asp:TextBox ID="txtDescPgto" runat="server" Width="254px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label8" runat="server" Text="Possui pagamentos intermediários:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <telerik:RadComboBox ID="cbPgtoIntermediario" Runat="server" Culture="pt-BR" 
                                        Width="80px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Não" Value="Nao" />
                                            <telerik:RadComboBoxItem runat="server" Text="Sim" Value="Sim" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label9" runat="server" 
                                        Text="Número de sequência que inicia o(s) Pagto(s) intermediário(s):"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtSeqIniPgtoInter" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label10" runat="server" 
                                        Text="Quantidade de Pagtos intermediários:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtQtdPgtoInter" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label11" runat="server" 
                                        Text="Intervalo entre os Pagtos. Intermediários (em anos):"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <asp:TextBox ID="txtIntervaloPgtoInter" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label12" runat="server" 
                                        Text="O pagamento intermediário é o pedido de exame:"></asp:Label>
                                </td>
                                <td class="td" style="height: 24px">
                                    <telerik:RadComboBox ID="cbPgtoInterPedidoExame" Runat="server" Culture="pt-BR" 
                                        Width="80px" MarkFirstMatch="false">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" 
                                                Text="Não" Value="Nao" />
                                            <telerik:RadComboBoxItem runat="server" Owner="cbPgtoInterPedidoExame" 
                                                Text="Sim" Value="Sim" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3" colspan="2">
                                    <asp:Label ID="Label13" runat="server" 
                                        Text="Descrição para o Pagto. internediário:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3" colspan="2">
                                    <asp:TextBox ID="txtDescPgtoInterm" runat="server" Width="254px"></asp:TextBox>
                                </td>
                            </tr>
                    </asp:Panel>
                </ContentTemplate>                
    </telerik:RadDock>       
    </telerik:RadDockZone>
    </telerik:RadDockLayout>  
</asp:Content>
