Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Core.Interfaces.Negocio
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos

Partial Public Class frmAlterarSenha
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.007"
    End Function

    Private Sub btnModificar_Click()
        Dim SenhaAtualInformada As ISenha
        Dim NovaSenha As ISenha
        Dim ConfirmacaoDaNovaSenha As ISenha

        SenhaAtualInformada = ObtenhaSenha(txtSenhaAntiga.Text)
        NovaSenha = ObtenhaSenha(txtNovaSenha.Text)
        ConfirmacaoDaNovaSenha = ObtenhaSenha(txtConfirmacaoNovaSenha.Text)

        Try
            Using Servico As IServicoDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSenha)()
                Servico.Altere(FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario.ID, _
                               SenhaAtualInformada, _
                               NovaSenha, _
                               ConfirmacaoDaNovaSenha)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Senha alterada com sucesso."), False)

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function ObtenhaSenha(ByVal SenhaDescriptografada As String) As ISenha
        Dim Senha As ISenha
        Dim SenhaTXTCript As String

        SenhaTXTCript = AjudanteDeCriptografia.CriptografeMaoUnicao(SenhaDescriptografada)

        Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {SenhaTXTCript, Now})
        Return Senha
    End Function

    Private Sub rtbToolBar_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBar.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnModificar"
                Call btnModificar_Click()
        End Select
    End Sub

End Class