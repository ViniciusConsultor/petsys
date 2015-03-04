<%@ Page Title="" Language="C#" MasterPageFile="~/WorkSpace.Master" AutoEventWireup="true" CodeBehind="frmProcessarEmLote.aspx.cs" Inherits="PMP.Client.PMP.frmProcessarEmLote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="btnProcessar" runat="server" Text="Processar" OnClick=btnProcessar_OnClick />
</asp:Content>
