Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Core.Interfaces.Servicos
Imports Core.Interfaces.Negocio

Public Class DefinicaoDeNovaSenha
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession OrElse Not IsPostBack Then
            FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Nothing
            Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
                FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Servico.ObtenhaConexao
            End Using

            If Not Request.QueryString("ID") Is Nothing Then
                Using ServicoDeSenha As IServicoDeSenha = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeSenha)()
                    Dim IDOperador As Nullable(Of Long)

                    ViewState("IDREDEFINICAO") = CLng(Request.QueryString("ID"))

                    IDOperador = ServicoDeSenha.ObtenhaIDOperadorParaRedifinirSenha(CLng(Request.QueryString("ID")))

                    btnIrParaLogin.Visible = False
                    btnRedefinir.Visible = True

                    If IDOperador.HasValue Then
                        ViewState("IDOPERADOR") = IDOperador.Value
                        Exit Sub
                    End If
                End Using
            End If

            MostraMensagemDeRequisicaoInvalida()
        End If
    End Sub

    Private Sub MostraMensagemDeRequisicaoInvalida()
        btnRedefinir.Visible = False
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Requisição de redefinição inválida ou já foi executada. Por favor solicite uma nova redefinição de senha."), False)

    End Sub

   Private Function ObtenhaSenha(ByVal SenhaDescriptografada As String) As ISenha
        Dim Senha As ISenha
        Dim SenhaTXTCript As String

        SenhaTXTCript = AjudanteDeCriptografia.CriptografeMaoUnicao(SenhaDescriptografada)

        Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {SenhaTXTCript, Now})
        Return Senha
    End Function

    Private Sub btnRedefinir_Click(sender As Object, e As System.EventArgs) Handles btnRedefinir.Click
        Dim NovaSenha As ISenha
        Dim ConfirmacaoDaNovaSenha As ISenha

        NovaSenha = ObtenhaSenha(txtNovaSenha.Text)
        ConfirmacaoDaNovaSenha = ObtenhaSenha(txtConfirmacaoNovaSenha.Text)

        Try
            Using Servico As IServicoDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSenha)()
                Servico.RedefinaSenha(CLng(ViewState("IDREDEFINICAO")), _
                                CLng(ViewState("IDOPERADOR")), _
                               NovaSenha, _
                               ConfirmacaoDaNovaSenha)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Senha alterada com sucesso."), False)

            btnRedefinir.Visible = False
            btnIrParaLogin.Visible = True
            
        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnIrParaLogin_Click(sender As Object, e As System.EventArgs) Handles btnIrParaLogin.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class