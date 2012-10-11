Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
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

        Dim IDsDiretivas As IList(Of String) = (From Diretiva In Diretivas Select Diretiva.ID).ToList()

        Usuario = New Usuario(Operador.Pessoa.ID.Value, Operador.Pessoa.Nome, IDsDiretivas, CType(Operador.Pessoa, IPessoaFisica).Sexo.ID)
        FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Usuario
        Response.Redirect("Desktop.aspx")
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLimpar.Click
        txtLogin.Text = ""
        txtSenha.Text = ""
    End Sub

    Private Sub Login_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Session.IsNewSession OrElse Not IsPostBack Then
            Dim Empresa As IEmpresa = Nothing

            Try
                Using Servico As IServicoDeEmpresa = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeEmpresa)()
                    Empresa = Servico.Obtenha()
                End Using
            Catch

            End Try
            
            lblNomeEmpresa.Text = "EMPRESA NÃO INFORMADA"

            If Not Empresa Is Nothing Then lblNomeEmpresa.Text = Empresa.Pessoa.Nome
        End If
    End Sub

End Class