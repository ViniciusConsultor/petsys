<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master"
    CodeBehind="frmEscolhaDaEmpresa.aspx.vb" Inherits="WorkSpace.frmEscolhaDaEmpresa" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table id="Table3" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="left">
                            <table id="Table5" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="right" class="label">
                                        <asp:Label ID="Label4" runat="server">Empresa:</asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cboEmpresas" runat="server" Width="300px">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEntrar" runat="server" CssClass="botao130X30" Text="Iniciar sessão" ToolTip="Clique para iniciar a sessão.">
                                        </asp:Button>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
