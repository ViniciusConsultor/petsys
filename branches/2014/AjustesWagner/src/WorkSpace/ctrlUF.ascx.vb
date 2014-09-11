Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Public Class ctrlUF
    Inherits UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
        CarregueCombo()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboUF, Control))
        cboUF.ClearSelection()
    End Sub

    Public Property Codigo As String
        Get
            Return cboUF.SelectedValue
        End Get
        Set(ByVal value As String)
            cboUF.SelectedValue = value
        End Set
    End Property
    Public Property Sigla As UF
        Get
            If (String.IsNullOrEmpty(cboUF.SelectedValue)) Then
                Return Nothing
            Else : Return UF.Obtenha(CShort(cboUF.SelectedValue))
            End If
        End Get
        Set(value As UF)

        End Set
    End Property

    Private Sub CarregueCombo()
        For Each estado In UF.ObtenhaTodos()
            Dim item = New RadComboBoxItem(estado.Nome, estado.ID.ToString())

            item.Attributes.Add("Sigla", estado.Sigla)

            cboUF.Items.Add(item)
            item.DataBind()
        Next
    End Sub

End Class