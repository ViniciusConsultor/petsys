Imports System.Text
Imports System.Web

Public Class UtilidadesDePersistencia

    Public Shared Function FiltraApostrofe(ByVal Valor As String) As String
        Dim Retorno As String = Nothing

        If Not String.IsNullOrEmpty(Valor) Then
            Retorno = Valor.Replace("'", "`")
        End If

        Return Retorno
    End Function

    Public Shared Function TPVd(ByVal Valor As Double) As String
        Dim Retorno As String = Nothing

        If Not IsNothing(Valor) Then
            Retorno = Valor.ToString.Replace(",", ".")
        End If

        Return Retorno
    End Function
    
    Public Overloads Shared Function GetValorBooleano(ByVal Linha As DataRow, ByVal NomeColuna As String) As Boolean
        Return p_getValorBoolean(Linha.Item(NomeColuna))
    End Function

    Public Overloads Shared Function getValorBooleano(ByVal Linha As DataRowView, ByVal NomeColuna As String) As Boolean
        Return p_getValorBoolean(Linha.Item(NomeColuna))
    End Function

    Public Overloads Shared Function getValorBooleano(ByVal Leitor As IDataReader, ByVal NomeColuna As String) As Boolean
        Return p_getValorBoolean(Leitor.Item(NomeColuna))
    End Function

    Public Shared Function EhNulo(ByVal Leitor As IDataReader, ByVal NomeColuna As String) As Boolean
        Return IsDBNull(Leitor.Item(NomeColuna))
    End Function

    Public Shared Function GetValorArrayBytes(linha As IDataReader, NomeColuna As String) As Byte()
        Return CType(linha(NomeColuna), Byte())
    End Function


    Private Overloads Shared Function p_getValorBoolean(ByVal Valor As Object) As Boolean
        If IsDBNull(Valor) Then
            Return False
        End If

        If Valor.Equals("S") Or Valor.Equals(1) Or Valor.Equals("1") Then
            Return True
        End If

        Return False
    End Function

    Public Overloads Shared Function GetValorString(ByVal Leitor As IDataReader, ByVal NomeColuna As String) As String
        Return p_getValor(Leitor.Item(NomeColuna))
    End Function

    Public Overloads Shared Function GetValor(ByVal Linha As DataRow, ByVal NomeColuna As String) As String
        Return p_getValor(Linha.Item(NomeColuna))
    End Function

    Public Overloads Shared Function GetValor(ByVal Linha As DataRowView, ByVal NomeColuna As String) As String
        Return p_getValor(Linha.Item(NomeColuna))
    End Function

    Public Shared Function p_getValor(ByVal Valor As Object) As String
        If IsDBNull(Valor) Then
            Return ""
        End If

        Return CStr(Valor)
    End Function

    Public Overloads Shared Function GetValorLong(ByVal Leitor As IDataReader, ByVal NomeColuna As String) As Long
        Return p_getValorLong(Leitor.Item(NomeColuna))
    End Function

    Public Overloads Shared Function getValorLong(ByVal Linha As DataRow, ByVal NomeColuna As String) As Long
        Return p_getValorLong(Linha.Item(NomeColuna))
    End Function

    Public Overloads Shared Function getValorLong(ByVal Linha As DataRowView, ByVal NomeColuna As String) As Long
        Return p_getValorLong(Linha.Item(NomeColuna))
    End Function

    Public Shared Function p_getValorLong(ByVal Valor As Object) As Long
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim nNumero As Long
        Try
            nNumero = CType(Valor, Long)
        Catch ex As Exception
            nNumero = 0
        End Try

        Return nNumero
    End Function

    Public Shared Function getValorByte(ByVal Leitor As IDataReader, ByVal NomeColuna As String) As Byte
        Return p_getValorByte(Leitor.Item(NomeColuna))
    End Function

    Public Shared Function getValorByte(ByVal Linha As DataRow, ByVal NomeColuna As String) As Byte
        Return p_getValorByte(Linha.Item(NomeColuna))
    End Function

    Public Shared Function getValorByte(ByVal Linha As DataRowView, ByVal NomeColuna As String) As Byte
        Return p_getValorByte(Linha.Item(NomeColuna))
    End Function

    Public Shared Function p_getValorByte(ByVal Valor As Object) As Byte
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim Numero As Byte
        Try
            Numero = CType(Valor, Byte)
        Catch ex As Exception
            Numero = 0
        End Try

        Return Numero
    End Function

    Public Overloads Shared Function getValorInteger(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Integer
        Return p_getValorInteger(leitor.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorInteger(ByVal Linha As DataRow, ByVal nomeColuna As String) As Integer
        Return p_getValorInteger(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorInteger(ByVal Linha As DataRowView, ByVal nomeColuna As String) As Integer
        Return p_getValorInteger(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function p_getValorInteger(ByVal Valor As Object) As Integer
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim Numero As Integer
        Try
            Numero = CType(Valor, Integer)
        Catch ex As Exception
            Numero = 0
        End Try

        Return Numero
    End Function

    Public Overloads Shared Function getValorShort(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Short
        Return p_getValorShort(leitor.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorShort(ByVal Linha As DataRow, ByVal nomeColuna As String) As Short
        Return p_getValorShort(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorShort(ByVal Linha As DataRowView, ByVal nomeColuna As String) As Short
        Return p_getValorShort(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function p_getValorShort(ByVal Valor As Object) As Short
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim Numero As Short
        Try
            Numero = CType(Valor, Short)
        Catch ex As Exception
            Numero = 0
        End Try

        Return Numero
    End Function

    Public Shared Function getValorChar(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Char
        Return p_getValorChar(leitor.Item(nomeColuna))
    End Function

    Public Shared Function getValorChar(ByVal Linha As DataRow, ByVal nomeColuna As String) As Char
        Return p_getValorChar(Linha.Item(nomeColuna))
    End Function

    Public Shared Function getValorChar(ByVal Linha As DataRowView, ByVal nomeColuna As String) As Char
        Return p_getValorChar(Linha.Item(nomeColuna))
    End Function

    Public Shared Function ObtenhaStringMapeadaDeListaDeString(itens As IList(Of String), caracterSeparador As Char) As String
        Dim strConvertida As New StringBuilder

        For Each item As String In itens
            strConvertida.Append(item & caracterSeparador)
        Next

        Return strConvertida.Remove(strConvertida.Length - 1, 1).ToString()
    End Function

    Public Shared Function MapeieStringParaListaDeString(str As String, caracterSeparador As Char) As IList(Of String)
        Dim itens As String() = str.Split(caracterSeparador)

        Return New List(Of String)(itens)
    End Function

    Public Shared Function p_getValorChar(ByVal Valor As Object) As Char
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim Ch As Char
        Try
            Ch = CType(Valor, Char)
        Catch ex As Exception
            Ch = Nothing
        End Try

        Return Ch
    End Function

    Public Overloads Shared Function getValorDouble(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Double
        Return p_getValorDouble(leitor.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorDouble(ByVal Linha As DataRow, ByVal nomeColuna As String) As Double
        Return p_getValorDouble(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorDouble(ByVal Linha As DataRowView, ByVal nomeColuna As String) As Double
        Return p_getValorDouble(Linha.Item(nomeColuna))
    End Function

    Public Shared Function p_getValorDouble(ByVal Valor As Object) As Double
        If IsDBNull(Valor) Then
            Return Nothing
        End If

        Dim Numero As Double
        Try
            Numero = CType(Valor, Double)
        Catch ex As Exception
            Numero = 0
        End Try

        Return Numero
    End Function

    Public Overloads Shared Function getValorDate(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Nullable(Of Date)
        Return p_getValorDate(leitor.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorDate(ByVal Linha As DataRow, ByVal nomeColuna As String) As Nullable(Of Date)
        Return p_getValorDate(Linha.Item(nomeColuna))
    End Function

    Public Overloads Shared Function getValorDate(ByVal Linha As DataRowView, ByVal nomeColuna As String) As Nullable(Of Date)
        Return p_getValorDate(Linha.Item(nomeColuna))
    End Function

    Public Shared Function getValorHourMinute(ByVal leitor As IDataReader, ByVal nomeColuna As String) As Nullable(Of Date)
        If EhNulo(leitor, nomeColuna) Then Return Nothing

        Dim Horas As Integer = UtilidadesDePersistencia.getValorInteger(leitor, nomeColuna) \ 100 Mod 100
        Dim Minutos As Integer = UtilidadesDePersistencia.getValorInteger(leitor, nomeColuna) Mod 100

        Return New Date(Now.Year, Now.Month, Now.Day, Horas, Minutos, 0)
    End Function

    Public Overloads Shared Function getValorDateHourSec(ByVal Leitor As IDataReader, ByVal nomeColuna As String) As Nullable(Of Date)
        Dim dataStr As String = p_getValor(Leitor.Item(nomeColuna))
        Dim dataRetorno As Date

        If dataStr.Equals(String.Empty) Then
            Return Nothing
        End If

        Dim strParteData As String = Mid(dataStr, 1, 8)
        Dim strParteHora As String = Mid(dataStr, 9, dataStr.Length - 8)

        Dim cAno As Integer = CInt(strParteData) \ 10000
        Dim cMes As Integer = CInt(strParteData) \ 100 Mod 100
        Dim cDia As Integer = CInt(strParteData) Mod 100

        Dim cHoras As Integer = CInt(strParteHora) \ 10000
        Dim cMinutos As Integer = CInt(strParteHora) \ 100 Mod 100
        Dim Segundos As Integer = CInt(strParteHora) Mod 100

        dataRetorno = New Date(cAno, cMes, cDia, cHoras, cMinutos, Segundos)

        Return dataRetorno
    End Function

    Public Shared Function p_getValorDate(ByVal valor As Object) As Nullable(Of Date)

        Dim dataAsInt As Integer

        If IsDBNull(valor) OrElse CType(valor, Integer) = 0 Then
            Return Nothing
        End If

        dataAsInt = p_getValorInteger(valor)
        Return New Date(dataAsInt \ 10000, dataAsInt \ 100 Mod 100, dataAsInt Mod 100)
    End Function

    Public Shared Function MontaFiltro(Of T)(ByVal sNomeCampo As String, ByVal lista As IList(Of T), ByVal sOperacao As String, ByVal insereAspas As Boolean) As String
        Dim sAux As String

        If lista.Count = 0 Then Return String.Empty
        sAux = ""

        For Each oid As T In lista
            If lista.IndexOf(oid) <> 0 Then
                sAux &= " " & Trim$(sOperacao) & " "
            End If

            sAux &= " " & sNomeCampo & " = " & IIf(insereAspas, "'" & oid.ToString() & "'", oid.ToString()).ToString()
        Next

        sAux = "(" & Trim(sAux) & ")"

        Return " " & Trim(sAux) & " "

    End Function
End Class
