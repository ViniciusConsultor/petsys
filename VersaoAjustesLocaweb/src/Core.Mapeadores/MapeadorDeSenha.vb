Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces

Public Class MapeadorDeSenha
    Implements IMapeadorDeSenha
    
    Public Function ObtenhaSenhaDoOperador(ByVal IDOperador As Long) As ISenha Implements IMapeadorDeSenha.ObtenhaSenhaDoOperador
        Dim Sql As New StringBuilder
        Dim Senha As ISenha = Nothing
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql.Append("SELECT SENHA, DATACADASTRO")
        Sql.Append(" FROM NCL_SNHOP")
        Sql.Append(String.Concat(" WHERE IDOPERADOR = ", IDOperador.ToString))

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Senha = FabricaGenerica.GetInstancia.CrieObjeto(Of ISenha)(New Object() {UtilidadesDePersistencia.GetValorString(Leitor, "SENHA"), _
                                                                                             UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATACADASTRO")})

                End If
            Finally
                Leitor.Close()
            End Try

        End Using

        Return Senha
    End Function

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal Senha As ISenha) Implements IMapeadorDeSenha.Altere
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_SNHOP SET ")
        Sql.Append(String.Concat("SENHA = '", Senha.ToString, "', "))
        Sql.Append(String.Concat("DATACADASTRO = '", Senha.DataDeCadastro.ToString("yyyyMMddhhmmss"), "'"))
        Sql.Append(" WHERE ")
        Sql.Append(String.Concat("IDOPERADOR = ", IDOperador.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString, False)
    End Sub

    Public Sub Insira(ByVal IDOperador As Long, _
                      ByVal Senha As ISenha) Implements IMapeadorDeSenha.Insira
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_SNHOP (IDOPERADOR, SENHA, DATACADASTRO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(IDOperador.ToString, ", "))
        Sql.Append(String.Concat("'", Senha.ToString, "', "))
        Sql.Append(String.Concat("'", Senha.DataDeCadastro.ToString("yyyyMMddhhmmss"), "'"))
        Sql.Append(")")

        DBHelper.ExecuteNonQuery(Sql.ToString, False)
    End Sub

    Public Sub Remova(ByVal IDOperador As Long) Implements IMapeadorDeSenha.Remova
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery("DELETE FROM NCL_SNHOP WHERE IDOPERADOR = " & IDOperador.ToString)
    End Sub

    Public Function RegistreDefinicaoDeNovaSenha(ByVal Operador As IOperador) As Long Implements IMapeadorDeSenha.RegistreDefinicaoDeNovaSenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Dim idRequisicaoReDefinicao = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_REDEFSNH (ID, IDOPERADOR, DTSOLICITACAO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(idRequisicaoReDefinicao.ToString, ", "))
        Sql.Append(String.Concat(Operador.Pessoa.ID.ToString(), ", "))
        Sql.Append(String.Concat("'", Now.ToString("yyyyMMddhhmmss"), "'"))
        Sql.Append(")")

        DBHelper.ExecuteNonQuery(Sql.ToString, False)

        Return idRequisicaoReDefinicao
    End Function

    Public Function ObtenhaIDOperadorParaRedifinirSenha(IDRedefinicaoDeSenha As Long) As Long? Implements IMapeadorDeSenha.ObtenhaIDOperadorParaRedifinirSenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper

        Sql.Append("SELECT IDOPERADOR, ID")
        Sql.Append(" FROM NCL_REDEFSNH")
        Sql.Append(String.Concat(" WHERE ID= ", IDRedefinicaoDeSenha.ToString))

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Return UtilidadesDePersistencia.GetValorLong(Leitor, "IDOPERADOR")
                End If
            Finally
                Leitor.Close()
            End Try

        End Using

        Return Nothing
    End Function

    Public Sub ExcluaRefinicaoDeSenha(IDRedefinicaoDeSenha As Long) Implements IMapeadorDeSenha.ExcluaRefinicaoDeSenha
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper
        DBHelper.ExecuteNonQuery("DELETE FROM NCL_REDEFSNH WHERE ID = " & IDRedefinicaoDeSenha.ToString)
    End Sub

End Class