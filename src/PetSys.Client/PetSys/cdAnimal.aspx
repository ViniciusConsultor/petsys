<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/WorkSpace.Master"
    CodeBehind="cdAnimal.aspx.vb" Inherits="PetSys.Client.cdAnimal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ctrlPessoa.ascx" TagName="ctrlPessoa" TagPrefix="uc1" %>
<%@ Register src="crtlAnimal.ascx" tagname="crtlAnimal" tagprefix="uc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadToolBar ID="rtbToolBar" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/new.gif" Text="Novo"
                CommandName="btnNovo" CausesValidation="False" CommandArgument="OPE.PET.002.0001" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/edit.gif" Text="Modificar"
                CommandName="btnModificar" CausesValidation="False" CommandArgument="OPE.PET.002.0002" />
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/delete.gif" Text="Excluir"
                CommandName="btnExcluir" CausesValidation="False" CommandArgument="OPE.PET.002.0003" />
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
           
            <telerik:RadDock ID="RadDock1" runat="server" Title="Animal" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                  <uc2:crtlAnimal ID="crtlAnimal1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock3" runat="server" Title="Proprietário" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <uc1:ctrlPessoa ID="ctrlPessoa1" runat="server" />
                </ContentTemplate>
            </telerik:RadDock>
            <telerik:RadDock ID="RadDock2" runat="server" Title="Dados do animal" DefaultCommands="ExpandCollapse"
                EnableAnimation="True" Skin="Vista" DockMode="Docked">
                <ContentTemplate>
                    <asp:Panel ID="pnlDadosDoAnimal" runat="server">
                        <table class="tabela">
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label5" runat="server" Text="Foto"></asp:Label>
                                </td>
                                <td class="td">
                                    <radscriptblock id="RadScriptBlock1" runat="server">
                                                <script type="text/javascript">
                                                    function conditionalPostback(sender, args) {
                                                        if (args.get_eventTarget() == "<%= ButtonSubmit.UniqueID %>") {
                                                            args.set_enableAjax(false);
                                                        }
                                                    }
                                                </script>
                                            </radscriptblock>
                                    <asp:Image ID="imgFoto" runat="server" />
                                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" ClientEvents-OnRequestStart="conditionalPostback">
                                        <telerik:RadUpload ID="uplFoto" runat="server" AllowedFileExtensions=".jpg,.jpeg,.gif,.bmp"
                                            ControlObjectsVisibility="None" Culture="Portuguese (Brazil)" OverwriteExistingFiles="True"
                                            Skin="Vista">
                                            <Localization Select="Buscar" />
                                        </telerik:RadUpload>
                                        <asp:Button ID="ButtonSubmit" runat="server" Text="Upload" 
                                            CausesValidation="False" />
                                    </telerik:RadAjaxPanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label1" runat="server" Text="Data de nascimento"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadDatePicker ID="txtDataDeNascimento" runat="server" Skin="Vista">
                                        <Calendar Skin="Vista" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="Campo deve ser informado." 
                                        ControlToValidate="txtDataDeNascimento"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label2" runat="server" Text="Sexo"></asp:Label>
                                </td>
                                <td class="td">
                                    <asp:RadioButtonList ID="rblSexo" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="rblSexo" ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label3" runat="server" Text="Raça"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadTextBox ID="txtRaca" runat="server">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtRaca" ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="th3">
                                    <asp:Label ID="Label4" runat="server" Text="Espécie"></asp:Label>
                                </td>
                                <td class="td">
                                    <telerik:RadComboBox ID="cboEspecie" runat="server">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="cboEspecie" ErrorMessage="Campo deve ser informado."></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </telerik:RadDock>
        </telerik:RadDockZone>
    </telerik:RadDockLayout>
    
     <telerik:RadToolBar ID="toolBarRodape" runat="server" Skin="Vista" Style="width: 100%;">
        <Items>
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/vacina.gif" Text="Vacinas"
                CommandName="btnVacinas" CausesValidation="False"  />
                
            <telerik:RadToolBarButton runat="server" ImageUrl="~/imagens/atendimento14.gif" Text="Atendimentos"
                CommandName="btnAtendimentos" CausesValidation="False"  />
        </Items>
    </telerik:RadToolBar>
</asp:Content>
