Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Interfaces

Public Class MapeadorDeBancosEAgencias
    Implements IMapeadorDeBancosEAgencias

    Public Sub InsiraAgencia(ByVal Agencia As IAgencia) Implements IMapeadorDeBancosEAgencias.InsiraAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Agencia.ID = GeradorDeID.getInstancia().getProximoID()

        Sql.Append("INSERT INTO NCL_AGENCIABANCO (")
        Sql.Append("ID, IDBANCO, NOME, NUMERO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Agencia.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", Agencia.Banco.ID, "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Nome), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Numero), "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub ModifiqueAgencia(ByVal Agencia As IAgencia) Implements IMapeadorDeBancosEAgencias.ModifiqueAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_AGENCIABANCO SET ")
        Sql.Append(String.Concat(" NUMERO = '", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Numero), "',"))
        Sql.Append(String.Concat(" NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Nome), "',"))
        Sql.Append(String.Concat(" IDBANCO = '", Agencia.Banco.ID, "'"))
        Sql.Append(String.Concat(" WHERE ID = ", Agencia.ID.Value.ToString()))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAgencias(ByVal IDBanco As String) As IList(Of IAgencia) Implements IMapeadorDeBancosEAgencias.ObtenhaAgencias
        Return Nothing
    End Function

  Public Sub RemovaAgencia(ByVal ID As Long) Implements IMapeadorDeBancosEAgencias.RemovaAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_AGENCIABANCO ")
        Sql.Append(" WHERE ID = ")
        Sql.Append(ID.ToString)
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAgencia(ByVal IDBanco As String, ByVal IDAgencia As Long) As IAgencia Implements IMapeadorDeBancosEAgencias.ObtenhaAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agencia As IAgencia = Nothing

        Sql.Append("SELECT ID, NUMERO, IDBANCO, NOME FROM NCL_AGENCIABANCO WHERE")
        Sql.Append(String.Concat(" IDBANCO = '", IDBanco.ToString, "'"))
        Sql.Append(String.Concat(" AND ID = ", IDAgencia.ToString))
        
        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Agencia = MontaObjetoAgencia(Leitor)
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Agencia
    End Function

    Private Function MontaObjetoAgencia(ByVal Leitor As IDataReader) As IAgencia
        Dim Banco As Banco
        Dim Agencia As IAgencia
        
        Banco = Banco.Obtenha(UtilidadesDePersistencia.GetValorString(Leitor, "IDBANCO"))
        
        Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)()
        Agencia.Numero = UtilidadesDePersistencia.GetValorString(Leitor, "NUMERO")
        Agencia.Banco = Banco
        Agencia.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
        Agencia.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")

        Return Agencia
    End Function

    Public Function ObtenhaAgenciasPorNomeComoFiltro(ByVal Banco As Banco, _
                                                     ByVal NomeDaAgencia As String, _
                                                     ByVal Quantidade As Integer) As IList(Of IAgencia) Implements IMapeadorDeBancosEAgencias.ObtenhaAgenciasPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agencia As IAgencia = Nothing
        Dim Agencias As IList(Of IAgencia) = New List(Of IAgencia)

        Sql.Append("SELECT ID, NUMERO, IDBANCO, NOME FROM NCL_AGENCIABANCO WHERE")
        Sql.Append(String.Concat(" IDBANCO = '", Banco.ID, "'"))

        If Not String.IsNullOrEmpty(NomeDaAgencia) Then
            Sql.Append(String.Concat("AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(NomeDaAgencia), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, Quantidade)
            Try
                While Leitor.Read
                    Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)()
                    Agencia.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                    Agencia.Numero = UtilidadesDePersistencia.GetValorString(Leitor, "NUMERO")
                    Agencia.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                    Agencia.Banco = Banco
                    Agencias.Add(Agencia)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Agencias

    End Function

End Class