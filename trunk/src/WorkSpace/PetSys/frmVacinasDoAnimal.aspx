<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master" CodeBehind="frmVacinasDoAnimal.aspx.vb" Inherits="WorkSpace.frmVacinasDoAnimal" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" AutoPostBack="True" Skin="Vista"
        Style="width: 100%;">
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock1" runat="server" Title="Vacinas do animal" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                      <asp:Panel ID="pnlVacinasDoAnimal" runat="server">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" ShowFooter="True" Skin="Vista">
                            <MasterTableView>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>    
            </telerik:RadDockZone>    
    </telerik:RadDockLayout>
</asp:Content>