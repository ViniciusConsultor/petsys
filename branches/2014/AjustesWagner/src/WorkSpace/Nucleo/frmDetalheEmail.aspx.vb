Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Negocio.Filtros.HistoricoDeEmail
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Public Class frmDetalheEmail
    Inherits Superpagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PreparaTela()

            Dim id As Nullable(Of Long)

            If (Not String.IsNullOrEmpty(Request.QueryString("Id"))) Then
                id = Convert.ToInt64(Request.QueryString("Id"))

                If id Is Nothing Then
                    Throw New Exception("Histórico de e-mail inexistente")
                End If

                MostreEmail(id)
            End If
        End If
    End Sub

    Private Sub PreparaTela()
        UtilidadesWeb.LimparComponente((CType(pnlDadosDoEmail, Control)))
        ctrlTemplateDeEmail.Inicializa()
        MostraDestinarios(New List(Of String))
        MostraDestinariosCC(New List(Of String))
        MostraDestinariosCCo(New List(Of String))
    End Sub

    Private Sub MostraDestinarios(destinarios As IList(Of String))
        If destinarios Is Nothing Then
            destinarios = New List(Of String)()
        End If

        grdDestinatarios.DataSource = destinarios
        grdDestinatarios.DataBind()
    End Sub

    Private Sub MostraDestinariosCC(destinarios As IList(Of String))
        If destinarios Is Nothing Then
            destinarios = New List(Of String)()
        End If

        grdDestinariosCC.DataSource = destinarios
        grdDestinariosCC.DataBind()
    End Sub

    Private Sub MostraDestinariosCCo(destinarios As IList(Of String))
        If destinarios Is Nothing Then
            destinarios = New List(Of String)()
        End If

        grdDestinatariosCCo.DataSource = destinarios
        grdDestinatariosCCo.DataBind()
    End Sub

    Private Sub MostreEmail(id As Nullable(Of Long))
        Dim filtro As IFiltroHistoricoDeEmailPorID = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorID)()

        filtro.Operacao = OperacaoDeFiltro.IgualA
        filtro.ValorDoFiltro = id.Value.ToString()

        Using Servico As IServicoDeEnvioDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeEnvioDeEmail)()
            historico = Servico.ObtenhaHistoricos(filtro, 1, 0).Item(0)
        End Using

        txtData.SelectedDate = historico.Data
        txtContexto.Text = historico.Contexto
        txtRemetente.Text = historico.Remetente
        txtAssunto.Text = historico.Assunto
        ctrlTemplateDeEmail.TextoDoTemplate = historico.Mensagem
        MostraDestinarios(historico.Destinatarios)
        MostraDestinariosCC(historico.DestinatariosEmCopia)
        MostraDestinariosCCo(historico.DestinatariosEmCopiaOculta)

    End Sub

    Private Property Historico As IHistoricoDeEmail
        Get
            Return CType(ViewState("HISTORICO_DO_CONTEXTO"), IHistoricoDeEmail)
        End Get
        Set(value As IHistoricoDeEmail)
            ViewState("HISTORICO_DO_CONTEXTO") = value
        End Set
    End Property

    Protected Sub grdDestinatarios_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)
        UtilidadesWeb.PaginacaoDataGrid(grdDestinatarios, Historico.Destinatarios, e)
    End Sub

    Protected Sub grdDestinatariosCC_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)
        UtilidadesWeb.PaginacaoDataGrid(grdDestinariosCC, Historico.Destinatarios, e)
    End Sub

    Protected Sub grdAnexos_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)

    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return ""
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return Nothing
    End Function

    Protected Sub grdDestinatariosCCo_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)
        UtilidadesWeb.PaginacaoDataGrid(grdDestinatariosCCo, Historico.Destinatarios, e)
    End Sub

End Class