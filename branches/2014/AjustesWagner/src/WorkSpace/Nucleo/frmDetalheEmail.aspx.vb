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
        ctrlTemplateDeEmail.Inicializa()
    End Sub

    Private Sub MostreEmail(id As Nullable(Of Long))
        Dim filtro As IFiltroHistoricoDeEmailPorID = FabricaGenerica.GetInstancia().CrieObjeto(Of IFiltroHistoricoDeEmailPorID)()
        Dim historico As IHistoricoDeEmail

        filtro.Operacao = OperacaoDeFiltro.IgualA
        filtro.ValorDoFiltro = id.Value.ToString()

        Using Servico As IServicoDeEnvioDeEmail = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeEnvioDeEmail)()
            historico = Servico.ObtenhaHistoricos(filtro, 1, 0).Item(0)
        End Using

        'ctrlTemplateDeEmail.a historico
    End Sub

    Protected Sub grdDestinatarios_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)

    End Sub

    Protected Sub grdDestinatariosCC_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)

    End Sub

    Protected Sub grdAnexos_OnPageIndexChanged(ByVal sender As Object, ByVal e As GridPageChangedEventArgs)

    End Sub

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return ""
    End Function

    Protected Overrides Function ObtenhaBarraDeFerramentas() As RadToolBar
        Return Nothing
    End Function

End Class