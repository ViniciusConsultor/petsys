Imports Diary.Interfaces.Mapeadores
Imports Diary.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio.LazyLoad

Public Class MapeadorDeDespacho
    Implements IMapeadorDeDespacho

    Public Sub Inserir(ByVal Despacho As IDespacho) Implements IMapeadorDeDespacho.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        Despacho.ID = GeradorDeID.getInstancia.getProximoID()

        Sql.Append("INSERT INTO DRY_DESPACHO (")
        Sql.Append("ID, TIPO, IDSOLICITANTE, IDALVO, IDSOLICITACAO, TIPOSOLICITACAO, DATADODESPACHO, TIPODESTDESPACHO, IDCOMPROMISSO, IDTAREFA, IDLEMBRETE)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Despacho.ID.ToString, ", "))
        Sql.Append(String.Concat(Despacho.Tipo.ID, ", "))
        Sql.Append(String.Concat(Despacho.Solicitante.ID, ", "))
        Sql.Append(String.Concat(Despacho.Alvo.ID, ", "))
        Sql.Append(String.Concat(Despacho.Solicitacao.ID, ", "))
        Sql.Append(String.Concat(Despacho.Solicitacao.Tipo.ID, ", "))
        Sql.Append(String.Concat(Despacho.DataDoDespacho.ToString("yyyyMMddhhmmss"), ", "))
        Sql.Append(String.Concat(Despacho.TipoDestino.ID, ", "))

        If Despacho.TipoDestino.Equals(TipoDestinoDespacho.Compromisso) Then
            Sql.Append(String.Concat(CType(Despacho, IDespachoAgenda).Compromisso.ID.Value, ", "))
            Sql.Append("NULL, ")
            Sql.Append("NULL)")
        ElseIf Despacho.TipoDestino.Equals(TipoDestinoDespacho.Tarefa) Then
            Sql.Append("NULL, ")
            Sql.Append(String.Concat(CType(Despacho, IDespachoTarefa).Tarefa.ID.Value, ", "))
            Sql.Append("NULL)")
        ElseIf Despacho.TipoDestino.Equals(TipoDestinoDespacho.Lembrete) Then
            Sql.Append("NULL, ")
            Sql.Append("NULL, ")
            Sql.Append(String.Concat(CType(Despacho, IDespachoLembrete).Lembrete.ID.Value, ")"))
        End If

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long) As IList(Of IDespacho) Implements IMapeadorDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Despachos As IList(Of IDespacho) = New List(Of IDespacho)

        Sql.Append("SELECT ID, TIPO, IDSOLICITANTE, IDALVO, IDSOLICITACAO, TIPOSOLICITACAO, DATADODESPACHO, TIPODESTDESPACHO, IDCOMPROMISSO, IDTAREFA, IDLEMBRETE")
        Sql.Append(" FROM DRY_DESPACHO ")
        Sql.Append(" WHERE IDSOLICITACAO = " & IDSolicitacao)
        Sql.Append(" ORDER BY DATADODESPACHO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Despachos.Add(Me.ObtenhaObjetoDespacho(Leitor))
            End While
        End Using

        Return Despachos
    End Function

    Private Function ObtenhaObjetoDespacho(ByVal Leitor As IDataReader) As IDespacho
        Dim Despacho As IDespacho
        Dim TipoDestino As TipoDestinoDespacho
        Dim Solicitacao As ISolicitacao = Nothing
        Dim TipoDaSolicitacao As TipoDeSolicitacao

        TipoDestino = TipoDestinoDespacho.Obtenha(UtilidadesDePersistencia.getValorByte(Leitor, "TIPODESTDESPACHO"))

        If TipoDestino.Equals(TipoDestinoDespacho.Compromisso) Then
            Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoAgenda)()
            CType(Despacho, IDespachoAgenda).Compromisso = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ICompromissoLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDCOMPROMISSO"))
        ElseIf TipoDestino.Equals(TipoDestinoDespacho.Tarefa) Then
            Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoTarefa)()
            CType(Despacho, IDespachoTarefa).Tarefa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ITarefaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDTAREFA"))
        Else
            TipoDestino.Equals(TipoDestinoDespacho.Lembrete)
            Despacho = FabricaGenerica.GetInstancia.CrieObjeto(Of IDespachoLembrete)()
            CType(Despacho, IDespachoLembrete).Lembrete = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ILembreteLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDLEMBRETE"))
        End If

        Despacho.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
        Despacho.Tipo = TipoDeDespacho.Obtenha(UtilidadesDePersistencia.getValorByte(Leitor, "TIPO"))
        Despacho.Solicitante = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDSOLICITANTE"))
        Despacho.Alvo = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of IPessoaFisicaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDALVO"))
        Despacho.DataDoDespacho = UtilidadesDePersistencia.getValorDateHourSec(Leitor, "DATADODESPACHO").Value

        TipoDaSolicitacao = TipoDeSolicitacao.Obtenha(UtilidadesDePersistencia.getValorByte(Leitor, "TIPOSOLICITACAO"))

        If TipoDaSolicitacao.Equals(TipoDeSolicitacao.Audiencia) Then
            Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeAudienciaLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDSOLICITACAO"))
        Else
            Solicitacao = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad(Of ISolicitacaoDeConviteLazyLoad)(UtilidadesDePersistencia.GetValorLong(Leitor, "IDSOLICITACAO"))
        End If

        Despacho.Solicitacao = Solicitacao

        Return Despacho
    End Function

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal DataInicial As Date, ByVal DataFinal As Date?) As IList(Of IDespacho) Implements IMapeadorDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Despachos As IList(Of IDespacho) = New List(Of IDespacho)

        Sql.Append("SELECT ID, TIPO, IDSOLICITANTE, IDALVO, IDSOLICITACAO, TIPOSOLICITACAO, DATADODESPACHO, TIPODESTDESPACHO, IDCOMPROMISSO, IDTAREFA, IDLEMBRETE")
        Sql.Append(" FROM DRY_DESPACHO ")
        Sql.Append(" WHERE IDSOLICITACAO = " & IDSolicitacao)

        Sql.Append(String.Concat(" AND DATADODESPACHO >= '", DataInicial.ToString("yyyyMMdd") & "000101", "'"))

        If DataFinal.HasValue Then
            Sql.Append(String.Concat(" AND DATADECADASTRO <= '", DataFinal.Value.ToString("yyyyMMdd") & "235959", "'"))
        End If

        Sql.Append(" ORDER BY DATADODESPACHO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Despachos.Add(Me.ObtenhaObjetoDespacho(Leitor))
            End While
        End Using

        Return Despachos
    End Function

    Public Function ObtenhaDespachosDaSolicitacao(ByVal IDSolicitacao As Long, ByVal Tipo As TipoDeDespacho) As IList(Of IDespacho) Implements IMapeadorDeDespacho.ObtenhaDespachosDaSolicitacao
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Despachos As IList(Of IDespacho) = New List(Of IDespacho)

        Sql.Append("SELECT ID, TIPO, IDSOLICITANTE, IDALVO, IDSOLICITACAO, TIPOSOLICITACAO, DATADODESPACHO, TIPODESTDESPACHO, IDCOMPROMISSO, IDTAREFA, IDLEMBRETE")
        Sql.Append(" FROM DRY_DESPACHO ")
        Sql.Append(" WHERE IDSOLICITACAO = " & IDSolicitacao)
        Sql.Append(" AND TIPO = " & Tipo.ID.ToString)
        Sql.Append(" ORDER BY DATADODESPACHO")

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read
                Despachos.Add(Me.ObtenhaObjetoDespacho(Leitor))
            End While
        End Using

        Return Despachos
    End Function

End Class