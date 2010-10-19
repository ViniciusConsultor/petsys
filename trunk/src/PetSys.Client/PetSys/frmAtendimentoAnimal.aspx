<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAtendimentoAnimal.aspx.vb" Inherits="PetSys.Client.frmAtendimentoAnimal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="crtlAnimal.ascx" TagName="crtlAnimal" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.PET.003.0001" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Animal" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:crtlAnimal ID="crtlAnimal1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Histórico de atendimentos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlHistoricoDeAtendimentos" runat="server">
                        <telerik:RadGrid ID="grdAtendimentos" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="10" GridLines="None" Width="100%">
                            <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                            <MasterTableView GridLines="Both">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Modificar" FilterImageToolTip="Modificar"
                                        HeaderTooltip="Modificar" ImageUrl="~/imagens/edit.gif" UniqueName="column10">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" FilterImageToolTip="Excluir"
                                        HeaderTooltip="Excluir" ImageUrl="~/imagens/delete.gif" UniqueName="column8">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="column" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DataEHoraDoAtendimento" HeaderText="Data e hora"
                                        UniqueName="column30">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Veterinario.Pessoa.Nome" HeaderText="Veterinário"
                                        UniqueName="column2" Visible="True">
                                    </telerik:GridBoundColumn>
                                    
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
          
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
</asp:Content>
