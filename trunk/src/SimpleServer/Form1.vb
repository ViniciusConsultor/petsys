Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting
Imports Compartilhados

Public Class Form1

    Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIniciar.Click
        Dim tcp As TcpChannel = New TcpChannel(8080)

        ChannelServices.RegisterChannel(tcp)
        RemotingConfiguration.RegisterWellKnownServiceType(GetType(ServicoRemoto), "ServicoRemoto", WellKnownObjectMode.SingleCall)
    End Sub
End Class
