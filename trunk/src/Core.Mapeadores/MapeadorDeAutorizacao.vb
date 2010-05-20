Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Compartilhados.DBHelper
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Mapeadores
Imports System.Text

Public Class MapeadorDeAutorizacao
    Implements IMapeadorDeAutorizacao

    Public Function ObtenhaModulosDisponiveis() As IList(Of IModulo) Implements IMapeadorDeAutorizacao.ObtenhaModulosDisponiveis
        Dim SQL As New StringBuilder
        Dim Modulos As IList(Of IModulo)
        Dim DBHelper As IDBHelper

        SQL.AppendLine("SELECT NCL_MODULO.IDMODULO AS ID_MODULO, NCL_MODULO.NOME AS NOME_MODULO,")
        SQL.AppendLine("NCL_FUNCAO.IDFUNCAO AS ID_FUNCAO, NCL_FUNCAO.NOME AS NOME_FUNCAO,")
        SQL.AppendLine("NCL_OPERACAO.IDOPERACAO AS ID_OPERACAO, NCL_OPERACAO.NOME AS NOME_OPERACAO")
        SQL.AppendLine(" FROM NCL_MODULO")
        SQL.AppendLine(" LEFT JOIN NCL_FUNCAO ON NCL_MODULO.IDMODULO = NCL_FUNCAO.IDMODULO")
        SQL.AppendLine(" LEFT JOIN NCL_OPERACAO ON NCL_FUNCAO.IDMODULO = NCL_OPERACAO.IDMODULO AND")
        SQL.AppendLine(" NCL_FUNCAO.IDFUNCAO = NCL_OPERACAO.IDFUNCAO")
        SQL.AppendLine(" ORDER BY NCL_MODULO.IDMODULO, NCL_FUNCAO.IDFUNCAO, NCL_OPERACAO.IDOPERACAO")

        DBHelper = ServerUtils.criarNovoDbHelper
        Modulos = New List(Of IModulo)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            Dim Modulo As IModulo = Nothing
            Dim Funcao As IFuncao = Nothing
            Dim Operacao As IOperacao = Nothing
            Dim IdModuloCorrente As String = ""
            Dim IdFuncaoCorrente As String = ""

            While Leitor.Read

                If IdModuloCorrente <> UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO") Then
                    Modulo = FabricaGenerica.GetInstancia.CrieObjeto(Of IModulo)()
                    Modulo.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO")
                    Modulo.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_MODULO")
                    Modulos.Add(Modulo)
                    IdModuloCorrente = UtilidadesDePersistencia.GetValorString(Leitor, "ID_MODULO")
                End If

                If IdFuncaoCorrente <> UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO") Then
                    Funcao = FabricaGenerica.GetInstancia.CrieObjeto(Of IFuncao)()
                    Funcao.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO")
                    Funcao.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_FUNCAO")
                    Modulo.AdicioneFuncao(Funcao)
                    IdFuncaoCorrente = UtilidadesDePersistencia.GetValorString(Leitor, "ID_FUNCAO")
                End If

                If Not UtilidadesDePersistencia.EhNulo(Leitor, "ID_OPERACAO") Then
                    Operacao = FabricaGenerica.GetInstancia.CrieObjeto(Of IOperacao)()
                    Operacao.ID = UtilidadesDePersistencia.GetValorString(Leitor, "ID_OPERACAO")
                    Operacao.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME_OPERACAO")

                    Funcao.AdicioneOperacao(Operacao)
                End If
            End While
        End Using

        Return Modulos
    End Function

    Public Sub Modifique(ByVal IDGrupo As Long, _
                         ByVal Diretivas As IList(Of IDiretivaDeSeguranca)) Implements IMapeadorDeAutorizacao.Modifique
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        SQL = New StringBuilder

        SQL.AppendLine("DELETE FROM NCL_AUTORIZACAO")
        SQL.AppendLine(String.Concat(" WHERE IDGRUPO =", IDGrupo.ToString))

        DBHelper.ExecuteNonQuery(SQL.ToString)

        For Each Diretiva As IDiretivaDeSeguranca In Diretivas
            SQL = New StringBuilder

            SQL.Append("INSERT INTO NCL_AUTORIZACAO (IDGRUPO, IDDIRETIVA)")
            SQL.Append(" VALUES (")
            SQL.Append(String.Concat(IDGrupo.ToString, ", "))
            SQL.Append(String.Concat("'", Diretiva.ID, "')"))
            DBHelper.ExecuteNonQuery(SQL.ToString)
        Next
    End Sub

    Public Function ObtenhaDiretivasDeSegurancaDoGrupo(ByVal ID As Long) As IList(Of IDiretivaDeSeguranca) Implements IMapeadorDeAutorizacao.ObtenhaDiretivasDeSegurancaDoGrupo
        Dim SQL As StringBuilder
        Dim DBHelper As IDBHelper
        Dim Diretivas As IList(Of IDiretivaDeSeguranca)
        Dim Diretiva As IDiretivaDeSeguranca

        DBHelper = ServerUtils.criarNovoDbHelper

        SQL = New StringBuilder

        SQL.Append("SELECT IDDIRETIVA")
        SQL.Append(" FROM NCL_AUTORIZACAO WHERE ")
        SQL.Append(String.Concat("IDGRUPO = ", ID.ToString))

        Diretivas = New List(Of IDiretivaDeSeguranca)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read
                Diretiva = FabricaGenerica.GetInstancia.CrieObjeto(Of IDiretivaDeSeguranca)()
                Diretiva.ID = UtilidadesDePersistencia.GetValorString(Leitor, "IDDIRETIVA")
                Diretivas.Add(Diretiva)
            End While
        End Using

        Return Diretivas
    End Function

End Class