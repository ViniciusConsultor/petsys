<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTipoEndereco.ascx.vb"
    Inherits="WorkSpace.ctrlTipoEndereco" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radcombobox id="cboTiposDeEndereco" runat="server" autopostback="True" enableloadondemand="True"
    loadingmessage="Carregando..." markfirstmatch="false" showdropdownontextboxclick="False"
    allowcustomtext="True" highlighttemplateditems="True" width="90%" skin="Vista"
    causesvalidation="False" emptymessage="Selecione um tipo de endereço" maxlength="255">
    </telerik:radcombobox>
