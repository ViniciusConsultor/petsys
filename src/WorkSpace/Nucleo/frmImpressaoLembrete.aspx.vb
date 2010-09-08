Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados

Partial Public Class frmImpressaoLembrete
    Inherits SuperPagina

    Private Const CHAVE_ID_PROPRIETARIO_LEMBRETE_IMPRESSAO As String = "CHAVE_ID_PROPRIETARIO_LEMBRETE_IMPRESSAO"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ExibaTelaInicial()
            If Not String.IsNullOrEmpty(Request.QueryString("IdProprietario")) Then
                ViewState(CHAVE_ID_PROPRIETARIO_LEMBRETE_IMPRESSAO) = CLng(Request.QueryString("IdProprietario"))
            End If
        End If
    End Sub

    Private Sub ExibaTelaInicial()
        UtilidadesWeb.LimparComponente(CType(pnlFiltro, Control))
        CarregaOpcoesDeImpressao()
    End Sub

    Private Sub CarregaOpcoesDeImpressao()
        cboOpcoesDeImpressao.Items.Clear()
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto + Descrição", "1"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Assunto", "2"))
        cboOpcoesDeImpressao.Items.Add(New RadComboBoxItem("Descrição", "3"))
    End Sub

    Private Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisar.Click
        Dim Lembretes As IList(Of ILembrete)
        Dim PossiveisInconsistencias As String

        PossiveisInconsistencias = ValidaDados()

        If Not String.IsNullOrEmpty(PossiveisInconsistencias) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(PossiveisInconsistencias), False)
            Exit Sub
        End If

        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            Lembretes = Servico.ObtenhaLembretes(CLng(ViewState(CHAVE_ID_PROPRIETARIO_LEMBRETE_IMPRESSAO)), _
                                                 txtDataInicial.SelectedDate.Value, _
                                                 txtDataFinal.SelectedDate)
        End Using

        If Lembretes Is Nothing OrElse Lembretes.Count = 0 Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Nenhum lembrete foi encontrada com o filtro que foi informado."), False)
            Exit Sub
        End If

        Dim GeradorDePDF As GerarLembretesEmPDF
        Dim NomeDoPDFGerado As String = Nothing
        Dim URL As String

        GeradorDePDF = New GerarLembretesEmPDF(Lembretes)

        Try
            If cboOpcoesDeImpressao.SelectedValue = "1" Then
                NomeDoPDFGerado = GeradorDePDF.GerePDF(True, True)
            ElseIf cboOpcoesDeImpressao.SelectedValue = "2" Then
                NomeDoPDFGerado = GeradorDePDF.GerePDF(True, False)
            ElseIf cboOpcoesDeImpressao.SelectedValue = "3" Then
                NomeDoPDFGerado = GeradorDePDF.GerePDF(False, True)
            End If

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
            Exit Sub
        End Try

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & UtilidadesWeb.PASTA_LOADS & "/" & NomeDoPDFGerado
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Imprimir"), False)
    End Sub

    Private Function ValidaDados() As String
        If Not txtDataInicial.SelectedDate.HasValue Then Return "A data de início do(s) lembrete(s) deve ser informada."

        Return Nothing
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Nothing
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return Nothing
    End Function


End Class