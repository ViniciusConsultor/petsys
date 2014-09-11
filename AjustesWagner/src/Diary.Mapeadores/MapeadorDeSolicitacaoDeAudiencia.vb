Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Public Class MapeadorDeSolicitacaoDeAudiencia
    Implements IMapeadorDeSolicitacaoDeAudiencia

    Private Function ObtenhaSQL() As String
        Dim Sql As New StringBuilder

        Sql.Append("SELECT PESSOA_CONTATO.ID AS ID_PESSOA_CONTATO, PESSOA_CONTATO.NOME AS NOME_PESSOA_CONTATO, PESSOA_CONTATO.TIPO AS TIPO_PESSOA_CONTATO,")
        Sql.Append(" DRY_CONTATO.IDPESSOA, DRY_CONTATO.TIPOPESSOA, DRY_CONTATO.CARGO, DRY_CONTATO.OBSERVACOES,")
        Sql.Append(" DRY_SOLICAUDI.ID AS ID_SOLICITACAO, DRY_SOLICAUDI.IDCONTATO, DRY_SOLICAUDI.ASSUNTO, DRY_SOLICAUDI.DESCRICAO, DRY_SOLICAUDI.CODIGO, DRY_SOLICAUDI.LOCAL,")
        Sql.Append(" DRY_SOLICAUDI.DATADECADASTRO, DRY_SOLICAUDI.ESTAATIVA, DRY_SOLICAUDI.IDUSUARIOCAD, PESSOA_USUARIO.ID AS ID_USUARIO, PESSOA_USUARIO.NOME AS NOME_USUARIO")
        Sql.Append(" FROM NCL_PESSOA AS PESSOA_CONTATO, DRY_CONTATO, DRY_SOLICAUDI, NCL_PESSOA AS PESSOA_USUARIO")
        Sql.Append(" WHERE DRY_CONTATO.IDPESSOA = PESSOA_CONTATO.ID")
        Sql.Append(" AND DRY_SOLICAUDI.IDCONTATO  = DRY_CONTATO.IDPESSOA")
        Sql.Append(" AND DRY_SOLICAUDI.IDUSUARIOCAD = PESSOA_USUARIO.ID")

        Return Sql.ToString
    End Function

    Private Function ObtenhaOrderBy() As String
        Return " ORDER BY DRY_SOLICAUDI.DATADECADASTRO DESC"
    End Function

    Public Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IMapeadorDeSolicitacaoDeAudiencia.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        SolicitacaoDeAudiencia.ID = GeradorDeID.getInstancia.getProximoID()
        SolicitacaoDeAudiencia.Codigo = Me.ObtenhaProximoCodigoDisponivel

        Sql.Append("INSERT INTO DRY_SOLICAUDI (")
        Sql.Append("ID, CODIGO, IDCONTATO, ASSUNTO, DESCRICAO, DATADECADASTRO, ESTAATIVA, IDUSUARIOCAD, LOCAL)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(SolicitacaoDeAudiencia.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(SolicitacaoDeAudiencia.Codigo, ", "))
        Sql.Append(String.Concat(SolicitacaoDeAudiencia.Contato.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeAudiencia.Assunto), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeAudiencia.Descricao), "', "))
        Sql.Append(String.Concat("'", SolicitacaoDeAudiencia.DataDaSolicitacao.ToString("yyyyMMddHHmmss"), "', "))
        Sql.Append(String.Concat("'", IIf(SolicitacaoDeAudiencia.Ativa, "S", "N"), "', "))
        Sql.Append(String.Concat(SolicitacaoDeAudiencia.UsuarioQueCadastrou.ID, ", "))

        If Not String.IsNullOrEmpty(SolicitacaoDeAudiencia.Local) Then
            Sql.Append(String.Concat("'", SolicitacaoDeAudiencia.Local, "')"))
        Else
            Sql.Append("NULL)")
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IMapeadorDeSolicitacaoDeAudiencia.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICAUDI SET")
        Sql.Append(String.Concat(" ASSUNTO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeAudiencia.Assunto), "',"))
        Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeAudiencia.Descricao), "',"))

        If Not String.IsNullOrEmpty(SolicitacaoDeAudiencia.Local) Then
            Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeAudiencia.Local), "'"))
        Else
            Sql.Append(" LOCAL = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", SolicitacaoDeAudiencia.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia Implements IMapeadorDeSolicitacaoDeAudiencia.ObtenhaSolicitacaoDeAudiencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICAUDI.ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                SolicitacaoDeAudiencia = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return SolicitacaoDeAudiencia
    End Function

    Private Function MontaObjeto(ByVal Leitor As IDataReader) As ISolicitacaoDeAudiencia
        Dim Contato As IContato = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing
        Dim Usuario As Usuario
        Dim SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia = Nothing

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

        SolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of ISolicitacaoDeAudiencia)()
        SolicitacaoDeAudiencia.Assunto = UtilidadesDePersistencia.GetValorString(Leitor, "ASSUNTO")
        SolicitacaoDeAudiencia.Ativa = UtilidadesDePersistencia.GetValorBooleano(Leitor, "ESTAATIVA")
        SolicitacaoDeAudiencia.Contato = Contato
        SolicitacaoDeAudiencia.DataDaSolicitacao = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATADECADASTRO").Value
        SolicitacaoDeAudiencia.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")
        SolicitacaoDeAudiencia.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID_SOLICITACAO")
        SolicitacaoDeAudiencia.Codigo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGO")
        SolicitacaoDeAudiencia.UsuarioQueCadastrou = Usuario

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "LOCAL") Then
            SolicitacaoDeAudiencia.Local = UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL")
        End If

        Return SolicitacaoDeAudiencia
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean) As IList(Of ISolicitacaoDeAudiencia) Implements IMapeadorDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia) = New List(Of ISolicitacaoDeAudiencia)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICAUDI.ESTAATIVA = 'S'")
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

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                   ByVal DataInicio As Date, _
                                                   ByVal DataFim As Date) As IList(Of ISolicitacaoDeAudiencia) Implements IMapeadorDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia) = New List(Of ISolicitacaoDeAudiencia)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICAUDI.ESTAATIVA = 'S'")
        End If

        Sql.Append(String.Concat(" AND DRY_SOLICAUDI.DATADECADASTRO >= '", DataInicio.ToString("yyyyMMdd") & "000000", "'"))
        Sql.Append(String.Concat(" AND DRY_SOLICAUDI.DATADECADASTRO <= '", DataFim.ToString("yyyyMMdd") & "235959", "'"))

        Sql.Append(Me.ObtenhaOrderBy)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Solicitacoes.Add(Me.MontaObjeto(Leitor))
            End While

        End Using

        Return Solicitacoes
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeAudiencia.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM DRY_SOLICAUDI")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Finalizar(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeAudiencia.Finalizar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICAUDI SET ESTAATIVA = 'N'")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Private Function ObtenhaProximoCodigoDisponivel() As Long
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim CodigoMaximo As Long

        Sql.Append("SELECT MAX(CODIGO) AS CODIGOMAXIMO ")
        Sql.Append("FROM DRY_SOLICAUDI ")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                CodigoMaximo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGOMAXIMO") + 1
            End If
        End Using

        Return CodigoMaximo
    End Function

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeAudiencia Implements IMapeadorDeSolicitacaoDeAudiencia.ObtenhaSolicitacaoPorCodigo
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICAUDI.CODIGO = ", Codigo.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                SolicitacaoDeAudiencia = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return SolicitacaoDeAudiencia
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal ConsiderarSolicitacoesFinalizadas As Boolean, _
                                                   ByVal IDContato As Long) As IList(Of ISolicitacaoDeAudiencia) Implements IMapeadorDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia) = New List(Of ISolicitacaoDeAudiencia)

        Sql.Append(Me.ObtenhaSQL)

        If Not ConsiderarSolicitacoesFinalizadas Then
            Sql.Append(" AND DRY_SOLICAUDI.ESTAATIVA = 'S'")
        End If

        Sql.Append(String.Concat(" AND DRY_SOLICAUDI.IDCONTATO = ", IDContato.ToString))
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