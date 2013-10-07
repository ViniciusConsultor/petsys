<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlLayoutRevistaPatente.ascx.cs" Inherits="MP.Client.MP.ctrlLayoutRevistaPatente" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlLayoutRevistaPatente" runat="server">
    <table class="tabela">
        <tr>
            <td>
                <telerik:RadGrid ID="grdLayoutRevistaPatente" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" GridLines="None" Skin="Vista">
                    <PagerStyle AlwaysVisible="True" Mode="NumericPages" />
                    <MasterTableView GridLines="Both">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Identificador" HeaderText="Nome Campo" UniqueName="column1" Visible="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DescricaoResumida" HeaderText="Descrição Resumida" UniqueName="column2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TamanhoDoCampo" HeaderText="Tamanho do Campo" UniqueName="column3">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CampoDelimitadorDoRegistro" HeaderText="Identifica Registro" UniqueName="column4">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CampoIdentificadorDoProcesso" HeaderText="Identifica Processo" UniqueName="column5" Visible="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CampoIdentificadorDeColidencia" HeaderText="Identifica Colidência" UniqueName="column6" Visible="True">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>