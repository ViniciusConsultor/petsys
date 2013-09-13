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
        End If
    End Sub

    Private Sub btnEnviarEmail_Click(sender As Object, e As System.EventArgs) Handles btnEnviarEmail.Click
        Using Servico As IServicoDeOperador = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeOperador)()

            Dim operador = Servico.ObtenhaOperadorPorLogin(txtLogin.Text)

            If operador Is Nothing Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia("Login inválido."), False)
                Exit Sub
            End If

            If operador.Pessoa.EnderecoDeEmail Is Nothing Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Você não possui e-mail cadastrado. Peça para o adminitrador do sistema cadastrá-lo ou modificar a sua senha."), False)
                Exit Sub
            End If
            
        End Using

    End Sub

    Private Sub ObtenhaEmailParaDefinicaoDaNovaSenha(operador As IOperador)
        Dim link As String



        link = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & "DefinicaoDeNovaSenha.aspx?id="

    End Sub

End Class