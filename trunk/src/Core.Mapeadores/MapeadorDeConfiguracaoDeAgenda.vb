Imports Core.Interfaces.Mapeadores
Imports Core.Interfaces.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio

Public Class MapeadorDeConfiguracaoDeAgenda
    Implements IMapeadorDeConfiguracaoDeAgenda

    Public Sub Excluir(ByVal ID As Long) Implements IMapeadorDeConfiguracaoDeAgenda.Excluir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_CFGAGENDA")
        Sql.Append(String.Concat(" WHERE ID = ", ID.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Inserir(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IMapeadorDeConfiguracaoDeAgenda.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper
        ConfiguracaoDeAgenda.ID = GeradorDeID.getInstancia.getProximoID

        Sql.Append("INSERT INTO NCL_CFGAGENDA (")
        Sql.Append("ID, NOME, PRIMEIRODIASEMANA, HORARIOINICIO, ")
        Sql.Append("HORARIOFIM, INTERVALO) ")
        Sql.Append("VALUES (")
        Sql.Append(String.Concat(ConfiguracaoDeAgenda.ID.Value.ToString, ", "))
        Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(ConfiguracaoDeAgenda.Nome), "', "))
        Sql.Append(String.Concat(ConfiguracaoDeAgenda.PrimeiroDiaDaSemana.IDDoDia, ", "))
        Sql.Append(String.Concat(ConfiguracaoDeAgenda.HoraDeInicio.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(ConfiguracaoDeAgenda.HoraDeTermino.ToString("HHmm"), ", "))
        Sql.Append(String.Concat(ConfiguracaoDeAgenda.IntervaloEntreHorarios.ToString("HHmm"), ")"))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Sub Modificar(ByVal ConfiguracaoDeAgenda As IConfiguracaoDeAgenda) Implements IMapeadorDeConfiguracaoDeAgenda.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_CFGAGENDA SET ")
        Sql.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(ConfiguracaoDeAgenda.Nome), "', "))
        Sql.Append(String.Concat("PRIMEIRODIASEMANA = ", ConfiguracaoDeAgenda.PrimeiroDiaDaSemana.IDDoDia, ", "))
        Sql.Append(String.Concat("HORARIOINICIO = ", ConfiguracaoDeAgenda.HoraDeInicio.ToString("HHmm"), ", "))
        Sql.Append(String.Concat("HORARIOFIM = ", ConfiguracaoDeAgenda.HoraDeTermino.ToString("HHmm"), ", "))
        Sql.Append(String.Concat("INTERVALO = ", ConfiguracaoDeAgenda.IntervaloEntreHorarios.ToString("HHmm")))
        Sql.Append(String.Concat("WHERE ID = ", ConfiguracaoDeAgenda.ID.Value.ToString))

        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaConfiguracao(ByVal ID As Long) As IConfiguracaoDeAgenda Implements IMapeadorDeConfiguracaoDeAgenda.ObtenhaConfiguracao
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, PRIMEIRODIASEMANA, HORARIOINICIO, ")
        Sql.Append("HORARIOFIM, INTERVALO ")
        Sql.Append("FROM NCL_CFGAGENDA ")
        Sql.Append("WHERE ID = " & ID.ToString)

        Dim Configuracao As IConfiguracaoDeAgenda = Nothing
        Dim Configuracoes As IList(Of IConfiguracaoDeAgenda)

        Configuracoes = ObtenhaConfiguracoes(Sql, Integer.MaxValue)

        If Not Configuracoes.Count = 0 Then
            Configuracao = Configuracoes.Item(0)
        End If

        Return Configuracao
    End Function

    Public Function ObtenhaConfiguracoesPorNomeComoFiltro(ByVal Nome As String, _
                                                          ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda) Implements IMapeadorDeConfiguracaoDeAgenda.ObtenhaConfiguracoesPorNomeComoFiltro
        Dim Sql As New StringBuilder

        Sql.Append("SELECT ID, NOME, PRIMEIRODIASEMANA, HORARIOINICIO, ")
        Sql.Append("HORARIOFIM, INTERVALO ")
        Sql.Append("FROM NCL_CFGAGENDA ")

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat("WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome).ToUpper, "%'"))
        End If

        Return ObtenhaConfiguracoes(Sql, Integer.MaxValue)
    End Function

    Private Function ObtenhaConfiguracoes(ByVal Sql As StringBuilder, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IConfiguracaoDeAgenda)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Configuracoes As IList(Of IConfiguracaoDeAgenda)
        Dim Configuracao As IConfiguracaoDeAgenda

        Configuracoes = New List(Of IConfiguracaoDeAgenda)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            While Leitor.Read AndAlso Configuracoes.Count < QuantidadeMaximaDeRegistros
                Configuracao = FabricaGenerica.GetInstancia.CrieObjeto(Of IConfiguracaoDeAgenda)()
                Configuracao.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Configuracao.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                Configuracao.HoraDeInicio = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORARIOINICIO").Value
                Configuracao.HoraDeTermino = UtilidadesDePersistencia.getValorHourMinute(Leitor, "HORARIOFIM").Value
                Configuracao.IntervaloEntreHorarios = UtilidadesDePersistencia.getValorHourMinute(Leitor, "INTERVALO").Value
                Configuracao.PrimeiroDiaDaSemana = DiaDaSemana.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "PRIMEIRODIASEMANA"))
                Configuracoes.Add(Configuracao)
            End While
        End Using

        Return Configuracoes
    End Function

End Class
