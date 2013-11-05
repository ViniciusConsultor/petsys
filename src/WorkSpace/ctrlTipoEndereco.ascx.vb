Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Public Class ctrlTipoEndereco
    Inherits System.Web.UI.UserControl

    Public Event TipoFoiSelecionado(ByVal Tipo As ITipoDeEndereco)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboTiposDeEndereco, Control))
        cboTiposDeEndereco.ClearSelection()
        TipoSelecionado = Nothing
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(cboTiposDeEndereco, Control), Habilitar)
    End Sub

    Public Property Nome() As String
        Get
            Return cboTiposDeEndereco.Text
        End Get
        Set(ByVal value As String)
            cboTiposDeEndereco.Text = value
        End Set
    End Property

    Public Property TipoSelecionado() As ITipoDeEndereco
        Get
            Return CType(ViewState(Me.ClientID), ITipoDeEndereco)
        End Get
        Set(ByVal value As ITipoDeEndereco)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Public WriteOnly Property ExibeTituloParaSelecionarUmItem() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                cboTiposDeEndereco.EmptyMessage = "Selecione um tipo de endereço"
                Exit Property
            End If
            cboTiposDeEndereco.EmptyMessage = ""
        End Set
    End Property

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            cboTiposDeEndereco.AutoPostBack = value
        End Set
    End Property

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboTiposDeEndereco.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboTiposDeEndereco.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Private Sub cboTiposDeEndereco_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboTiposDeEndereco.ItemsRequested
        Dim TiposDeEndereco As IList(Of ITipoDeEndereco)

        Using Servico As IServicoDeTipoDeEndereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeTipoDeEndereco)()
            TiposDeEndereco = Servico.ObtenhaPorNome(e.Text, 50)
        End Using

        If Not TiposDeEndereco Is Nothing Then
            For Each Tipo As ITipoDeEndereco In TiposDeEndereco
                cboTiposDeEndereco.Items.Add(New RadComboBoxItem(Tipo.Nome, Tipo.ID.ToString))
            Next
        End If
    End Sub

    Private Sub cboTiposDeEndereco_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboTiposDeEndereco.SelectedIndexChanged
        Dim Tipo As ITipoDeEndereco
        Dim Valor As String

        Valor = DirectCast(sender, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            TipoSelecionado = Nothing
            Exit Sub
        End If

        Using Servico As IServicoDeTipoDeEndereco = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeTipoDeEndereco)()
            Tipo = Servico.Obtenha(CLng(Valor))
        End Using

        TipoSelecionado = Tipo
        RaiseEvent TipoFoiSelecionado(Tipo)
    End Sub

End Class