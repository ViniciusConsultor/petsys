Imports LotoFacil.Negocio
Imports LotoFacil.Facade

Public Class frmNovaAposta

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNovo.Click
        Dim DezenasEscolhidas As IList(Of Dezena)

        DezenasEscolhidas = New List(Of Dezena)

        For Each Item As String In chkDezenasEscolhidas.CheckedItems
            Dim Dezena As Dezena = New Dezena(CShort(Item))

            DezenasEscolhidas.Add(Dezena)
        Next

        Dim Facade As FacadeGeradorDeJogos
        Dim JogosGerados As IList(Of Jogo)
        Dim Concurso As Concurso = Nothing

        Facade = New FacadeGeradorDeJogos()

        Try
            JogosGerados = Facade.GereJogos(Concurso, DezenasEscolhidas)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Inconsistencias")
        End Try

    End Sub

    Private Sub frmNovaAposta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ExibaTelaInicial()
    End Sub

    Private Sub ExibaTelaInicial()

    End Sub
End Class