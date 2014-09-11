Imports Compartilhados
Imports System.Text
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.DBHelper

Public Class frmConfigurarConexao

    Private Sub frmConfigurarConexao_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CarregaConfiguracaoAtual()
    End Sub

    Private Sub LimpaTela()
        txtPorta.Text = ""
        txtSenha.Text = ""
        txtServidor.Text = ""
        txtUsuario.Text = ""
        txtBancoDeDados.Text = ""
    End Sub

    Private Sub ExibaTelaSQLServer()
        txtPorta.Enabled = False
    End Sub

    Private Sub ExibaTelaMySQL()
        txtPorta.Enabled = True
    End Sub

    Private Sub CarregaConfiguracaoAtual()
        chkUtilizaUpercase.Checked = Util.SistemaUtilizaSQLUpperCase()

        If Not TemConfiguracaoSalva() Then
            ExibaTelaSQLServer()
            rbSQLServer.Checked = True
            Exit Sub
        End If

        Dim Provider As TipoDeProviderConexao

        Provider = TipoDeProviderConexao.Obtenha(CChar(Util.ObtenhaCaminhoDeConfiguracaoDoServicoDeConexao))

        If Provider.Equals(TipoDeProviderConexao.MYSQL) Then
            PreencheTelaDadosMySQL()
        Else
            PreencheTelaDadosSQLServer()
        End If

    End Sub

    Private Function TemConfiguracaoSalva() As Boolean
        Return Not String.IsNullOrEmpty(Util.ObtenhaStringDeConexao)
    End Function

    Private Sub PreencheTelaDadosMySQL()
        Dim Pedacos() As String
        Dim PedacoSenha As String
        Dim PedacoUsuario As String
        Dim PedacoBancoDeDados As String
        Dim PedacoServidor As String

        'SERVER=localhost; DATABASE=simple;Uid=root;Pwd=pet
        Dim StringDeConexaoMySQL As String = Util.ObtenhaStringDeConexao

        rbMySQL.Checked = True

        Pedacos = StringDeConexaoMySQL.Split(";"c)

        PedacoSenha = Pedacos(3)
        PedacoUsuario = Pedacos(2)
        PedacoBancoDeDados = Pedacos(1)
        PedacoServidor = Pedacos(0)

        txtBancoDeDados.Text = PedacoBancoDeDados.Substring(PedacoBancoDeDados.IndexOf("=") + 1)
        txtPorta.Text = ""
        txtSenha.Text = PedacoSenha.Substring(PedacoSenha.IndexOf("=") + 1)
        txtServidor.Text = PedacoServidor.Substring(PedacoServidor.IndexOf("=") + 1)
        txtUsuario.Text = PedacoUsuario.Substring(PedacoUsuario.IndexOf("=") + 1)
    End Sub

    Private Sub PreencheTelaDadosSQLServer()
        Dim Pedacos() As String
        Dim PedacoSenha As String
        Dim PedacoUsuario As String
        Dim PedacoBancoDeDados As String
        Dim PedacoServidor As String

        'Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=Simple;Data Source=pc-1325\SQLEXPRESS
        Dim StringDeConexaoSQLServer As String = Util.ObtenhaStringDeConexao

        rbSQLServer.Checked = True

        Pedacos = StringDeConexaoSQLServer.Split(";"c)

        PedacoSenha = Pedacos(0)
        PedacoUsuario = Pedacos(2)
        PedacoBancoDeDados = Pedacos(3)
        PedacoServidor = Pedacos(4)

        txtBancoDeDados.Text = PedacoBancoDeDados.Substring(PedacoBancoDeDados.IndexOf("=") + 1)
        txtPorta.Text = ""
        txtSenha.Text = PedacoSenha.Substring(PedacoSenha.IndexOf("=") + 1)
        txtServidor.Text = PedacoServidor.Substring(PedacoServidor.IndexOf("=") + 1)
        txtUsuario.Text = PedacoUsuario.Substring(PedacoUsuario.IndexOf("=") + 1)
    End Sub

    Private Sub btnTestar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestar.Click
        Dim Inconsistencias As String = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencias) Then
            MsgBox(Inconsistencias, MsgBoxStyle.Exclamation, "Inconsistencias")
            Exit Sub
        End If

        Dim Conexao As IConexao


        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao(ObtenhaProvider, ObtenhaStringDeConexao, chkUtilizaUpercase.Checked)
        End Using


        Dim Helper As IDBHelper = Nothing

        Try
            Helper = DBHelperFactory.Create(Conexao)

            MsgBox("Conexão testada com sucesso.")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not Helper Is Nothing Then Helper.Dispose()
        End Try


    End Sub

    Private Function ObtenhaStringDeConexaoMySQL() As String
        'SERVER=localhost; DATABASE=simple;Uid=root;Pwd=pet

        Dim StringDeConexao As String

        StringDeConexao = "SERVER=" & txtServidor.Text & "; DATABASE=" & txtBancoDeDados.Text & "; Uid=" & txtUsuario.Text & "; Pwd=" & txtSenha.Text

        Return StringDeConexao
    End Function

    Private Function ObtenhaStringDeConexaoSQLServer() As String
        '"Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=Simple;Data Source=pc-1325\SQLEXPRESS"

        Dim StringDeConexao As String

        StringDeConexao = "Password=" & txtSenha.Text & ";Persist Security Info=True;User ID=" & txtUsuario.Text & "; Initial Catalog=" & txtBancoDeDados.Text & "; Data Source=" & txtServidor.Text

        Return StringDeConexao
    End Function

    Private Function ValidaDados() As String
        Dim Inconsistencias As New StringBuilder

        If String.IsNullOrEmpty(txtBancoDeDados.Text) Then Inconsistencias.AppendLine("O banco de dados deve ser informado.")
        'If String.IsNullOrEmpty(txtPorta.Text) Then Inconsistencias.AppendLine("O banco de dados deve ser informado.")
        If String.IsNullOrEmpty(txtSenha.Text) Then Inconsistencias.AppendLine("A senha deve ser informada.")
        If String.IsNullOrEmpty(txtServidor.Text) Then Inconsistencias.AppendLine("O servidor deve ser informado.")
        If String.IsNullOrEmpty(txtUsuario.Text) Then Inconsistencias.AppendLine("O usuário deve ser informado.")

        Return Inconsistencias.ToString
    End Function

    Private Function ObtenhaProvider() As TipoDeProviderConexao
        If rbMySQL.Checked Then Return TipoDeProviderConexao.MYSQL

        Return TipoDeProviderConexao.SQLSERVER
    End Function

    Private Function ObtenhaStringDeConexao() As String
        Dim Provider As TipoDeProviderConexao

        Provider = ObtenhaProvider()

        If Provider.Equals(TipoDeProviderConexao.MYSQL) Then Return ObtenhaStringDeConexaoMySQL()

        Return ObtenhaStringDeConexaoSQLServer()
    End Function

    Private Sub rbMySQL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbMySQL.CheckedChanged
        If CType(sender, RadioButton).Checked Then
            LimpaTela()
            ExibaTelaMySQL()
        End If
        
    End Sub

    Private Sub rbSQLServer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSQLServer.CheckedChanged
        If CType(sender, RadioButton).Checked Then
            LimpaTela()
            ExibaTelaSQLServer()
        End If
    End Sub

    Private Sub btnSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim Inconsistencias As String = ValidaDados()

        If Not String.IsNullOrEmpty(Inconsistencias) Then
            MsgBox(Inconsistencias, MsgBoxStyle.Exclamation, "Inconsistencias")
            Exit Sub
        End If

        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao(ObtenhaProvider, ObtenhaStringDeConexao, chkUtilizaUpercase.Checked)
            Servico.Configure(Conexao)
        End Using

    End Sub

End Class