Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados.Componentes.Web

Public Class ctrlPais
    Inherits System.Web.UI.UserControl

    Public Event PaisFoiSelecionado(ByVal Pais As IPais)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub cboPais_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboPais.ItemsRequested
        Dim Paises As IList(Of IPais)

        Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()
            Paises = Servico.ObtenhaPaisesPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Paises Is Nothing Then
            For Each Pais As IPais In Paises
                Dim Item As New RadComboBoxItem(Pais.Nome, Pais.ID.ToString)

                Item.Attributes.Add("Sigla", Pais.Sigla)
                cboPais.Items.Add(Item)
                Item.DataBind()
            Next
        End If

    End Sub

    Private Sub cboPais_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPais.SelectedIndexChanged
        Dim Pais As IPais
        Dim Valor As String

        Valor = DirectCast(o, RadComboBox).SelectedValue
        If String.IsNullOrEmpty(Valor) Then
            PaisSelecionado = Nothing
            Return
        End If

        Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()
            Pais = Servico.ObtenhaPais(CLng(Valor))
        End Using

        PaisSelecionado = Pais
        RaiseEvent PaisFoiSelecionado(Pais)
    End Sub

    Public Property PaisSelecionado() As IPais
        Get
            Return CType(ViewState(Me.ClientID), IPais)
        End Get
        Set(ByVal value As IPais)
            ViewState.Add(Me.ClientID, value)
        End Set
    End Property

    Public Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlPais, Control))
        PaisSelecionado = Nothing
        cboPais.ClearSelection()
    End Sub

    Public Sub HabiliteComponente(ByVal Habilitar As Boolean)
        UtilidadesWeb.HabilitaComponentes(CType(pnlPais, Control), Habilitar)
    End Sub

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboPais.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboPais.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return cboPais.Text
        End Get
        Set(ByVal value As String)
            cboPais.Text = value
        End Set
    End Property

    Public WriteOnly Property ExibeTituloParaSelecionarUmItem() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                cboPais.EmptyMessage = "Selecione um país"
                Exit Property
            End If
            cboPais.EmptyMessage = ""
        End Set
    End Property

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            cboPais.AutoPostBack = value
        End Set
    End Property

End Class