Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio

Public Class EsqueceuSenha
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession OrElse Not IsPostBack Then
            FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Nothing
            Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
                FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Servico.ObtenhaConexao
            End Using
            txtLogin.Focus()

            btnEnviarEmail.Visible = True
            btnSair.Visible = False
        End If
    End Sub

    Private Sub btnEnviarEmail_Click(sender As Object, e As System.EventArgs) Handles btnEnviarEmail.Click
        Dim operador As IOperador

        Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeOperador)()
            operador = Servico.ObtenhaOperadorPorLogin(txtLogin.Text)

            If operador Is Nothing Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Login inválido."), False)
                Exit Sub
            End If

            If operador.Pessoa.EnderecosDeEmails Is Nothing Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Você não possui e-mail cadastrado. Peça para o adminitrador do sistema cadastrá-lo ou modificar a sua senha."), False)
                Exit Sub
            End If
        End Using

        Using Servico As IServicoDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSenha)()

            Try
                Servico.RegistreDefinicaoDeNovaSenha(operador, UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & "DefinicaoDeNovaSenha.aspx?id=")
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
                Exit Sub
            End Try


            btnEnviarEmail.Visible = False
            btnSair.Visible = True

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("A requisição de redefinição da sua senha foi enviada no seu e-mail."), False)
        End Using

    End Sub

    Private Sub btnSair_Click(sender As Object, e As System.EventArgs) Handles btnSair.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class