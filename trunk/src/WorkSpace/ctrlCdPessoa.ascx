<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCdPessoa.ascx.vb" Inherits="WorkSpace.ctrlCdPessoa" %>
<%@ Register src="~/ctrlCdPessoaFisica.ascx" tagname="ctrlCdPessoaFisica" tagprefix="uc1" %>
<%@ Register src="~/ctrlCdPessoaJuridica.ascx" tagname="ctrlCdPessoaJuridica" tagprefix="uc2" %>

<uc1:ctrlCdPessoaFisica ID="ctrlCdPessoaFisica1" runat="server" />
<uc2:ctrlCdPessoaJuridica ID="ctrlCdPessoaJuridica1" runat="server" />

