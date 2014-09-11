Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting
Imports Compartilhados
Imports System.Reflection
Imports System.IO
Imports System.Text

Public Class Form1

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IniciaServidorRemoting(False)
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

    Private Sub IniciaServidorRemoting(ByVal EhReinicio As Boolean)
        Dim StringDeStatus As New StringBuilder

        If EhReinicio Then
            StringDeStatus.AppendLine("Reiniciando servidor de aplicação...")
        Else
            StringDeStatus.AppendLine("Inciando servidor de aplicação...")
        End If

        txtStatus.Text = StringDeStatus.ToString

        If EhReinicio Then
            StringDeStatus.AppendLine("Desregistrando canais de comunicação....")
            txtStatus.Text = StringDeStatus.ToString

            For Each Canal As IChannel In ChannelServices.RegisteredChannels
                ChannelServices.UnregisterChannel(Canal)
                StringDeStatus.AppendLine("Desregistrando canal " & Canal.ChannelName)
                txtStatus.Text = StringDeStatus.ToString
            Next

            txtStatus.Text = StringDeStatus.ToString
        End If

        StringDeStatus.AppendLine("Registrando canal de comunicação.... Protocolo TCP porta 1235")
        ChannelServices.RegisterChannel(New TcpServerChannel(1235), False)

        txtStatus.Text = StringDeStatus.ToString
        Dim DicionarioDeAssemblyTypes As Dictionary(Of String, Type())

        StringDeStatus.AppendLine("Carregando Serviços Remotos...")
        txtStatus.Text = StringDeStatus.ToString
        DicionarioDeAssemblyTypes = CarregaAssemblysDeServicos()

        For Each Item As KeyValuePair(Of String, Type()) In DicionarioDeAssemblyTypes
            For Each TipoDoAssembly As Type In Item.Value
                If Not TipoDoAssembly.Namespace.Contains("My") Then
                    StringDeStatus.AppendLine("Tipo " & TipoDoAssembly.Name & " carregado com sucesso.")
                    txtStatus.Text = StringDeStatus.ToString
                    RemotingConfiguration.RegisterWellKnownServiceType(TipoDoAssembly, TipoDoAssembly.Name, WellKnownObjectMode.Singleton)
                End If
            Next
        Next
    End Sub

    Private Sub btnReiniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReiniciar.Click
        IniciaServidorRemoting(True)
    End Sub

End Class