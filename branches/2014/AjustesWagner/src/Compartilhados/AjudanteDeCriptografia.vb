Imports System.IO
Imports System.Security.Cryptography
Imports System.Text


Public Class AjudanteDeCriptografia

    Private Shared key As RC2CryptoServiceProvider = New RC2CryptoServiceProvider

    Shared Sub New()
        AjudanteDeCriptografia.key.IV = New Byte() {&H89, 220, &H92, &H8E, &HF3, 0, &HA9, &H20}
        AjudanteDeCriptografia.key.Key = New Byte() {&HF2, &HBA, &H36, &HAD, &H85, &HB6, &H93, &HFC, &H4D, &HB5, &H8B, &H84, 80, 110, &H9F, &H15}
    End Sub

    Public Shared Function Criptografe(ByVal TextoNormal As String) As String
        Dim stream As New MemoryStream
        Dim stream2 As New CryptoStream(stream, AjudanteDeCriptografia.key.CreateEncryptor, CryptoStreamMode.Write)
        Dim writer As New StreamWriter(stream2)
        writer.WriteLine(TextoNormal)
        writer.Close()
        stream2.Close()
        Dim inArray As Byte() = stream.ToArray
        stream.Close()
        Return Convert.ToBase64String(inArray)
    End Function

    Public Shared Function CriptografeMaoUnicao(ByVal TextoNormal As String) As String
        Dim provider As New MD5CryptoServiceProvider
        Dim encoding As New UTF8Encoding
        Dim builder As New StringBuilder
        Dim num As Byte
        For Each num In provider.ComputeHash(encoding.GetBytes(TextoNormal))
            builder.Append((num.ToString("x2") & " "))
        Next
        Return builder.ToString.TrimEnd(New Char(0 - 1) {})
    End Function

    Public Shared Function Descriptografe(ByVal TextoCriptografado As String) As String
        Dim stream As New MemoryStream(Convert.FromBase64String(TextoCriptografado))
        Dim stream2 As New CryptoStream(stream, AjudanteDeCriptografia.key.CreateDecryptor, CryptoStreamMode.Read)
        Dim reader As New StreamReader(stream2)
        Dim str As String = reader.ReadLine
        reader.Close()
        stream2.Close()
        stream.Close()
        Return str
    End Function

End Class



