<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlSituacaoDoProcesso.ascx.cs" Inherits="MP.Client.MP.ctrlSituacaoDoProcesso" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlSituacaoDoProcesso" runat="server">
<table class="tabela">
        <tr>          
            <td class="td">
                <telerik:RadComboBox ID="cboSituacaoDoProcesso" runat="server" EmptyMessage="Selecione um tipo de situação"
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="80%" Skin="Vista" CausesValidation="False" AutoPostBack="True"
                    onitemsrequested="cboSituacaoDoProcesso_ItemsRequested" 
                    onselectedindexchanged="cboSituacaoDoProcesso_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="10%">
                                    Código
                                </td>
                                <td width="46%">
                                    Descrição da situação
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
                                    <%#DataBinder.Eval(Container, "Attributes['DescricaoSituacao']")%>
                                </td>                               
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
        </tr>
    </table>
</asp:Panel>


