Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Public Class MapeadorDeSolicitacaoDeVisita
    Implements IMapeadorDeSolicitacaoDeVisita

    Private Function ObtenhaSQL() As String
        Dim Sql As New StringBuilder

        Sql.Append("SELECT PESSOA_CONTATO.ID AS ID_PESSOA_CONTATO, PESSOA_CONTATO.NOME AS NOME_PESSOA_CONTATO, PESSOA_CONTATO.TIPO AS TIPO_PESSOA_CONTATO,")
        Sql.Append(" DRY_CONTATO.IDPESSOA, DRY_CONTATO.TIPOPESSOA, DRY_CONTATO.CARGO, DRY_CONTATO.OBSERVACOES,")
        Sql.Append(" DRY_SOLICVISI.ID AS ID_SOLICITACAO, DRY_SOLICVISI.IDCONTATO, DRY_SOLICVISI.ASSUNTO, DRY_SOLICVISI.DESCRICAO, DRY_SOLICVISI.CODIGO, DRY_SOLICVISI.LOCAL,")
        Sql.Append(" DRY_SOLICVISI.DATADECADASTRO, DRY_SOLICVISI.ESTAATIVA, DRY_SOLICVISI.IDUSUARIOCAD, PESSOA_USUARIO.ID AS ID_USUARIO, PESSOA_USUARIO.NOME AS NOME_USUARIO")
        Sql.Append(" FROM NCL_PESSOA AS PESSOA_CONTATO, DRY_CONTATO, DRY_SOLICVISI, NCL_PESSOA AS PESSOA_USUARIO")
        Sql.Append(" WHERE DRY_CONTATO.IDPESSOA = PESSOA_CONTATO.ID")
        Sql.Append(" AND DRY_SOLICVISI.IDCONTATO  = DRY_CONTATO.IDPESSOA")
        Sql.Append(" AND DRY_SOLICVISI.IDUSUARIOCAD = PESSOA_USUARIO.ID")

        Return Sql.ToString
    End Function

    Private Function ObtenhaOrderBy() As String
        Return " ORDER BY DRY_SOLICVISI.DATADECADASTRO DESC"
    End Function

    Public Sub Inserir(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita) Implements IMapeadorDeSolicitacaoDeVisita.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        SolicitacaoDeVisita.ID = GeradorDeID.getInstancia.getProximoID()
        SolicitacaoDeVisita.Codigo = Me.ObtenhaProximoCodigoDisponivel

        Sql.Append("INSERT INTO DRY_SOLICVISI (")
        Sql.Append("ID, CODIGO, IDCONTATO, ASSUNTO, DESCRICAO, DATADECADASTRO, ESTAATIVA, IDUSUARIOCAD, LOCAL)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(SolicitacaoDeVisita.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(SolicitacaoDeVisita.Codigo, ", "))
        Sql.Append(String.Concat(SolicitacaoDeVisita.Contato.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeVisita.Assunto), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeVisita.Descricao), "', "))
        Sql.Append(String.Concat("'", SolicitacaoDeVisita.DataDaSolicitacao.ToString("yyyyMMddHHmmss"), "', "))
        Sql.Append(String.Concat("'", IIf(SolicitacaoDeVisita.Ativa, "S", "N"), "', "))
        Sql.Append(String.Concat(SolicitacaoDeVisita.UsuarioQueCadastrou.ID, ", "))

        If Not String.IsNullOrEmpty(SolicitacaoDeVisita.Local) Then
            Sql.Append(String.Concat("'", SolicitacaoDeVisita.Local, "')"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeVisita As ISolicitacaoDeVisita) Implements IMapeadorDeSolicitacaoDeVisita.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICVISI SET")
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeVisita.Assunto), "',"))
        Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeVisita.Descricao), "',"))

        If Not String.IsNullOrEmpty(SolicitacaoDeVisita.Local) Then
            Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeVisita.Local), "'"))
        Else
            Sql.Append(" LOCAL = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", SolicitacaoDeVisita.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaSolicitacaoDeVisita(ByVal ID As Long) As ISolicitacaoDeVisita Implements IMapeadorDeSolicitacaoDeVisita.ObtenhaSolicitacaoDeVisita
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim SolicitacaoDeVisita As ISolicitacaoDeVisita = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICVISI.ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                SolicitacaoDeVisita = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return SolicitacaoDeVisita
    End Function

    Private Function MontaObjeto(ByVal Leitor As IDataReader) As ISolicitacaoDeVisita
        Dim Contato As IContato = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing
        Dim Usuario As Usuario
        Dim SolicitacaoDeVisita As ISolicitacaoDeVisita = Nothing

        Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO_PESSOA_CONTATO"))

        If Tipo.Equals(TipoDePessoa.Fisica) Then
            Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
        Else
            Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
        End If

        Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID_PESSOA_CONTATO")
        Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_PESSOA_CONTATO")

        Contato = FabricaGenerica.GetInstancia.CrieObjeto(Of IContato)(New Object() {Pessoa})

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "CARGO") Then
            Contato.Cargo = UtilidadesDePersistencia.GetValorString(Leitor, "CARGO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACOES") Then
            Contato.Observacoes = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACOES")
        End If

        Usuario = New Usuario(UtilidadesDePersistencia.GetValorLong(Leitor, "ID_USUARIO"), _
                              UtilidadesDePersistencia.GetValorString(Leitor, "NOME_USUARIO"))

        SolicitacaoDeVisita = FabricaGenerica.GetInstancia.CrieObjeto(Of ISolicitacaoDeVisita)()
        SolicitacaoDeVisita.Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")
        SolicitacaoDeVisita.Ativa = UtilidadesDePersistencia.GetValorBooleano(Leitor, "ESTAATIVA")
        SolicitacaoDeVisita.Contato = Contato
        SolicitacaoDeVisita.DataDaSolicitacao = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATADECADASTRO").Value
        SolicitacaoDeVisita.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")
        SolicitacaoDeVisita.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID_SOLICITACAO")
        SolicitacaoDeVisita.Codigo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGO")
        SolicitacaoDeVisita.UsuarioQueCadastrou = Usuario

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
            SolicitacaoDeVisita.Local = UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL")
        End If

        Return SolicitacaoDeVisita
    End Function

    Public Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeVisita) Implements IMapeadorDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeVisita) = New List(Of ISolicitacaoDeVisita)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICVISI.ESTAATIVA = 'S'")
        End If

        Sql.Append(Me.ObtenhaOrderBy)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Solicitacoes.Add(Me.MontaObjeto(Leitor))
            End While
        End Using

        Return Solicitacoes
    End Function

    Public Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                   ByVal DataInicio As Date, _
                                                   ByVal DataFim As Date) As IList(Of ISolicitacaoDeVisita) Implements IMapeadorDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeVisita) = New List(Of ISolicitacaoDeVisita)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICVISI.ESTAATIVA = 'S'")
        End If

        Sql.Append(String.Concat(" AND DRY_SOLICVISI.DATADECADASTRO >= '", DataInicio.ToString("yyyyMMdd") & "000000", "'"))
        Sql.Append(String.Concat(" AND DRY_SOLICVISI.DATADECADASTRO <= '", DataFim.ToString("yyyyMMdd") & "235959", "'"))

        Sql.Append(Me.ObtenhaOrderBy)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Solicitacoes.Add(Me.MontaObjeto(Leitor))
            End While

        End Using

        Return Solicitacoes
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeVisita.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM DRY_SOLICVISI")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Finalizar(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeVisita.Finalizar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICVISI SET ESTAATIVA = 'N'")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function ObtenhaProximoCodigoDisponivel() As Long
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim CodigoMaximo As Long

        Sql.Append("SELECT MAX(CODIGO) AS CODIGOMAXIMO ")
        Sql.Append("FROM DRY_SOLICVISI ")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                CodigoMaximo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGOMAXIMO") + 1
            End If
        End Using

        Return CodigoMaximo
    End Function

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeVisita Implements IMapeadorDeSolicitacaoDeVisita.ObtenhaSolicitacaoPorCodigo
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim SolicitacaoDeVisita As ISolicitacaoDeVisita = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICVISI.CODIGO = ", Codigo.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                SolicitacaoDeVisita = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return SolicitacaoDeVisita
    End Function

    Public Function ObtenhaSolicitacoesDeVisita(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                ByVal IDContato As Long) As IList(Of ISolicitacaoDeVisita) Implements IMapeadorDeSolicitacaoDeVisita.ObtenhaSolicitacoesDeVisita
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeVisita) = New List(Of ISolicitacaoDeVisita)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICVISI.ESTAATIVA = 'S'")
        End If

        Sql.Append(String.Concat(" AND DRY_SOLICVISI.IDCONTATO = ", IDContato.ToString))
        Sql.Append(Me.ObtenhaOrderBy)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Solicitacoes.Add(Me.MontaObjeto(Leitor))
            End While

        End Using

        Return Solicitacoes
    End Function

End Class
