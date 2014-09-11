Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI

Public Class ctrlOperacaoFiltro
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
        CarregueCombo()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboOperacaoFiltro, Control))
        cboOperacaoFiltro.ClearSelection()
    End Sub

    Public Property Codigo As String
        Get
            Return cboOperacaoFiltro.SelectedValue
        End Get
        Set(ByVal value As String)
            cboOperacaoFiltro.SelectedValue = value
        End Set
    End Property

    Private Sub CarregueCombo()
        For Each operacao In OperacaoDeFiltro.ObtenhaTodos()
            Dim item = New RadComboBoxItem(operacao.Descricao, operacao.ID.ToString())

            item.Attributes.Add("Codigo", operacao.ID.ToString())

            cboOperacaoFiltro.Items.Add(item)
            item.DataBind()
        Next
    End Sub

End Class