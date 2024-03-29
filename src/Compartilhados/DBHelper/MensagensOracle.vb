﻿Namespace DBHelper

    Friend Class MensagensOracle
        Inherits MensagensDeBanco

        Private Shared DicionarioDeMensagens As IDictionary(Of String, String)
        Private Shared Instancia As MensagensOracle
        Private Shared ObjLock As New Object

        Private Sub New()
            DicionarioDeMensagens = New Dictionary(Of String, String)

            DicionarioDeMensagens.Add("2292", "Não será possível executar a operação. O dado está sendo utilizado pelo sistema.")
            DicionarioDeMensagens.Add("1", "Não será possível executar a operação. O dado já existe no sistema.")
        End Sub

        Public Shared Function GetInstancia() As MensagensOracle
            If Instancia Is Nothing Then
                SyncLock ObjLock
                    If Instancia Is Nothing Then
                        Instancia = New MensagensOracle
                    End If
                End SyncLock
            End If

            Return Instancia
        End Function

        Public Overrides Function ObtenhaMensagemDeErro(ByVal CodigoDoErro As String) As String
            Dim MensagemDeErro As String = Nothing

            DicionarioDeMensagens.TryGetValue(CodigoDoErro, MensagemDeErro)

            Return MensagemDeErro
        End Function

    End Class

End Namespace

