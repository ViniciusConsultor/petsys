Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Public Class MapeadorDeSolicitacaoDeConvite
    Implements IMapeadorDeSolicitacaoDeConvite

    Private Function ObtenhaSQL() As String
        Dim Sql As New StringBuilder

        Sql.Append("SELECT PESSOA_CONTATO.ID AS ID_PESSOA_CONTATO, PESSOA_CONTATO.NOME AS NOME_PESSOA_CONTATO, PESSOA_CONTATO.TIPO AS TIPO_PESSOA_CONTATO,")
        Sql.Append(" DRY_CONTATO.IDPESSOA, DRY_CONTATO.TIPOPESSOA, DRY_CONTATO.CARGO, DRY_CONTATO.OBSERVACOES,")
        Sql.Append(" DRY_SOLICCONVT.ID AS ID_SOLICITACAO, DRY_SOLICCONVT.IDCONTATO, DRY_SOLICCONVT.LOCAL, DRY_SOLICCONVT.OBSERVACAO, DRY_SOLICCONVT.DESCRICAO, DRY_SOLICCONVT.CODIGO,")
        Sql.Append(" DRY_SOLICCONVT.DATAEHORA, DRY_SOLICCONVT.DATADECADASTRO, DRY_SOLICCONVT.ESTAATIVA, DRY_SOLICCONVT.IDUSUARIOCAD, PESSOA_USUARIO.ID AS ID_USUARIO, PESSOA_USUARIO.NOME AS NOME_USUARIO")
        Sql.Append(" FROM NCL_PESSOA AS PESSOA_CONTATO, DRY_CONTATO, DRY_SOLICCONVT, NCL_PESSOA AS PESSOA_USUARIO")
        Sql.Append(" WHERE DRY_CONTATO.IDPESSOA = PESSOA_CONTATO.ID")
        Sql.Append(" AND DRY_SOLICCONVT.IDCONTATO  = DRY_CONTATO.IDPESSOA")
        Sql.Append(" AND DRY_SOLICCONVT.IDUSUARIOCAD = PESSOA_USUARIO.ID")

        Return Sql.ToString
    End Function

    Private Function ObtenhaOrderBy() As String
        Return " ORDER BY DRY_SOLICCONVT.DATADECADASTRO DESC"
    End Function

    Private Function MontaObjeto(ByVal Leitor As IDataReader) As ISolicitacaoDeConvite
        Dim Contato As IContato = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing
        Dim Usuario As Usuario
        Dim Solicitacao As ISolicitacaoDeConvite = Nothing

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

        Solicitacao = FabricaGenerica.GetInstancia.CrieObjeto(Of ISolicitacaoDeConvite)()
        Solicitacao.DataEHorario = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATAEHORA").Value
        Solicitacao.Local = UtilidadesDePersistencia.GetValorString(Leitor, "LOCAL")
        Solicitacao.Descricao = UtilidadesDePersistencia.GetValorString(Leitor, "DESCRICAO")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "OBSERVACAO") Then
            Solicitacao.Observacao = UtilidadesDePersistencia.GetValorString(Leitor, "OBSERVACAO")
        End If

        Solicitacao.Ativa = UtilidadesDePersistencia.GetValorBooleano(Leitor, "ESTAATIVA")
        Solicitacao.Contato = Contato
        Solicitacao.DataDaSolicitacao = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATADECADASTRO").Value
        Solicitacao.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID_SOLICITACAO")
        Solicitacao.Codigo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGO")
        Solicitacao.UsuarioQueCadastrou = Usuario

        Return Solicitacao
    End Function

    Private Function ObtenhaProximoCodigoDisponivel() As Long
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim CodigoMaximo As Long

        Sql.Append("SELECT MAX(CODIGO) AS CODIGOMAXIMO ")
        Sql.Append("FROM DRY_SOLICCONVT ")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                CodigoMaximo = UtilidadesDePersistencia.GetValorLong(Leitor, "CODIGOMAXIMO") + 1
            End If
        End Using

        Return CodigoMaximo
    End Function

    Public Sub Finalizar(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeConvite.Finalizar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICCONVT SET ESTAATIVA = 'N'")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite) Implements IMapeadorDeSolicitacaoDeConvite.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        SolicitacaoDeConvite.ID = GeradorDeID.getInstancia.getProximoID()
        SolicitacaoDeConvite.Codigo = Me.ObtenhaProximoCodigoDisponivel

        Sql.Append("INSERT INTO DRY_SOLICCONVT (")
        Sql.Append("ID, CODIGO, IDCONTATO, DATAEHORA, LOCAL, DESCRICAO, OBSERVACAO, DATADECADASTRO, ESTAATIVA, IDUSUARIOCAD)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(SolicitacaoDeConvite.ID.Value.ToString, ", "))
        Sql.Append(String.Concat(SolicitacaoDeConvite.Codigo, ", "))
        Sql.Append(String.Concat(SolicitacaoDeConvite.Contato.Pessoa.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", SolicitacaoDeConvite.DataEHorario.ToString("yyyyMMddHHmmss"), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Local), "', "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Descricao), "', "))

        If Not String.IsNullOrEmpty(SolicitacaoDeConvite.Observacao) Then
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Observacao), "', "))
        Else
            Sql.Append("NULL, ")
        End If

        Sql.Append(String.Concat("'", SolicitacaoDeConvite.DataDaSolicitacao.ToString("yyyyMMddHHmmss"), "', "))
        Sql.Append(String.Concat("'", IIf(SolicitacaoDeConvite.Ativa, "S", "N"), "', "))
        Sql.Append(String.Concat(SolicitacaoDeConvite.UsuarioQueCadastrou.ID, ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeConvite As ISolicitacaoDeConvite) Implements IMapeadorDeSolicitacaoDeConvite.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE DRY_SOLICCONVT SET =")
        Sql.Append(String.Concat(" DATAEHORA = '", SolicitacaoDeConvite.DataEHorario.ToString("yyyyMMddHHmmss"), "',"))
        Sql.Append(String.Concat(" LOCAL = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Local), "',"))
        Sql.Append(String.Concat(" DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Descricao), "'"))

        If Not String.IsNullOrEmpty(SolicitacaoDeConvite.Observacao) Then
            Sql.Append(String.Concat(" OBSERVACAO = '", UtilidadesDePersistencia.FiltraApostrofe(SolicitacaoDeConvite.Observacao), "'"))
        Else
            Sql.Append(" OBSERVACAO = NULL")
        End If

        Sql.Append(String.Concat(" WHERE ID = ", SolicitacaoDeConvite.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaSolicitacaoDeConvite(ByVal ID As Long) As ISolicitacaoDeConvite Implements IMapeadorDeSolicitacaoDeConvite.ObtenhaSolicitacaoDeConvite
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacao As ISolicitacaoDeConvite = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICCONVT.ID = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Solicitacao = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return Solicitacao
    End Function

    Public Function ObtenhaSolicitacaoPorCodigo(ByVal Codigo As Long) As ISolicitacaoDeConvite Implements IMapeadorDeSolicitacaoDeConvite.ObtenhaSolicitacaoPorCodigo
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacao As ISolicitacaoDeConvite = Nothing

        Sql.Append(Me.ObtenhaSQL)
        Sql.Append(String.Concat(" AND DRY_SOLICCONVT.CODIGO = ", Codigo.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            If Leitor.Read Then
                Solicitacao = Me.MontaObjeto(Leitor)
            End If
        End Using

        Return Solicitacao
    End Function

    Public Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeConvite) Implements IMapeadorDeSolicitacaoDeConvite.ObtenhaSolicitacoesDeConvite
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite) = New List(Of ISolicitacaoDeConvite)

        Sql.Append(Me.ObtenhaSQL)

        If TrazApenasAtivas Then
            Sql.Append(" AND DRY_SOLICCONVT.ESTAATIVA = 'S'")
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

    Public Function ObtenhaSolicitacoesDeConvite(ByVal TrazApenasAtivas As Boolean, _
                                                 ByVal DataInicio As Date, _
                                                 ByVal DataFim As Date) As IList(Of ISolicitacaoDeConvite) Implements IMapeadorDeSolicitacaoDeConvite.ObtenhaSolicitacoesDeConvite
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Solicitacoes As IList(Of ISolicitacaoDeConvite) = New List(Of ISolicitacaoDeConvite)

        Sql.Append(Me.ObtenhaSQL)

        If TrazApenasAtivas Then
            Sql.Append(" AND DRY_SOLICCONVT.ESTAATIVA = 'S'")
        End If

        Sql.Append(String.Concat(" AND DRY_SOLICCONVT.DATAEHORA >= '", DataInicio.ToString("yyyyMMddHHmmss"), "'"))
        Sql.Append(String.Concat(" AND DRY_SOLICCONVT.DATAEHORA <= '", DataFim.ToString("yyyyMMddHHmmss"), "'"))

        Sql.Append(Me.ObtenhaOrderBy)

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Solicitacoes.Add(Me.MontaObjeto(Leitor))
            End While

        End Using

        Return Solicitacoes
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeSolicitacaoDeConvite.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM DRY_SOLICCONVT")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

End Class