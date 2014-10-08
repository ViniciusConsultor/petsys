<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlEventos.ascx.cs"
    Inherits="MP.Client.MP.ctrlEventos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Panel ID="pnlEventos" runat="server">
    <table>
        <tr>
            <td class="th3">
                <asp:Label ID="Label33" runat="server" Text="Data"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadDatePicker ID="txtDataDoEvento" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="th3">
                <asp:Label ID="Label34" runat="server" Text="Descrição"></asp:Label>
            </td>
            <td class="td">
                <telerik:RadTextBox ID="txtDescricaoEvento" runat="server" MaxLength="4000" Skin="Vista"
                    TextMode="MultiLine" Width="400px" SelectionOnFocus="CaretToBeginning" Rows="5">
                </telerik:RadTextBox>
                <asp:Button ID="btnAdicionarEvento" runat="server" CssClass="RadUploadSubmit" Text="Adicionar evento" OnClick="btnAdicionarEvento_OnClick" />
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdEventos" runat="server" AutoGenerateColumns="False" GridLines="None"
        Skin="Vista" OnPageIndexChanged="grdEventos_OnPageIndexChanged" OnItemCommand="grdEventos_OnItemCommand">
        <MasterTableView GridLines="Both">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Excluir" UniqueName="column7"
                    ImageUrl="~/imagens/delete.gif">
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="Data" UniqueName="column1" Visible="True" HeaderText="Data"
                    DataFormatString="{0:dd/MM/yyyy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Descricao" UniqueName="column2" Visible="True"
                    HeaderText="Descrição">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>
