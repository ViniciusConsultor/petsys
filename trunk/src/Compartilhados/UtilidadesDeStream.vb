﻿Imports System.IO
Imports System.Text

Public Class UtilidadesDeStream

    Public Shared Function ConvertaArquivoAnsiParaUtf8(arquivoTxtAnsi As Stream) As StreamReader
        Dim Bytes As Byte() = TransformaStreamEmArrayDeBytes(arquivoTxtAnsi)

        Dim arquivoConvertido = New MemoryStream(Encoding.Convert(Encoding.[Default], Encoding.UTF8, Bytes))
        Return New StreamReader(arquivoConvertido, Encoding.UTF8, True)
    End Function

    Public Shared Function TransformaStreamEmArrayDeBytes(arquivo As Stream) As Byte()
        Dim bytes As Byte()

        Using memstream = New MemoryStream()
            arquivo.CopyTo(memstream)
            bytes = memstream.ToArray()
        End Using

        Return bytes
    End Function

    Public Shared Function TransformeArrayBytesEmStream(ArrayBytes As Byte()) As StreamReader
        Dim arquivoMemoria = New MemoryStream(ArrayBytes)

        Return New StreamReader(arquivoMemoria)
    End Function


End Class
