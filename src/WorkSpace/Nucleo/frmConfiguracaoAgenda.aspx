<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmConfiguracaoAgenda.aspx.vb" Inherits="WorkSpace.frmConfiguracaoAgenda" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument ="OPE.NCL.009.0001"/>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument ="OPE.NCL.009.0002"/>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument ="OPE.NCL.009.0003"/>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Cancelar"
                CommandName="btnCancelar" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/yes.gif" Text="Sim"
                CommandName="btnSim" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/cancel.gif" Text="Não"
                CommandName="btnNao" CausesValidation="False" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Configuração" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlConfiguracao" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboNome" runat="server" LoadingMessage="Aguarde..." 
                                        Skin="Vista" EmptyMessage="Selecione uma configuração" AllowCustomText="True" 
                                        AutoPostBack="True" CausesValidation="False" Width="400px" EnableLoadOnDemand="True">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="cboNome" ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados da configuração" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDaConfiguracao" runat="server">
                        <table>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Horário de início"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTimePicker ID="txtHorarioDeInicio" runat="server" 
                                        Culture="Portuguese (Brazil)" Skin="Vista" MinDate="">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <TimeView CellSpacing="-1" Culture="Portuguese (Brazil)" Skin="Vista">
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                        <DateInput LabelCssClass="" Skin="Vista">
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtHorarioDeInicio" ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Horário de término"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTimePicker ID="txtHorarioDeTermino" runat="server" 
                                        Culture="Portuguese (Brazil)" Skin="Vista" MinDate="">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <TimeView CellSpacing="-1" Culture="Portuguese (Brazil)" Skin="Vista">
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                        <DateInput LabelCssClass="">
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtHorarioDeTermino" 
                                        ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Intervalo entre horários"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTimePicker ID="txtIntervaloEntreHorarios" runat="server" 
                                        TimeView-StartTime="00:00" TimeView-Skin="Vista" Skin="Vista" 
                                        DateInput-Skin="Vista" MinDate="">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <TimeView CellSpacing="-1" Culture="Portuguese (Brazil)" Skin="Vista">
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                        <DateInput LabelCssClass="" Skin="Vista">
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtIntervaloEntreHorarios" 
                                        ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Primeiro dia da semana"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboPrimeiroDiaDaSemana" runat="server" 
                                        CausesValidation="False" Skin="Vista">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="cboPrimeiroDiaDaSemana" 
                                        ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
