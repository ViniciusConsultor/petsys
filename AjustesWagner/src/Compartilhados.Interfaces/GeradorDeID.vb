Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Servicos

Public Class GeradorDeID

    Private Shared instancia As GeradorDeID
    Private Shared objetoLock As New Object
    Private Shared contadorLock As New Object
    Private numeroHigh As Integer
    Private numeroLow As Integer

    Private Sub New()
        atualizarNumeroHigh()
        numeroLow = 0
    End Sub

    Public Shared Function getInstancia() As GeradorDeID
        If instancia Is Nothing Then
            SyncLock objetoLock
                If instancia Is Nothing Then
                    instancia = New GeradorDeID
                End If
            End SyncLock
        End If

        Return instancia
    End Function

    Public Shared Function ProximoID() As Long
        Return getInstancia.getProximoID
    End Function

    Private Sub atualizarNumeroHigh()
        Using Servico As IServicoDeGeracaoDeID = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeGeracaoDeID)()
            numeroHigh = Servico.ObtenhaNumeroHigh() + 1
            Servico.ArmazeneNumeroHigh(numeroHigh)
        End Using
    End Sub

    Public Function getProximoID() As Long
        Dim numeroID As New Long

        SyncLock contadorLock
            numeroLow += 1
            If numeroLow > 9999 Then
                atualizarNumeroHigh()
                numeroLow = 0
            End If

            numeroID = CType(numeroHigh, Long) * 10000
            numeroID += CType(numeroLow, Long)
        End SyncLock

        Return numeroID
    End Function

End Class