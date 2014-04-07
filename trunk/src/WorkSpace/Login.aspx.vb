Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Schedule

Partial Public Class Login
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

        Dim EmpresasVisiveis As IList(Of EmpresaVisivel) = (From Empresa In Operador.ObtenhaEmpresasVisiveis() Select New EmpresaVisivel(Empresa.Pessoa.ID.Value, Empresa.Pessoa.Nome)).ToList()

        Dim IDsDiretivas As IList(Of String) = (From Diretiva In Diretivas Select Diretiva.ID).ToList()

        Usuario = New Usuario(Operador.Pessoa.ID.Value, Operador.Pessoa.Nome, IDsDiretivas, CType(Operador.Pessoa, IPessoaFisica).Sexo.ID, EmpresasVisiveis)
        FabricaDeContexto.GetInstancia.GetContextoAtual.Usuario = Usuario



        Response.Redirect("frmEscolhaDaEmpresa.aspx")
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLimpar.Click
        txtLogin.Text = ""
        txtSenha.Text = ""
    End Sub

    Private Sub btnEsqueciMinhaSenha_Click(sender As Object, e As System.EventArgs) Handles btnEsqueciMinhaSenha.Click
        Response.Redirect("EsqueceuSenha.aspx")
    End Sub

End Class