Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Public Class ctrlCedente
    Inherits System.Web.UI.UserControl

    Public Event CedenteFoiSelecionado(ByVal cliente As ICedente)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboCedente, Control))
        CedenteSelecionado = Nothing
        cboCedente.ClearSelection()
        BotaoNovoEhVisivel = False
    End Sub

    Private Property Nome() As String
        Get
            Return cboCedente.Text
        End Get
        Set(ByVal value As String)
            cboCedente.Text = value
        End Set
    End Property

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanela(URL, "Cadastro de cedentes", 800, 550, "cdcedente_aspx"), False)
    End Sub

    Private Function ObtenhaURL() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdCedente.aspx")
        Return URL
    End Function

    Public Property CedenteSelecionado() As ICedente
        Get
            Return CType(ViewState(Me.ClientID), ICedente)
        End Get
        Set(ByVal value As ICedente)
            ViewState.Add(Me.ClientID, value)

            If Not value Is Nothing Then
                Nome = value.Pessoa.Nome
            End If
        End Set
    End Property

    Public WriteOnly Property BotaoNovoEhVisivel() As Boolean
        Set(ByVal value As Boolean)
            btnNovo.Visible = value
        End Set
    End Property

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim Principal As Compartilhados.Principal

        Principal = FabricaDeContexto.GetInstancia.GetContextoAtual
        'Só verificamos se tem permissão se o botão estiver marcado para ser exibido (pela aplicação)
        If btnNovo.Visible Then
            btnNovo.Visible = Principal.EstaAutorizado(btnNovo.CommandArgument)
        End If
    End Sub

    Private Sub cboCedente_ItemsRequested(sender As Object, e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cboCedente.ItemsRequested
        Dim Cedentes As IList(Of ICedente)

        Using Servico As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()
            Cedentes = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Cedentes Is Nothing Then
            For Each Cedente As ICedente In Cedentes
                Dim Item As New RadComboBoxItem(Cedente.Pessoa.Nome.Trim, Cedente.Pessoa.ID.ToString)
                cboCedente.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Private Sub cboCedente_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCedente.SelectedIndexChanged
        Dim Cedente As ICedente

        If String.IsNullOrEmpty(DirectCast(sender, RadComboBox).SelectedValue) Then
            CedenteSelecionado = Nothing
            Exit Sub
        End If

        Using Servico As IServicoDeCedente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCedente)()
            Cedente = Servico.Obtenha(CLng(DirectCast(sender, RadComboBox).SelectedValue))
        End Using

        CedenteSelecionado = Cedente
        RaiseEvent CedenteFoiSelecionado(Cedente)
    End Sub

End Class