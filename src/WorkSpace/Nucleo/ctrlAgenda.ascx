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
                
                <telerik:RadScheduler runat="server" DisplayDeleteConfirmation ="false"  
                    ID="schCompromissos" DataKeyField="ID"  
                    DataDescriptionField="Descricao" DataSubjectField="Assunto"
                    DataStartField="Inicio" DataEndField="Fim" 
                    AllowEdit="False" AllowInsert="False" Culture="Portuguese (Brazil)" 
                    EditFormTimeFormat="hh:mm" EnableAdvancedForm="False" FirstDayOfWeek="Monday" 
                    LastDayOfWeek="Friday" ShowFullTime="True" Width="100%" 
                    EnableDescriptionField="True" HoursPanelTimeFormat="HH:mm" 
                    StartEditingInAdvancedForm="False">
                    <Localization AdvancedAllDayEvent="Todo dia" AdvancedCalendarCancel="Cancelar" 
                        AdvancedClose="Fechar" AdvancedDay="Dia" AdvancedDays="dia(s)" 
                        AdvancedDescription="Descrição" />
                    <AdvancedForm Enabled="False" TimeFormat="hh:mm" />
                    <TimelineView UserSelectable="false" />
                    <TimeSlotContextMenuSettings EnableDefault="true" />
                </telerik:RadScheduler>
            </ContentTemplate>
        </telerik:RadDock>
        <telerik:RadDock ID="RadDock2" runat="server" Title="Tarefas" DefaultCommands="ExpandCollapse"
            EnableAnimation="True" Skin="Vista" DockMode="Docked">
            <ContentTemplate>
                            <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Nova tarefa"
                                        CommandName="btnNovaTarefa" CausesValidation="False" CommandArgument="OPE.NCL.009.0001" />
                                </Items>
                            </telerik:RadToolBar>
                            <telerik:RadGrid ID="grdTarefas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                PageSize="10" GridLines="None" Width="100%">
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
                                        <telerik:GridBoundColumn DataField="Prioridade.Descricao" HeaderText="Prioridade"
                                            UniqueName="column1" Visible="True">
                                        </telerik:GridBoundColumn>
                                        </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
            </ContentTemplate>
        </telerik:RadDock>
    </telerik:RadDockZone>
</telerik:RadDockLayout>
