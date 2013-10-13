<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMarcas.ascx.cs" Inherits="MP.Client.MP.ctrlMarcas" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlMarcas" runat="server" Width="100%">
<table class="tabela">
        <tr>
            <td class="th3">
                <asp:Label ID="Label6" runat="server" Text="Marca"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadComboBox ID="cboMarcas" runat="server" EmptyMessage="Selecione uma marca..."
                    EnableLoadOnDemand="True" LoadingMessage="Carregando..." MarkFirstMatch="false"
                    ShowDropDownOnTextboxClick="False" AllowCustomText="True" HighlightTemplatedItems="True"
                    Width="90%" Skin="Vista" CausesValidation="False" AutoPostBack="True" 
                    onitemsrequested="cboMarca_ItemsRequested" 
                    onselectedindexchanged="cboMarca_SelectedIndexChanged">
                    <HeaderTemplate>
                        <table width="96%">
                            <tr>
                                <td width="58%">
                                    Marca
                                </td>
                                <td width="17%">
                                    Apresentação
                                </td>
                                 <td width="17%">
                                    Natureza
                                </td>  
                                 <td width="6%">
                                    NCL
                                </td>                                
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td width="57%">
                                    <%# DataBinder.Eval(Container, "Text")%>
                                </td>
                                <td width="17%">
                                    <%#DataBinder.Eval(Container, "Attributes['Apresentacao']")%>
                                </td>
                                <td width="18%">
                                    <%#DataBinder.Eval(Container, "Attributes['Natureza']")%>
                                </td>      
                                <td width="8%">
                                    <%#DataBinder.Eval(Container, "Attributes['NCL']")%>
                                </td>                                
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadComboBox>                
               
            </td>
        </tr>
    </table>
</asp:Panel>
