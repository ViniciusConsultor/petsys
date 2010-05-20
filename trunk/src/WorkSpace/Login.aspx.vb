Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Partial Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession OrElse Not IsPostBack Then
            FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Nothing
            Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
                FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Servico.ObtenhaConexao
            End Using
            txtLogin.Focus()
        End If
    End Sub

    Protected Sub btnEntrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEntrar.Click
        Dim Operador As IOperador = Nothing
        Dim Login As String
        Dim Senha As String
        Dim Usuario As Usuario = Nothing

        Login = txtLogin.Text
        Senha = AjudanteDeCriptografia.CriptografeMaoUnicao(txtSenha.Text)

        Try
            Using Servico As IServicoDeAutenticacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutenticacao)()
                Operador = Servico.FacaLogon(Login, Senha)
            End Using

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
            Exit Sub
        End Try

        Dim Diretivas As New List(Of IDiretivaDeSeguranca)

        Using Servico As IServicoDeAutorizacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAutorizacao)()
            For Each Grupo As IGrupo In Operador.ObtenhaGrupos
                Diretivas.AddRange(Servico.ObtenhaDiretivasDeSegurancaDoGrupo(Grupo.ID.Value))
            Next
        End Using

        Dim IDsDiretivas As IList(Of String) = New List(Of String)

        For Each Diretiva As IDiretivaDeSeguranca In Diretivas
            IDsDiretivas.Add(Diretiva.ID)
        Next

        Usuario = New Usuario(Operador.Pessoa.ID.Value, Operador.Pessoa.Nome, IDsDiretivas, CType(Operador.Pessoa, IPessoaFisica).Sexo.ID)
        FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Usuario
        Response.Redirect("Desktop.aspx")
    End Sub

End Class