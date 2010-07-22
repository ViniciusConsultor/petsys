<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="frmAgenda.aspx.vb" Inherits="WorkSpace.frmAgenda" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Compartilhados.Componentes.Web" Namespace="Compartilhados.Componentes.Web"
    TagPrefix="cc1" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

        function onAppointmentDoubleClick(sender, eventArgs) {
            CallServer(eventArgs.get_appointment().get_id(), "");
        }

        function ReceiveServerData(rValue) {
            var vetor = rValue.split(',');
            var url = vetor[0];
            var titulo = vetor[1];
            var idjanela = vetor[2];
            var win;
            win = new Ext.Window({
                id: idjanela,
                title: titulo,
                layout: 'fit',
                modal: true,
                width: 640,
                height: 480,
                html: "<iframe src = " + url + " width=100% height=100%></iframe>"
            });
            win.show();
        }
    </script>

    <telerik:RadToolBar ID="ToolBarPrincipal" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="pnlProprietario" runat="server" Title="Proprietário da agenda"
                DefaultCommands="ExpandCollapse" EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc2:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="pnlCompromissos" runat="server" Title="Compromissos" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadToolBar ID="ToolBarCompromisso" runat="server" Skin="Vista" Style="width: 100%;">
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo compromisso"
                                CommandName="btnNovoCompromisso" CausesValidation="False" CommandArgument="OPE.NCL.012.0001" />
                        </Items>
                    </telerik:RadToolBar>
                    <telerik:RadScheduler runat="server" ID="schCompromissos" DataKeyField="ID" DataDescriptionField="Descricao"
                        DataSubjectField="Assunto" DataStartField="Inicio" DataEndField="Fim" Width="100%"
                        EnableDescriptionField="True" OnClientAppointmentDoubleClick="onAppointmentDoubleClick" AppointmentContextMenuSettings-EnableEmbeddedBaseStylesheet="False" AppointmentContextMenuSettings-EnableEmbeddedScripts="False" AppointmentContextMenuSettings-EnableEmbeddedSkins="False" TimeSlotContextMenuSettings-EnableEmbeddedBaseStylesheet="False" TimeSlotContextMenuSettings-EnableEmbeddedScripts="False" TimeSlotContextMenuSettings-EnableEmbeddedSkins="False">
                        <TimelineView UserSelectable="false" />
                                               
                        
                    </telerik:RadScheduler>
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="pnlTarefas" runat="server" Title="Tarefas" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <telerik:RadToolBar ID="ToolBarTarefa" runat="server" Skin="Vista" Style="width: 100%;">
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Nova tarefa"
                                CommandName="btnNovaTarefa" CausesValidation="False" CommandArgument="OPE.NCL.012.0004" />
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
    
    <asp:Timer ID="Timer1" runat="server" Interval="30000">
    </asp:Timer>
    <asp:Label ID="lblInconsistencia" runat="server" Font-Size="Medium" 
        Font-Bold="True" ForeColor="#CC3300"></asp:Label>
</asp:Content>
