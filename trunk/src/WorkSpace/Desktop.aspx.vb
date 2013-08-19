Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Visual

Partial Public Class Desktop
    Inherits PaginaDesktop

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Fabrica As New FabricaDeMenu

            Me.ClientScript.RegisterStartupScript(Me.GetType(), "jsmenu", Fabrica.ObtenhaMenu)
            Me.MontaAtalhos()
            Me.ExibaNotifacoes()
        End If
    End Sub

    Private Sub MontaAtalhos()
        Dim Atalhos As IList(Of Atalho)
        Dim Tabela As New HtmlTable
        Dim Linha As HtmlTableRow

        Atalhos = FabricaDeContexto.GetInstancia.GetContextoAtual.Perfil.Atalhos

        If Atalhos Is Nothing OrElse Atalhos.Count = 0 Then Exit Sub

        For I = 0 To 3
            Linha = New HtmlTableRow()
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Linha.Cells.Add(New HtmlTableCell())
            Tabela.Rows.Add(Linha)
        Next

        Dim IndiceLinha As Integer = 0
        Dim IndiceColuna As Integer = 0

        For Each Item As Atalho In Atalhos
            'Cria a div
            Dim Div As New HtmlGenericControl("div")
            Div.InnerText = Item.Nome

            'Cria a imagem
            Dim Imagem As New HtmlImage()
            Imagem.Src = Item.ObtenhaURLImagemCompleta

            Dim Link As New HtmlGenericControl("a ")
            Link.Attributes("href") = Item.ObtenhaURLCompleta(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual)

            Dim dt As New HtmlGenericControl("dt")
            dt.Attributes("id") = Item.ID.Replace(".", "_")

            Link.Controls.Add(Imagem)
            Link.Controls.Add(Div)
            dt.Controls.Add(Link)

            If IndiceLinha > 3 Then
                IndiceLinha = 0
                IndiceColuna += 1
            End If

            Tabela.Rows(IndiceLinha).Cells(IndiceColuna).Controls.Add(dt)
            IndiceLinha += 1
        Next

        Me.shortcuts.Controls.Add(Tabela)
    End Sub

    Private Sub ExibaNotifacoes()

    End Sub

End Class