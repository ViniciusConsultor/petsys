Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Partial Public Class ctrlMunicipios
    Inherits System.Web.UI.UserControl

    Public Event MunicipioFoiSelecionado(ByVal Municipio As IMunicipio)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub cboMunicipios_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboMunicipios.ItemsRequested
        Dim Municipios As IList(Of IMunicipio)

        Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
            Municipios = Servico.ObtenhaMunicipiosPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Municipios Is Nothing Then
            For Each Municipio As IMunicipio In Municipios
                Dim Item As New RadComboBoxItem(Municipio.Nome, Municipio.ID.ToString)

                Item.Attributes.Add("UF", Municipio.UF.Nome)
                cboMunicipios.Items.Add(Item)
                Item.DataBind()
            Next
        End If

    End Sub

    Private Sub cboMunicipios_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboMunicipios.SelectedIndexChanged
        Dim Municipio As IMunicipio
        Dim Valor As String

        If Not cboMunicipios.AutoPostBack Then Exit Sub

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            MunicipioSelecionado = Nothing
            Exit Sub
        End If

        Using Servico As IServicoDeMunicipio = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMunicipio)()
            Municipio = Servico.ObtenhaMunicipio(CLng(Valor))
        End Using

        MunicipioSelecionado = Municipio
        RaiseEvent MunicipioFoiSelecionado(Municipio)
    End Sub

    Public Property MunicipioSelecionado() As IMunicipio
        Get
            Return CType(ViewState(Me.ClientID), IMunicipio)
        End Get
        Set(ByVal value As IMunicipio)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlMunicipio, Control))
        MunicipioSelecionado = Nothing
        cboMunicipios.ClearSelection()
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(pnlMunicipio, Control), Habilitar)
    End Sub

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboMunicipios.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboMunicipios.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public Property NomeDoMunicipio() As String
        Get
            Return cboMunicipios.Text
        End Get
        Set(ByVal value As String)
            cboMunicipios.Text = value
        End Set
    End Property

    Public WriteOnly Property ExibeTituloParaSelecionarUmItem() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                cboMunicipios.EmptyMessage = "Selecione um município"
                Exit Property
            End If
            cboMunicipios.EmptyMessage = ""
        End Set
    End Property

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            cboMunicipios.AutoPostBack = value
        End Set
    End Property

End Class