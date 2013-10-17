Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI

Public Class ctrlCliente
    Inherits System.Web.UI.UserControl

    Public Event ClienteFoiSelecionado(ByVal cliente As ICliente)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Inicializa()
        LimparControle()
    End Sub

    Private Sub LimparControle()
        UtilidadesWeb.LimparComponente(CType(cboCliente, Control))
        ClienteSelecionado = Nothing
        cboCliente.ClearSelection()
        BotaoNovoEhVisivel = False
    End Sub

    Private Property Nome() As String
        Get
            Return cboCliente.Text
        End Get
        Set(ByVal value As String)
            cboCliente.Text = value
        End Set
    End Property

    Private Sub btnNovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNovo.Click
        Dim URL As String

        URL = ObtenhaURL()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de clientes", 650, 480), False)
    End Sub

    Private Function ObtenhaURL() As String
        Dim URL As String

        URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual
        URL = String.Concat(URL, "Nucleo/cdCliente.aspx")
        Return URL
    End Function

    Public Property ClienteSelecionado() As ICliente
        Get
            Return CType(ViewState(Me.ClientID), ICliente)
        End Get
        Set(ByVal value As ICliente)
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

    Protected Sub cboCliente_OnItemsRequested(ByVal sender As Object, ByVal e As RadComboBoxItemsRequestedEventArgs)
        Dim Clientes As IList(Of ICliente)

        Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCliente)()
            Clientes = Servico.ObtenhaPorNomeComoFiltro(e.Text, 50)
        End Using

        If Not Clientes Is Nothing Then
            For Each Cliente As ICliente In Clientes
                Dim Item As New RadComboBoxItem(Cliente.Pessoa.Nome.Trim, Cliente.Pessoa.ID.ToString)

                Item.Attributes.Add("DataNascimento", "")
                Item.Attributes.Add("TelefoneComercial", "")
                Item.Attributes.Add("TelefoneCelular", "")
                cboCliente.Items.Add(Item)
                Item.DataBind()
            Next
        End If
    End Sub

    Protected Sub cboCliente_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim Cliente As ICliente

        If String.IsNullOrEmpty(DirectCast(sender, RadComboBox).SelectedValue) Then Exit Sub

        Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeCliente)()
            Cliente = Servico.Obtenha(CLng(DirectCast(sender, RadComboBox).SelectedValue))
        End Using

        ClienteSelecionado = Cliente
        RaiseEvent ClienteFoiSelecionado(Cliente)
    End Sub

End Class