﻿Imports Compartilhados
Imports Compartilhados.Schedule
Imports Core.Interfaces.Negocio.Schedules
Imports System.IO

Namespace Schedules

    Public Class ScheduleLimpaPdfs
        Inherits Schedule
        Implements IScheduleLimpaPdfs

        Protected Overrides Sub ExecuteTarefa()
            Dim arquivos = Directory.GetFileSystemEntries(Util.GetDiretorioLoads())

            For Each arquivo In arquivos
                Dim info = New FileInfo(arquivo)

                If CLng(info.LastWriteTime.ToString("yyyMMdd")) < CLng(Now.ToString("yyyMMdd")) Then
                    If Not String.IsNullOrEmpty(info.Extension) Then
                        File.Delete(arquivo)
                    End If
                End If
            Next
        End Sub

        Protected Overrides Sub Inicialize(Credencial As ICredencial)

        End Sub

        Public Overrides ReadOnly Property Nome As String
            Get
                Return "Schedule limpa PDFS"
            End Get
        End Property
    End Class

End Namespace
