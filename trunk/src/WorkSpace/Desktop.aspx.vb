Imports Compartilhados
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Visual
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Core.Interfaces.Negocio

Partial Public Class Desktop
    Inherits PaginaDesktop

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
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

            Dim id As String = Item.ID.Replace(".", "_")

            If Item.Tipo.Equals(TipoAtalho.Externo) Then
                id = id & "externo"
            End If

            dt.Attributes("id") = id

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

    Protected Sub cboPesquisa_OnItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs)
        cboPesquisa.Items.Clear()
        Dim ajudanteDePesquisa As HashSet(Of DTOAjudanteDePesquisaDeMenu)

        Using Servico As IServicoDeMenu = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeMenu)()
            ajudanteDePesquisa = Servico.ObtenhaFuncoesComCaminhoDoMenu(e.Text)
        End Using

        If ajudanteDePesquisa.Count() = 0 Then Exit Sub
        
        For Each dtoAjudanteDePesquisaDeMenu As DTOAjudanteDePesquisaDeMenu In From dtoAjudanteDePesquisaDeMenu1 In ajudanteDePesquisa Where FabricaDeContexto.GetInstancia().GetContextoAtual().EstaAutorizado(dtoAjudanteDePesquisaDeMenu1.IDDaFuncao)
            cboPesquisa.Items.Add(New RadComboBoxItem(dtoAjudanteDePesquisaDeMenu.NomeDaFuncao, dtoAjudanteDePesquisaDeMenu.CaminhoMenu))
        Next
    End Sub

    Protected Sub cboPesquisa_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        If String.IsNullOrEmpty(DirectCast(sender, RadComboBox).SelectedValue) Then
            cboPesquisa.ClearSelection()
            cboPesquisa.Text = ""
            Exit Sub
        End If

        Dim URL As String = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & DirectCast(sender, RadComboBox).SelectedValue
        
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, DirectCast(sender, RadComboBox).Text, 800, 550, DirectCast(sender, RadComboBox).SelectedValue.Replace(".", "_")), False)

        cboPesquisa.ClearSelection()
        cboPesquisa.Text = ""
    End Sub

End Class