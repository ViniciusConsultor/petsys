Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting
Imports Compartilhados
Imports System.Reflection
Imports System.IO

Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ChannelServices.RegisterChannel(New TcpServerChannel(1235), False)

        Dim DicionarioDeAssemblyTypes As Dictionary(Of String, Type())

        DicionarioDeAssemblyTypes = CarregaAssemblysDeServicos()

        For Each Item As KeyValuePair(Of String, Type()) In DicionarioDeAssemblyTypes
            For Each TipoDoAssembly As Type In Item.Value
                If Not TipoDoAssembly.Namespace.Contains("My") Then
                    RemotingConfiguration.RegisterWellKnownServiceType(TipoDoAssembly, TipoDoAssembly.Name, WellKnownObjectMode.Singleton)
                End If
            Next
        Next
    End Sub

    Private Function CarregaAssemblysDeServicos() As Dictionary(Of String, Type())
        Dim ArquivosDoDiretorioDeAplicacao() As String
        Dim DicionarioDeAssemblyTypes = New Dictionary(Of String, Type())

        ArquivosDoDiretorioDeAplicacao = Directory.GetFiles(Util.ObtenhaCaminhoDaPastaDoServidorDeAplicacao)

        For Each Arquivo As String In ArquivosDoDiretorioDeAplicacao
            If Arquivo.Contains("Remoting") AndAlso Arquivo.EndsWith(".dll") Then
                Dim Asse As Assembly

                Asse = Assembly.LoadFile(Arquivo)
                DicionarioDeAssemblyTypes.Add(Arquivo, Asse.GetTypes)
            End If
        Next

        Return DicionarioDeAssemblyTypes
    End Function

 
End Class