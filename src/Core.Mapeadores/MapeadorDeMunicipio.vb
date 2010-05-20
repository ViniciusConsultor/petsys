Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Interfaces

Public Class MapeadorDeMunicipio
    Implements IMapeadorDeMunicipio

    Public Sub Excluir(ByVal Id As Long) Implements IMapeadorDeMunicipio.Excluir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("DELETE FROM NCL_MUNICIPIO")
        SQL.Append(String.Concat(" WHERE ID = ", Id))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Inserir(ByVal Municipio As IMunicipio) Implements IMapeadorDeMunicipio.Inserir
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        Municipio.ID = GeradorDeID.getInstancia.getProximoID

        SQL.Append("INSERT INTO NCL_MUNICIPIO (ID, NOME, UF, CEP)")
        SQL.Append(" VALUES ( ")
        SQL.Append(String.Concat(Municipio.ID, ", "))
        SQL.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Municipio.Nome), "', "))
        SQL.Append(String.Concat(Municipio.UF.ID, ", "))

        If Not Municipio.CEP Is Nothing AndAlso Not Municipio.CEP.Numero Is Nothing Then
            SQL.Append(Municipio.CEP.Numero)
        Else
            SQL.Append("NULL")
        End If

        SQL.Append(")")

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Sub Modificar(ByVal Municipio As IMunicipio) Implements IMapeadorDeMunicipio.Modificar
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper = ServerUtils.getDBHelper

        SQL.Append("UPDATE NCL_MUNICIPIO SET ")
        SQL.Append(String.Concat("NOME = '", UtilidadesDePersistencia.FiltraApostrofe(Municipio.Nome), "', "))
        SQL.Append(String.Concat("UF = ", Municipio.UF.ID, ", "))
        SQL.Append("CEP = ")

        If Not Municipio.CEP Is Nothing AndAlso Not Municipio.CEP.Numero Is Nothing Then
            SQL.Append(Municipio.CEP.Numero)
        Else
            SQL.Append("NULL")
        End If

        SQL.Append(" WHERE ")
        SQL.Append(String.Concat("ID = ", Municipio.ID))

        DBHelper.ExecuteNonQuery(SQL.ToString)
    End Sub

    Public Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio Implements IMapeadorDeMunicipio.ObtenhaMunicipio
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()
        SQL.Append(String.Concat(" WHERE ID = ", Id))
        Return ObtenhaMunicio(SQL)
    End Function

    Private Function ObtenhaMunicio(ByVal SQL As StringBuilder) As IMunicipio
        Dim Municipios As IList(Of IMunicipio)
        Dim Municipio As IMunicipio = Nothing

        Municipios = ObtenhaMunicipios(SQL, Integer.MaxValue)

        If Not Municipios.Count = 0 Then
            Municipio = Municipios.Item(0)
        End If

        Return Municipio
    End Function

    Private Function ObtenhaMunicipios(ByVal SQL As StringBuilder, ByVal QuantidadeMaxima As Integer) As IList(Of IMunicipio)
        Dim DBHelper As IDBHelper = ServerUtils.criarNovoDbHelper
        Dim Municipios As IList(Of IMunicipio)
        Dim Municipio As IMunicipio

        Municipios = New List(Of IMunicipio)

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            While Leitor.Read AndAlso Municipios.Count < QuantidadeMaxima

                Municipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IMunicipio)()
                Municipio.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "ID")
                Municipio.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")
                Municipio.UF = UF.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "UF"))

                If Not IsDBNull(Leitor("CEP")) Then
                    Municipio.CEP = New CEP(UtilidadesDePersistencia.GetValorLong(Leitor, "CEP"))
                End If

                Municipios.Add(Municipio)
            End While
        End Using

        Return Municipios
    End Function

    Private Function ObtenhaSQL() As StringBuilder
        Dim SQL As New StringBuilder

        SQL.Append("SELECT ID, NOME, UF, CEP")
        SQL.Append(" FROM NCL_MUNICIPIO")

        Return SQL
    End Function

    Public Function Existe(ByVal Municipio As IMunicipio) As Boolean Implements IMapeadorDeMunicipio.Existe
        Dim SQL As New StringBuilder
        Dim DBHelper As IDBHelper

        SQL.Append("SELECT COUNT(*) AS QUANTIDADE FROM NCL_MUNICIPIO")
        SQL.Append(" WHERE NOME = '")
        SQL.Append(UtilidadesDePersistencia.FiltraApostrofe(Municipio.Nome))
        SQL.Append("'")
        SQL.Append(" AND UF = ")
        SQL.Append(Municipio.UF.ID)

        If Not Municipio.ID Is Nothing Then
            SQL.Append(" AND ID <> ")
            SQL.Append(Municipio.ID)
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(SQL.ToString)
            If Leitor.Read Then
                Return UtilidadesDePersistencia.getValorInteger(Leitor, "QUANTIDADE") <> 0
            End If
        End Using

    End Function

    Public Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, _
                                                       ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio) Implements IMapeadorDeMunicipio.ObtenhaMunicipiosPorNomeComoFiltro
        Dim SQL As New StringBuilder

        SQL = ObtenhaSQL()

        If Not String.IsNullOrEmpty(Nome) Then
            SQL.Append(String.Concat(" WHERE NOME LIKE '", UtilidadesDePersistencia.FiltraApostrofe(Nome).ToUpper, "%'"))
        End If

        SQL.Append(" ORDER BY NOME")
        Return ObtenhaMunicipios(SQL, QuantidadeMaximaDeRegistros)
    End Function

End Class