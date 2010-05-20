Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio
Imports Compartilhados

Partial Public Class frmPainelDeControle
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
            CarregueConfiguracao()
        End If
    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.010"
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Private Sub ExibaTelaInicial()
        cboTipoDeServidor.Items.Clear()

        For Each Tipo As TipoDeServidorDeEmail In TipoDeServidorDeEmail.ObtenhaTodos
            cboTipoDeServidor.Items.Add(New RadComboBoxItem(Tipo.Descricao, Tipo.ID))
        Next
    End Sub

    Private Sub CarregueConfiguracao()
        Dim Configuracao As IConfiguracaoDoSistema

        Using Servico As IServicoDeConfiguracoesDoSistema = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConfiguracoesDoSistema)()
            Configuracao = Servico.ObtenhaConfiguracaoDoSistema
        End Using

        If Not Configuracao Is Nothing Then
            chkNotificarErrosNaAplicacaoAutomaticamente.Checked = Configuracao.NotificarErrosAutomaticamente

            cboTipoDeServidor.SelectedValue = Configuracao.ConfiguracaoDeEmailDoSistema.TipoDoServidor.ID
            txtServidorDeSaidaDeEmail.Text = Configuracao.ConfiguracaoDeEmailDoSistema.ServidorDeSaidaDeEmail
            txtPorta.Text = Configuracao.ConfiguracaoDeEmailDoSistema.Porta.ToString
            chkSSL.Checked = Configuracao.ConfiguracaoDeEmailDoSistema.HabilitarSSL
            chkRequerAutenticacao.Checked = Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao

            If Configuracao.ConfiguracaoDeEmailDoSistema.RequerAutenticacao Then
                txtUsuarioAutenticacaoServidorDeSaida.Text = Configuracao.ConfiguracaoDeEmailDoSistema.UsuarioDeAutenticacaoDoServidorDeSaida
                txtSenhaUsuarioAutenticacaoServidorDeSaida.Text = Configuracao.ConfiguracaoDeEmailDoSistema.SenhaDoUsuarioDeAutenticacaoDoServidorDeSaida
            End If

            txtRemetente.Text = Configuracao.ConfiguracaoDeEmailDoSistema.EmailRemetente
        End If

    End Sub

End Class