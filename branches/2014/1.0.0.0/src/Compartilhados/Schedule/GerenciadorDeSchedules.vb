Imports System.Xml
Imports Compartilhados.Fabricas
Imports System.Reflection

Namespace Schedule

    Public Class GerenciadorDeSchedules

        Private _Schedules As XmlDocument
        Private Shared InstanciaSolitaria As GerenciadorDeSchedules
        Private Shared ReadOnly ObjetoLock As New Object

        Public Sub InicializeSchedules()
            Dim noSchedules As XmlNode = _Schedules.ChildNodes.Item(0)

            For Each NoSchedule As XmlNode In noSchedules
                Dim Instancia As Object = Nothing

                Try
                    Instancia = FabricaGenerica.GetInstancia.CrieObjeto(NoSchedule.Attributes("fullname").Value, NoSchedule.Attributes("type").Value)
                    Dim mi As MethodInfo = Instancia.GetType().GetMethod("Inicie")

                    Dim segundos As Nullable(Of Double)

                    If Not NoSchedule.Attributes("segundos") Is Nothing Then

                        segundos = CDbl(NoSchedule.Attributes("segundos").Value)
                    End If

                    mi.Invoke(Instancia, BindingFlags.InvokeMethod, Nothing, New Object() {segundos}, Nothing)
                Catch ex As DLLNaoEncontradaException
                    'Não faz nada
                End Try
            Next
        End Sub

        Private Sub New()
            CarregueSchedules()
        End Sub

        Public Shared Function GetInstancia() As GerenciadorDeSchedules

            If InstanciaSolitaria Is Nothing Then
                SyncLock ObjetoLock
                    If InstanciaSolitaria Is Nothing Then
                        InstanciaSolitaria = New GerenciadorDeSchedules
                    End If
                End SyncLock
            End If

            Return InstanciaSolitaria
        End Function

        Private Sub CarregueSchedules()
            _Schedules = New XmlDocument
            _Schedules.Load(Util.ObtenhaCaminhoArquivoXMLDeSchedule())
        End Sub
    End Class

End Namespace