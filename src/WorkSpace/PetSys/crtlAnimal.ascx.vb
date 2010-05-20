Imports PetSys.Interfaces.Negocio
Imports Compartilhados.Componentes.Web
Imports Telerik.Web.UI
Imports Compartilhados
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas

Partial Public Class crtlAnimal
    Inherits System.Web.UI.UserControl

    Public Event AnimalFoiSelecionado(ByVal Animal As IAnimal)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        EhObrigatorio = False
        AutoPostBack = True
        LimparControle()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(pnlAnimal, Control))
        AnimalSelecionado = Nothing
    End Sub

    Private WriteOnly Property HabilitaControles() As Boolean
        Set(ByVal value As Boolean)
            UtilidadesWeb.HabilitaComponentes(CType(pnlAnimal, Control), value)
        End Set
    End Property

    Public WriteOnly Property EnableLoadOnDemand() As Boolean
        Set(ByVal value As Boolean)
            cboAnimal.EnableLoadOnDemand = value
        End Set
    End Property

    Public WriteOnly Property ShowDropDownOnTextboxClick() As Boolean
        Set(ByVal value As Boolean)
            cboAnimal.ShowDropDownOnTextboxClick = value
        End Set
    End Property

    Public Property NomeDoAnimal() As String
        Get
            Return cboAnimal.Text
        End Get
        Set(ByVal value As String)
            cboAnimal.Text = value
        End Set
    End Property

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de animais"), False)
    End Sub

    Private Sub btnDetalhar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDetalhar.Click
        Dim URL As String

        URL = ObtenhaURL()
        URL = String.Concat(URL, "?Id=", AnimalSelecionado.ID.Value.ToString)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de animais"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLCorrente

        URL = String.Concat(URL, "cdAnimal.aspx")
        Return URL
    End Function

    Public Property AnimalSelecionado() As IAnimal
        Get
            Return CType(Session(Me.ClientID), IAnimal)
        End Get
        Set(ByVal value As IAnimal)
            Session.Add(Me.ClientID, value)
        End Set
    End Property

    Public WriteOnly Property BotaoNovoEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            btnNovo.Visible = value
        End Set
    End Property

    Public WriteOnly Property BotaoDetalharEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            btnDetalhar.Visible = value
        End Set
    End Property

    Public WriteOnly Property EhObrigatorio() As Boolean
        Set(ByVal value As Boolean)
            rfvAnimal.Enabled = value
        End Set
    End Property

    Public WriteOnly Property TextoItemVazio() As String
        Set(ByVal value As String)
            cboAnimal.EmptyMessage = value
        End Set
    End Property

    'Verifica se o usuário tem permissão para cadastrar nova pessoa e se tem permissão para detalhar (ver os dados)
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnDetalhar.Visible Then
            Principal.EstaAutorizado(btnDetalhar.CommandArgument)
        End If
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnNovo.Visible Then
            Principal.EstaAutorizado(btnNovo.CommandArgument)
        End If
    End Sub

    Private Sub cboAnimal_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboAnimal.ItemsRequested
        Dim Animais As IList(Of IAnimal) = Nothing

        Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
            Animais = Servico.ObtenhaAnimaisPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Animais Is Nothing Then
            For Each Animal As IAnimal In Animais
                Dim Item As New RadComboBoxItem(Animal.Nome, Animal.ID.Value.ToString)

                If Not Animal.DataDeNascimento Is Nothing Then
                    Item.Attributes.Add("DataNascimento", Animal.DataDeNascimento.Value.ToString("dd/MM/yyyy"))
                Else
                    Item.Attributes.Add("DataNascimento", "Não informada")
                End If

                Item.Attributes.Add("Especie", Animal.Especie.Descricao)

                If Not String.IsNullOrEmpty(Animal.Raca) Then
                    Item.Attributes.Add("Raca", Animal.Raca)
                Else
                    Item.Attributes.Add("Raca", "Não informada")
                End If

                cboAnimal.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboAnimal_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboAnimal.SelectedIndexChanged
        Dim Animal As IAnimal = Nothing

        If String.IsNullOrEmpty(DirectCast(o, RadComboBox).SelectedValue) Then Exit Sub

        Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
            Animal = Servico.ObtenhaAnimal(CLng(DirectCast(o, RadComboBox).SelectedValue))
        End Using

        AnimalSelecionado = Animal
        RaiseEvent AnimalFoiSelecionado(Animal)
    End Sub

    Public WriteOnly Property AutoPostBack() As Boolean
        Set(ByVal value As Boolean)
            cboAnimal.AutoPostBack = value
        End Set
    End Property

End Class