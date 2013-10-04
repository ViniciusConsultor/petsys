<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGrupoDeAtividade.ascx.vb"
    Inherits="WorkSpace.ctrlGrupoDeAtividade" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:radcombobox id="cboGrupos" runat="server" autopostback="True" enableloadondemand="True"
    loadingmessage="Carregando..." markfirstmatch="false" showdropdownontextboxclick="False"
    allowcustomtext="True" highlighttemplateditems="True" width="90%" skin="Vista"
    causesvalidation="False" emptymessage="Selecione um grupo de atividade" maxlength="255">
    </telerik:radcombobox>
