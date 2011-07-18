Imports Compartilhados
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Public Class MapeadorDeBancosEAgencias
    Implements IMapeadorDeBancosEAgencias

    Public Sub InsiraAgencia(ByVal Agencia As IAgencia) Implements IMapeadorDeBancosEAgencias.InsiraAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_AGENCIABANCO (")
        Sql.Append("IDPESSOA, IDBANCO, NUMERO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Agencia.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(Agencia.Banco.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Numero), "')"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub InsiraBanco(ByVal Banco As IBanco) Implements IMapeadorDeBancosEAgencias.InsiraBanco
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_BANCO (")
        Sql.Append("IDPESSOA, NUMERO)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Banco.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Banco.Numero.ToString, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub ModifiqueAgencia(ByVal Agencia As IAgencia) Implements IMapeadorDeBancosEAgencias.ModifiqueAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_AGENCIABANCO SET ")
        Sql.Append(String.Concat(" NUMERO = '", UtilidadesDePersistencia.FiltraApostrofe(Agencia.Numero), "'"))
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Agencia.Pessoa.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub ModifiqueBanco(ByVal Banco As IBanco) Implements IMapeadorDeBancosEAgencias.ModifiqueBanco
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_BANCO SET ")
        Sql.Append(String.Concat(" NUMERO = ", Banco.Numero.ToString))
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Banco.Pessoa.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAgencias(ByVal IDBanco As Long) As IList(Of IAgencia) Implements IMapeadorDeBancosEAgencias.ObtenhaAgencias
        Return Nothing
    End Function

    Public Function ObtenhaBanco(ByVal Pessoa As IPessoa) As IBanco Implements IMapeadorDeBancosEAgencias.ObtenhaBanco
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Banco As IBanco = Nothing

        Sql.Append("SELECT IDPESSOA, NUMERO FROM NCL_BANCO WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Banco = MontaObjetoBanco(Leitor, Pessoa)
            End If
        End Using

        Return Banco
    End Function

    Private Function MontaObjetoBanco(ByVal Leitor As IDataReader, ByVal Pessoa As IPessoa) As IBanco
        Dim Banco As IBanco = Nothing

        Banco = FabricaGenerica.GetInstancia.CrieObjeto(Of IBanco)(New Object() {Pessoa})

        Banco.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMERO")

        Return Banco
    End Function

    Public Sub RemovaAgencia(ByVal ID As Long) Implements IMapeadorDeBancosEAgencias.RemovaAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_AGENCIABANCO ")
        Sql.Append(" WHERE IDPESSOA = ")
        Sql.Append(ID.ToString)
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub RemovaBanco(ByVal ID As Long) Implements IMapeadorDeBancosEAgencias.RemovaBanco
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_BANCO ")
        Sql.Append(" WHERE IDPESSOA = ")
        Sql.Append(ID.ToString)
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaAgencia(ByVal IDBanco As Long, ByVal IDAgencia As Long) As IAgencia Implements IMapeadorDeBancosEAgencias.ObtenhaAgencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agencia As IAgencia = Nothing

        Sql.Append("SELECT NCL_BANCO.IDPESSOA AS IDBANCO, NCL_BANCO.NUMERO AS NUMEROBANCO, NCL_AGENCIABANCO.IDPESSOA AS IDAGENCIA, NCL_AGENCIABANCO.NUMERO AS NUMEROAGENCIA FROM NCL_BANCO, NCL_AGENCIABANCO WHERE ")
        Sql.Append(String.Concat("NCL_BANCO.IDPESSOA = ", IDBanco.ToString))
        Sql.Append(String.Concat(" AND NCL_AGENCIABANCO.IDPESSOA = ", IDAgencia.ToString))
        Sql.Append(" AND NCL_AGENCIABANCO.IDBANCO = NCL_BANCO.IDPESSOA")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Agencia = MontaObjetoAgencia(Leitor)
            End If
        End Using

        Return Agencia
    End Function

    Private Function MontaObjetoAgencia(ByVal Leitor As IDataReader) As IAgencia
        Dim PessoaBanco As IPessoa
        Dim Banco As IBanco
        Dim Agencia As IAgencia
        Dim PessoaAgencia As IPessoa

        PessoaBanco = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDBANCO"))
        Banco = FabricaGenerica.GetInstancia.CrieObjeto(Of IBanco)(New Object() {PessoaBanco})
        Banco.Numero = UtilidadesDePersistencia.getValorInteger(Leitor, "NUMEROBANCO")

        PessoaAgencia = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaJuridicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDAGENCIA"))
        Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)(New Object() {PessoaAgencia})
        Agencia.Numero = UtilidadesDePersistencia.GetValorString(Leitor, "NUMEROAGENCIA")
        Agencia.Banco = Banco

        Return Agencia
    End Function

    Public Function ObtenhaBancosPorNomeComoFiltro(ByVal Nome As String, ByVal Quantidade As Integer) As IList(Of IBanco) Implements IMapeadorDeBancosEAgencias.ObtenhaBancosPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Banco As IBanco = Nothing
        Dim Pessoa As IPessoa = Nothing
        Dim Bancos As IList(Of IBanco) = New List(Of IBanco)

        Sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.NOME,")
        Sql.Append("NCL_BANCO.IDPESSOA, NCL_BANCO.NUMERO ")
        Sql.Append("FROM NCL_PESSOA, NCL_BANCO ")
        Sql.Append("WHERE NCL_BANCO.IDPESSOA = NCL_PESSOA.ID ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, Quantidade)
            While Leitor.Read

                Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

                Banco = MontaObjetoBanco(Leitor, Pessoa)
                Bancos.Add(Banco)
            End While
        End Using

        Return Bancos
    End Function

    Public Function ObtenhaAgenciasPorNomeComoFiltro(ByVal Banco As IBanco, _
                                                     ByVal NomeDaAgencia As String, _
                                                     ByVal Quantidade As Integer) As IList(Of IAgencia) Implements IMapeadorDeBancosEAgencias.ObtenhaAgenciasPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Agencia As IAgencia = Nothing
        Dim PessoaAgencia As IPessoa = Nothing
        Dim Agencias As IList(Of IAgencia) = New List(Of IAgencia)

        Sql.Append("SELECT ")
        Sql.Append("P1.ID AS IDPESSOABANCO, ")
        Sql.Append("P1.NOME AS NOMEBANCO, ")
        Sql.Append("NCL_BANCO.IDPESSOA AS IDBANCO, ")
        Sql.Append("NCL_BANCO.NUMERO AS NUMEROBANCO, ")
        Sql.Append("P2.ID AS IDPESSOAAGENCIA, ")
        Sql.Append("P2.NOME AS NOMEAGENCIA, ")
        Sql.Append("NCL_AGENCIABANCO.IDPESSOA AS IDAGENCIA, ")
        Sql.Append("NCL_AGENCIABANCO.NUMERO AS NUMEROAGENCIA, ")
        Sql.Append("NCL_AGENCIABANCO.IDBANCO AS IDBANCOAGENCIA ")
        Sql.Append("FROM ")
        Sql.Append("NCL_PESSOA P1, NCL_BANCO, NCL_PESSOA P2, NCL_AGENCIABANCO ")
        Sql.Append("WHERE ")
        Sql.Append("NCL_BANCO.IDPESSOA = P1.ID AND ")
        Sql.Append("NCL_AGENCIABANCO.IDPESSOA = P2.ID AND ")
        Sql.Append("NCL_AGENCIABANCO.IDBANCO = NCL_BANCO.IDPESSOA AND ")
        Sql.Append(String.Concat("NCL_AGENCIABANCO.IDBANCO = ", Banco.Pessoa.ID.Value.ToString))

        If Not String.IsNullOrEmpty(NomeDaAgencia) Then
            Sql.Append(String.Concat("AND P2.NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(NomeDaAgencia), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, Quantidade)
            While Leitor.Read
                PessoaAgencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                PessoaAgencia.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDAGENCIA")
                PessoaAgencia.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEAGENCIA")

                Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)(New Object() {PessoaAgencia})
                Agencia.Numero = UtilidadesDePersistencia.GetValorString(Leitor, "NUMEROAGENCIA")
                Agencia.Banco = Banco
                Agencias.Add(Agencia)
            End While
        End Using

        Return Agencias

    End Function

End Class