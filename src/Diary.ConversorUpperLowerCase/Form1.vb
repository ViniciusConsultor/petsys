Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.DBHelper
Imports System.Text
Imports Compartilhados.Conversores

Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Conexao As IConexao

        Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            Conexao = Servico.ObtenhaConexao
        End Using

        Me.Cursor = Cursors.Default
        FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Conexao
    End Sub

    Private Sub btnConverter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConverter.Click
        ServerUtils.setCredencial(Util.ConstruaCredencial)

        Try
            MigraTabelaDeContato()
            MigraTabelaDeSolicitacaoDeAudiencia()
            MigraTabelaDeSolicitacaoDeConvite()

            MsgBox("Migração realizada com sucesso.", MsgBoxStyle.Information, "Sucesso.")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub MigraTabelaDeContato()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT IDPESSOA, CARGO, OBSERVACOES FROM DRY_CONTATO"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE DRY_CONTATO SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "CARGO") Then
                    Sql2.Append("CARGO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "CARGO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("CARGO = NULL, ")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then
                    Sql2.Append("OBSERVACOES = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES"), rbLower.Checked) & "' ")
                Else
                    Sql2.Append("OBSERVACOES = NULL ")
                End If

                Sql2.Append("WHERE IDPESSOA = " & UtilidadesDePersistencia.GetValorLong(Leitor, "IDPESSOA"))

                ServerUtils.getDBHelper.ExecuteNonQuery(Sql2.ToString)
            End While
        End Using

    End Sub

    Private Sub MigraTabelaDeSolicitacaoDeAudiencia()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, ASSUNTO, LOCAL, DESCRICAO FROM DRY_SOLICAUDI"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE DRY_SOLICAUDI SET ")

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

    Private Sub MigraTabelaDeSolicitacaoDeConvite()
        Dim Sql As String
        Dim DbHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql = "SELECT ID, LOCAL, DESCRICAO, OBSERVACAO FROM DRY_SOLICCONVT"

        Using Leitor As IDataReader = DbHelper.obtenhaReader(Sql)
            While Leitor.Read
                Dim Sql2 As New StringBuilder

                Sql2.Append("UPDATE DRY_SOLICCONVT SET ")

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACAO") Then
                    Sql2.Append("OBSERVACAO = '" & UtilidadesDeConversao.FormataTextoPrimeiraLetraEmMaiusculoRestanteMinusculo(UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACAO"), rbLower.Checked) & "', ")
                Else
                    Sql2.Append("OBSERVACAO = NULL, ")
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
