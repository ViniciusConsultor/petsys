<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlProcedimentosInternos.ascx.cs" Inherits="MP.Client.MP.ctrlProcedimentosInternos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlProcedimentosInternos" runat="server">
<table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Tipo de procedimento:"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboProcedimentosInternos" runat="server" EmptyMessage="Selecione um tipo de procedimento"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" 
                    onitemsrequested="cboProcedimentosInternos_ItemsRequested" 
                    onselectedindexchanged="cboProcedimentosInternos_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="10%">
                                    Código
                                </td>
                                <td width="46%">
                                    Descrição do tipo de procedimento
                                </td>                                
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="10%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="46%">
                                    <%#DataBinder.Eval(Container, "Attributes['DescricaoTipo']")%>
                                </td>                               
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>                
                <asp:RequiredFieldValidator ID="rfvProcedimentosInternos" runat="server" ErrorMessage="Campo deve ser informado."
                    ControlToValidate="cboProcedimentosInternos"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>

