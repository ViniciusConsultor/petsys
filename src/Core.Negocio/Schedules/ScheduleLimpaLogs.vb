Imports Compartilhados
Imports Compartilhados.Schedule
Imports Core.Interfaces.Negocio.Schedules
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports System.IO

Namespace Schedules

    Public Class ScheduleLimpaLogs
        Inherits Schedule
        Implements IScheduleLimpaLogs

        Protected Overrides Sub ExecuteTarefa()
            'Quando precisar de conexao na execução da tarefa é necessário pegar nova conexao
            'Using Servico As IServicoDeConexao = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeConexao)()
            '    FabricaDeContexto.GetInstancia.GetContextoAtual.Conexao = Servico.ObtenhaConexao
            'End Using

            Dim arquivos = Directory.GetFileSystemEntries(Util.GetDiretorioLog())

            For Each arquivo In arquivos
                Dim info = New FileInfo(arquivo)

                If CLng(info.LastWriteTime.ToString("yyyMMdd")) < CLng(Now.ToString("yyyMMdd")) Then
                    File.Delete(arquivo)
                End If

            Next
        End Sub

        Protected Overrides Sub Inicialize()
          
        End Sub

        Public Overrides ReadOnly Property Nome As String
            Get
                Return "Schedule limpa logs"
            End Get
        End Property
    End Class

End Namespace