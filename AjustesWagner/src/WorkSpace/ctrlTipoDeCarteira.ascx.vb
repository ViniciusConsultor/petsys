Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Telerik.Web.UI

Public Class ctrlTipoDeCarteira
    Inherits UserControl

    Public Event TipoDeCarteiraFoiSelecionada As TipoDeCarteiraFoiSelecionadaEventHandler
    Public Delegate Sub TipoDeCarteiraFoiSelecionadaEventHandler(tipoDeCarteira As TipoDeCarteira)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
        CarregueCombo()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboTipoDeCarteira, Control))
        cboTipoDeCarteira.ClearSelection()
    End Sub

    Public Property Codigo As String
        Get
            Return cboTipoDeCarteira.SelectedValue
        End Get
        Set(ByVal value As String)
            cboTipoDeCarteira.SelectedValue = value
        End Set
    End Property
    Public Property Sigla As TipoDeCarteira
        Get
            If (String.IsNullOrEmpty(cboTipoDeCarteira.SelectedValue)) Then
                Return Nothing
            Else : Return TipoDeCarteira.Obtenha(CShort(cboTipoDeCarteira.SelectedValue))
            End If
        End Get
        Set(value As TipoDeCarteira)

        End Set
    End Property

    Private Sub CarregueCombo()
        For Each carteira In TipoDeCarteira.ObtenhaTodos()
            Dim item = New RadComboBoxItem(carteira.Nome, carteira.ID.ToString())

            item.Attributes.Add("Sigla", carteira.Sigla)

            cboTipoDeCarteira.Items.Add(item)
            item.DataBind()
        Next
    End Sub

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(value As Boolean)
            cboTipoDeCarteira.AutoPostBack = value
        End Set
    End Property

    Public Property TipoDeCarteiraSelecionada() As TipoDeCarteira
        Get
            Return DirectCast(ViewState(ClientID), TipoDeCarteira)
        End Get
        Set(value As TipoDeCarteira)
            ViewState.Add(Me.ClientID, value)
            cboTipoDeCarteira.SelectedValue = value.ID.ToString()
        End Set
    End Property

    Protected Sub cboTipoDeCarteira_SelectedIndexChanged(o As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        If Not cboTipoDeCarteira.AutoPostBack Then
            Return
        End If

        Dim carteira As TipoDeCarteira = Nothing

        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then
            LimparControle()
            Return
        End If

        carteira = TipoDeCarteira.Obtenha(CType((DirectCast(o, RadComboBox).SelectedValue), Short))

        TipoDeCarteiraSelecionada = carteira

        RaiseEvent TipoDeCarteiraFoiSelecionada(carteira)
    End Sub

End Class