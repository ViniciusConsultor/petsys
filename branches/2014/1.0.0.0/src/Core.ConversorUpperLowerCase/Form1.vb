Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Conversores

Public Class Form1

    Private Sub btnConverter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConverter.Click
        Try
            ServerUtils.setCredencial(Util.ConstruaCredencial)

            MigraTabelaDeMunicipio()
            MigraTabelaDeGrupo()
            MigraTabelaDePessoa()
            MigraTabelaDePessoaFisica()
            MigraTabelaDePessoaJuridica()
            MigraTabelaDeCompromisso()
            MigraTabelaDeTarefa()

            MsgBox("Migração realizada com sucesso.")
        Catch ex As Exception
            Me.Height = 450
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro")
            txtMsgErro.Text = ObtenhaMensagemDeLog(ex)
        End Try

    End Sub

    Private Function ObtenhaMensagemDeLog(ByVal ex As Exception) As String
        Dim Erro As Exception
        Dim DataDoErro As Date = Now
        Dim Mensagem As New StringBuilder

        Erro = ex

        Mensagem.AppendLine("Foi gerado uma exceção ")
        Mensagem.AppendLine("Data do erro : " & DataDoErro.ToString("dd/MM/yyyy"))
        Mensagem.AppendLine("Hora do erro : " & DataDoErro.ToString("HH:mm:ss"))

        Do While Not Erro Is Nothing
            Mensagem.AppendLine("Origem do erro : " & Erro.Source)
            Mensagem.AppendLine("Descrição do erro : " & Erro.Message)
            Mensagem.AppendLine("Método origem : " & Erro.ToString)
            Mensagem.AppendLine("Resumo : " & Erro.StackTrace)
            Mensagem.AppendLine("-------------------------------------------------------------")
            Erro = Erro.InnerException
        Loop

        Return Mensagem.ToString
    End Function

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao
        End Using


        Me.Height = 150
        Me.Cursor = Cursors.Default
        FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Conexao
    End Sub

    Private Sub MigraTabelaDeMunicipio()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, NOME FROM NCL_MUNICIPIO"

        Sql.ToLower()

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                ServerUtils.getDBHelper.ExecuteNonQuery("UPDATE NCL_MUNICIPIO SET NOME = '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "NOME"), rbLower.Checked) & "' WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDeGrupo()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, NOME FROM NCL_GRUPO"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                ServerUtils.getDBHelper.ExecuteNonQuery("UPDATE NCL_GRUPO SET NOME = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "NOME"), rbLower.Checked) & "' WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDePessoa()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, NOME, ENDEMAIL, LOGRADOURO, COMPLEMENTO, BAIRRO, SITE FROM NCL_PESSOA"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_PESSOA SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOME") Then
                    Sql2.Append("NOME = '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "NOME"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("NOME = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "ENDEMAIL") Then
                    Sql2.Append("ENDEMAIL= '" & UtilidadesDeConversao.FormataTextoTudoMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "ENDEMAIL"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("ENDEMAIL= NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOGRADOURO") Then
                    Sql2.Append("LOGRADOURO= '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "LOGRADOURO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("LOGRADOURO= NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "COMPLEMENTO") Then
                    Sql2.Append("COMPLEMENTO= '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "LOGRADOURO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("COMPLEMENTO= NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "BAIRRO") Then
                    Sql2.Append("BAIRRO= '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "BAIRRO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("BAIRRO= NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "SITE") Then
                    Sql2.Append("SITE= '" & UtilidadesDeConversao.FormataTextoTudoMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "SITE"), rbLower.Checked) & "'")
                Else
                    Sql2.Append("SITE= NULL ")
                End If

                Sql2.Append("WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDePessoaFisica()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT IDPESSOA, NOMEMAE, NOMEPAI FROM NCL_PESSOAFISICA"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_PESSOAFISICA SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEMAE") Then
                    Sql2.Append("NOMEMAE = '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "NOMEMAE"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("NOMEMAE = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEPAI") Then
                    Sql2.Append("NOMEPAI = '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPAI"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("NOMEPAI = NULL ")
                End If

                Sql2.Append("WHERE IDPESSOA = " & UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDePessoaJuridica()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT IDPESSOA, NOMEFANTASIA FROM NCL_PESSOAJURIDICA"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_PESSOAJURIDICA SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "NOMEFANTASIA") Then
                    Sql2.Append("NOMEFANTASIA = '" & UtilidadesDeConversao.FormataTextoEmMaisculoMinusculoIntercaladoPorEspaco(UtilidadesDePersistencia.GetValorString(Leitor, "NOMEFANTASIA"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("NOMEFANTASIA = NULL ")
                End If

                Sql2.Append("WHERE IDPESSOA = " & UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDeCompromisso()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, ASSUNTO, LOCAL, DESCRICAO FROM NCL_COMPROMISSO"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_COMPROMISSO SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "ASSUNTO") Then
                    Sql2.Append("ASSUNTO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("ASSUNTO = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
                    Sql2.Append("LOCAL = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("LOCAL = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
                    Sql2.Append("DESCRICAO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("DESCRICAO = NULL ")
                End If

                Sql2.Append("WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDeTarefa()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, ASSUNTO, DESCRICAO FROM NCL_TAREFA"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_TAREFA SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "ASSUNTO") Then
                    Sql2.Append("ASSUNTO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("ASSUNTO = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
                    Sql2.Append("DESCRICAO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("DESCRICAO = NULL, ")
                End If

                Sql2.Append("WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDeLembrete()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, ASSUNTO, LOCAL, DESCRICAO FROM NCL_LEMBRETE"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE NCL_LEMBRETE SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "ASSUNTO") Then
                    Sql2.Append("ASSUNTO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("ASSUNTO = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
                    Sql2.Append("LOCAL = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("LOCAL = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCRICAO") Then
                    Sql2.Append("DESCRICAO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("DESCRICAO = NULL, ")
                End If

                Sql2.Append("WHERE ID = " & UtilidadesDePersistencia.GetValorLong(Leitor, "ID"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

End Class
