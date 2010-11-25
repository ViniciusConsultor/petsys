<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdCompromisso.aspx.vb" Inherits="WorkSpace.cdCompromisso" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/save.gif" Text="Salvar"
                CommandName="btnSalvar" CausesValidation="True" />
            <telerik:RadToolBarButton runat="server" Text="Ajuda" ImageUrl="~/imagens/help.gif" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadDockLayout ID="RadDockLayout1" runat="server" Skin="Vista">
        <telerik:RadDockZone ID="RadDockZone1" runat="server" Skin="Vista">
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do compromisso" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDoCompromisso" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Data e horário de início"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDateTimePicker ID="txtDataHorarioInicio" runat="server">
                                    </telerik:RadDateTimePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Data e horário de término"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDateTimePicker ID="txtDataHorarioFim" runat="server">
                                    </telerik:RadDateTimePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Assunto"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadEditor ID="txtAssunto" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                                        Language="pt-BR" Skin="Vista" Width="350px">
                                        <FontNames>
                                            <telerik:EditorFont Value="Arial" />
                                            <telerik:EditorFont Value="Times New Roman" />
                                        </FontNames>
                                        <Colors>
                                            <telerik:EditorColor Index="0" Title="Preto" Value="Black" />
                                            <telerik:EditorColor Index="0" Title="Cinza" Value="Gray" />
                                            <telerik:EditorColor Index="0" Title="Vermelho" Value="Red" />
                                            <telerik:EditorColor Index="0" Title="Marron" Value="Brown" />
                                            <telerik:EditorColor Index="0" Title="Rosa" Value="Pink" />
                                            <telerik:EditorColor Index="0" Title="Amarelo" Value="Yellow" />
                                            <telerik:EditorColor Index="0" Title="Azul" Value="Blue" />
                                            <telerik:EditorColor Index="0" Title="Laranja" Value="Orange" />
                                        </Colors>
                                        <Content>
                                        
</Content>
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Local"></asp:Label>
                                </td>
                                <td class="td">
                                   <telerik:RadEditor ID="txtLocal" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                                        Language="pt-BR" Skin="Vista" Width="350px">
                                        <FontNames>
                                            <telerik:EditorFont Value="Arial" />
                                            <telerik:EditorFont Value="Times New Roman" />
                                        </FontNames>
                                        <Colors>
                                            <telerik:EditorColor Index="0" Title="Preto" Value="Black" />
                                            <telerik:EditorColor Index="0" Title="Cinza" Value="Gray" />
                                            <telerik:EditorColor Index="0" Title="Vermelho" Value="Red" />
                                            <telerik:EditorColor Index="0" Title="Marron" Value="Brown" />
                                            <telerik:EditorColor Index="0" Title="Rosa" Value="Pink" />
                                            <telerik:EditorColor Index="0" Title="Amarelo" Value="Yellow" />
                                            <telerik:EditorColor Index="0" Title="Azul" Value="Blue" />
                                            <telerik:EditorColor Index="0" Title="Laranja" Value="Orange" />
                                        </Colors>
                                        <Content>
                                        
</Content>
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Descrição"></asp:Label>
                                </td>
                                <td class="td">
                                 <telerik:RadEditor ID="txtDescricao" runat="server" EditModes="Design" ToolsFile="~/RadEditor/ToolsFile.xml"
                                        Language="pt-BR" Skin="Vista" Width="350">
                                        <FontNames>
                                            <telerik:EditorFont Value="Arial" />
                                            <telerik:EditorFont Value="Times New Roman" />
                                        </FontNames>
                                        <Colors>
                                            <telerik:EditorColor Index="0" Title="Preto" Value="Black" />
                                            <telerik:EditorColor Index="0" Title="Cinza" Value="Gray" />
                                            <telerik:EditorColor Index="0" Title="Vermelho" Value="Red" />
                                            <telerik:EditorColor Index="0" Title="Marron" Value="Brown" />
                                            <telerik:EditorColor Index="0" Title="Rosa" Value="Pink" />
                                            <telerik:EditorColor Index="0" Title="Amarelo" Value="Yellow" />
                                            <telerik:EditorColor Index="0" Title="Azul" Value="Blue" />
                                            <telerik:EditorColor Index="0" Title="Laranja" Value="Orange" />
                                        </Colors>
                                        <Content>
                                        
</Content>
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboStatus" runat="server">
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
