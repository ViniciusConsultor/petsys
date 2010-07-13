<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAgenda.ascx.vb"
    Inherits="WorkSpace.ctrlAgenda" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
    <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
        <telerik:RadDock ID="RadDock1" runat="server" Title="Compromissos" DefaultCommands="ExpandCollapse"
            EnableAnimation="True" Skin="Vista" DockMode="Docked">
            <ContentTemplate>
                <telerik:RadToolBar ID="RadToolBar1" runat="server" Skin="Vista" Style="width: 100%;">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo compromisso"
                            CommandName="btnNovoCompromisso" CausesValidation="False" CommandArgument="OPE.NCL.009.0001" />
                    </Items>
                </telerik:RadToolBar>
                
                <telerik:RadScheduler runat="server" ID="schCompromissos" DayStartTime="08:00:00"
                    DayEndTime="18:00:00" TimeZoneOffset="03:00:00" DataKeyField="ID"  DataDescriptionField="Descricao" DataSubjectField="Assunto"
                    DataStartField="Inicio" DataEndField="Fim">
                    <AdvancedForm Modal="true" />
                    <TimelineView UserSelectable="false" />
                    <TimeSlotContextMenuSettings EnableDefault="true" />
                    <AppointmentContextMenuSettings EnableDefault="true" />
                </telerik:RadScheduler>
            </ContentTemplate>
        </telerik:RadDock>
        <telerik:RadDock ID="RadDock2" runat="server" Title="Tarefas" DefaultCommands="ExpandCollapse"
            EnableAnimation="True" Skin="Vista" DockMode="Docked">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Nova tarefa"
                                        CommandName="btnNovaTarefa" CausesValidation="False" CommandArgument="OPE.NCL.009.0001" />
                                </Items>
                            </telerik:RadToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdTarefas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                PageSize="10" GridLines="None">
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
                                        <telerik:GridBoundColumn DataField="Assunto" HeaderText="Assunto" UniqueName="column30">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" UniqueName="column2"
                                            Visible="True">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DataDeInicio" HeaderText="Data de início" UniqueName="column33">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DataDeConclusao" HeaderText="Data de conclusão"
                                            UniqueName="column3">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status.Descricao" HeaderText="Status" UniqueName="column6">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Prioridade.Descricao" HeaderText="Prioridade"
                                            UniqueName="column1" Visible="True">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadDock>
    </telerik:RadDockZone>
</telerik:RadDockLayout>
